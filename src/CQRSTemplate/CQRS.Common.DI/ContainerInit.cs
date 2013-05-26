namespace CQRS.Common.DI
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;

    using Castle.Core;
    using Castle.Facilities.Startable;
    using Castle.Facilities.TypedFactory;
    using Castle.MicroKernel;
    using Castle.MicroKernel.Context;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.Resolvers.SpecializedResolvers;
    using Castle.Windsor;

    using NHibernate;

    using CQRS.Base.CQRS.Commands.Decorator;
    using CQRS.Base.CQRS.Commands.Handler;
    using CQRS.Base.CQRS.Query.Attributes;
    using CQRS.Base.DDD.Domain.Annotations;
    using CQRS.Base.DDD.Infrastructure.Events;
    using CQRS.Base.DDD.Infrastructure.Events.Implementation;
    using CQRS.Base.Infrastructure.Attributes;
    using CQRS.Base.Infrastructure.NHibernate;
    using CQRS.Base.Infrastructure.NHibernate.Conventions;

    public class ContainerInit
    {
        public static IWindsorContainer RegisterDI(Assembly webAssembly)
        {
            IWindsorContainer container = new WindsorContainer();

            AddResolversAndFacilities(container);
            RegisterMvcControllers(container, webAssembly);

            // Temp
            RegisterEventPublishers(container);

            // Register event component listeners
            // This line resolves IEventSubscriber
            container.AddFacility<SubscribeEventListenerFacility>();

            RegisterDomain(container);            
            RegisterSingletons(container);
            RegisterORM(container);            
            RegisterStatefullComponents(container, webAssembly);
            RegisterEventListeners(container);
            RegisterCommandHandlers(container);
            SetMvcContainer(container);
            return container;
        }

        private static void RegisterEventPublishers(IWindsorContainer container)
        {
            container.Register(
                Classes.FromAssemblyContaining<SimpleEventPublisher>()
                    .Where(x => x == typeof(SimpleEventPublisher))
                    .WithServiceAllInterfaces()
                    .LifestyleSingleton());
        }

        private static void RegisterSingletons(IWindsorContainer container)
        {
            container.Register(
                Classes.FromAssemblyInDirectory(new AssemblyFilter(HttpRuntime.BinDirectory))
                    .Where(t => t.IsComponentLifestyle(ComponentLifestyle.Singleton))
                    .StartIfNecessary()
                    .WithServiceAllInterfaces()
                    .WithServiceSelf()
                    .LifestyleSingleton());
        }

        private static void RegisterDomain(IWindsorContainer container)
        {
            container.Register(
                Classes.FromAssemblyInDirectory(new AssemblyFilter(HttpRuntime.BinDirectory))
                    .Where(t => t.IsComponentLifestyle(ComponentLifestyle.Transient) || t.IsDefined(typeof(DomainFactoryAttribute), true)
                        || t.IsDefined(typeof(DomainRepositoryImplementationAttribute), true)
                        || t.IsDefined(typeof(DomainServiceAttribute), true) || t.IsDefined(typeof(ReaderAttribute), true))
                    .WithServiceAllInterfaces()
                    .WithServiceSelf()
                    .LifestyleTransient());
        }

        private static void RegisterORM(IWindsorContainer container)
        {
            AutomappingConfiguration.IsEntityPredicate = t =>
                t.IsDefined(typeof(DomainEntityAttribute), true);

            AutomappingConfiguration.IsComponentPredicate =
                t => t.IsDefined(typeof(DomainValueObjectAttribute), true);

            container.Register(Component.For<ISessionFactory>()
                                   .UsingFactoryMethod(() => EntityManager.SessionFactory)
                                   .LifestyleSingleton());

            container.Register(Component.For<ISession>()
                                   .UsingFactoryMethod(EntityManager.SessionFactory.OpenSession)
                                   .LifestylePerWebRequest());

            container.Register(Component.For<IPerRequestSessionFactory>()
                                   .AsFactory());

            container.Register(Component.For<IEntityManager>()
                                   .ImplementedBy<EntityManager>()
                                   .LifestyleSingleton());
        }

        private static void RegisterMvcControllers(IWindsorContainer container, Assembly webAssembly)
        {
            container.Register(Classes.FromAssembly(webAssembly)
                                   .BasedOn<IController>()
                                   .LifestyleTransient());
        }

        private static void SetMvcContainer(IWindsorContainer container)
        {
            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        private static void RegisterStatefullComponents(IWindsorContainer container, Assembly webAssembly)
        {
            container.Register(Classes.FromAssembly(webAssembly)
                                   .Where(t => t.IsComponentLifestyle(ComponentLifestyle.PerSession))
                                   .WithServiceAllInterfaces()
                                   .WithServiceSelf()
                                   .LifestylePerSession());
        }

        private static void RegisterEventListeners(IWindsorContainer container)
        {
            container.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter(HttpRuntime.BinDirectory))
                                   .Where(l => l.IsEventListener())
                                   .WithServiceAllInterfaces()
                                   .Configure(x => x.Start())
                                   .LifestyleSingleton());
        }

        private static void RegisterCommandHandlers(IWindsorContainer container)
        {
            // Register decorators
            container.Register(
                Component.For(typeof(ICommandHandler<>)).ImplementedBy(typeof(TransactionalCommandHandlerDecorator<>)),
                Component.For(typeof(ICommandHandler<>)).ImplementedBy(typeof(CommitNHibernateCommandHandlerDecorator<>)),
                Component.For(typeof(ICommandHandler<>)).ImplementedBy(typeof(ConatinerCommandHandlerDecorator<>)));

            foreach (var assembly in EntityManager.GetAssemblies())
            {
                foreach (var registration in from f in assembly.GetTypes()
                                             where f.IsClass
                                             from i in f.GetInterfaces()
                                             where
                                                 i.IsGenericType &&
                                                 i.GetGenericTypeDefinition() == typeof(ICommandHandler<>)
                                             let genericArgument = i.GetGenericArguments()[0]
                                             where !genericArgument.ContainsGenericParameters
                                             select new { Impl = f, Key = genericArgument.FullName })
                {
                    container.Register(Component.For<ICommandHandler>()
                                           .ImplementedBy(registration.Impl)
                                           .Named(registration.Key)
                                           .LifestyleTransient());
                }
            }
            container.Register(Component.For<ICommandHandlerFactory>()
                                   .AsFactory(f => f.SelectedWith(new CommandHandlerFactoryComponentSelector())));
        }

        private static void AddResolversAndFacilities(IWindsorContainer container)
        {
            container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel));
            container.AddFacility<TypedFactoryFacility>();
            container.AddFacility<StartableFacility>();
        }
    }
}
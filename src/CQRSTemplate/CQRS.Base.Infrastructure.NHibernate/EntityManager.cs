using System;
using System.Reflection;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using CQRS.Base.Infrastructure.NHibernate.Conventions;

namespace CQRS.Base.Infrastructure.NHibernate
{
    public class EntityManager : IEntityManager
    {
        public static Func<Assembly[]> GetAssemblies = () => new Assembly[0];

        private readonly IPerRequestSessionFactory _perRequestSessionFactory;

        public EntityManager(IPerRequestSessionFactory perRequestSessionFactory)
        {
            _perRequestSessionFactory = perRequestSessionFactory;
        }

        private static readonly Lazy<ISessionFactory> NHibernateFactory =
           new Lazy<ISessionFactory>(() =>
           {
               var config = Configure();
               var factory = config.BuildSessionFactory();
               return factory;
           });

        private static Configuration Configure()
        {
            var config = Fluently.Configure()
                .Database(
                MsSqlConfiguration.MsSql2008.ConnectionString(b => b.FromConnectionStringWithKey("db"))
                )
                .Mappings(m =>
                {
                    var model = AutoMap.Assemblies(new AutomappingConfiguration(), GetAssemblies());
                    model.Conventions.Add(new SetEnumTypeConvention());
                    model.Conventions.Add(new UseNewSqlDateTime2TypeConvention());
                    model.Conventions.Add(new CollectionAccessConvention());
                    model.Conventions.Add(new SqlTimestampConvention());
                    model.Conventions.Add(new SetTableNameConvention());
                    model.Conventions.Add(DefaultLazy.Never());
                    model.Conventions.Add(AutoImport.Never());
                    foreach (var assembly in GetAssemblies())
                    {
                        model.AddMappingsFromAssembly(assembly);
                    }
                    m.AutoMappings.Add(model);
                })
                .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(true, true))
                .BuildConfiguration();
            return config;
        }
        public static ISessionFactory SessionFactory { get { return NHibernateFactory.Value; } }

        public ISession CurrentSession
        {
            get { return _perRequestSessionFactory.Create(); }
        }
    }
}
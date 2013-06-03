using CQRS.Base.CQRS.Commands.Handler;

namespace CQRS.Base.Infrastructure.EntityFramework
{
    public class CommitCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> _inner;
        public IEntityManager Manager { get; set; }

        public CommitCommandHandlerDecorator(ICommandHandler<TCommand> inner)
        {
            _inner = inner;
        }

        public void Handle(TCommand command)
        {
            _inner.Handle(command);
            Manager.CurrentContext.SaveChanges();
        }
    }
}
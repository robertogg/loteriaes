namespace LoteriaES.Core.CQRS
{
    public interface ICommandHandler<in TCommand>
    {
        void Execute(TCommand command);
    }
}

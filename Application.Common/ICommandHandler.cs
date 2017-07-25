namespace Application.Common
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        CommandResult HandleCommand(TCommand command);
    }
}
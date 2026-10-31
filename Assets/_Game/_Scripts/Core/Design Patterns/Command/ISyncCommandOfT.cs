namespace Game.Core.DesignPatterns.Command
{
    public interface ISyncCommand<T> : ICommand
    {
        T Execute();
    }
}

namespace Game.Core.DesignPatterns.Command
{
    public interface ISyncCommand : ICommand
    {
        void Execute();
    }

}

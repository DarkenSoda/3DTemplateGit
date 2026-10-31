namespace Game.Core.DesignPatterns.Command
{
    public interface ICommandFactory
    {
        void Populate<T>(T command) where T : ICommand;
    }
}

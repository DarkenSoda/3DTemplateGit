using VContainer;

namespace Game.Core.DesignPatterns.Command
{
    public sealed class CommandFactory : ICommandFactory
    {
        private readonly IObjectResolver _objectResolver;
        public CommandFactory(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }
        public void Populate<T>(T command) where T : ICommand
        {
            _objectResolver.Inject(command);
        }
    }
}

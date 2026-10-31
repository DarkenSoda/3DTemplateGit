using System.Threading.Tasks;
using System;

namespace Game.Core.DesignPatterns.Command
{
    public interface ICommandDispatcher
    {
        void RegisterAndExecuteSync(Func<ISyncCommand> command);
        Task RegisterAndExecuteAsync(Func<IAsyncCommand> command);

        T RegisterAndExecuteSync<T>(Func<ISyncCommand<T>> command);
        Task<T> RegisterAndExecuteAsync<T>(Func<IAsyncCommand<T>> command);

        void AbortAll();
    }
}
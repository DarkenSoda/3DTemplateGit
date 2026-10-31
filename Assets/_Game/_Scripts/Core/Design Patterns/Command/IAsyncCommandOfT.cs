using System.Threading;
using System.Threading.Tasks;

namespace Game.Core.DesignPatterns.Command
{
    public interface IAsyncCommand<T> : ICommand
    {
        Task<T> ExecuteAsync(CancellationToken cancellationToken);
    }
}

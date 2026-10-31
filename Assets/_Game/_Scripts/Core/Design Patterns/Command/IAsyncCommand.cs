using System.Threading;
using System.Threading.Tasks;

namespace Game.Core.DesignPatterns.Command
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}

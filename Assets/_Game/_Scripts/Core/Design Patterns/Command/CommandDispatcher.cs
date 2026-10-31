using System;
using System.Threading;
using System.Threading.Tasks;

namespace Game.Core.DesignPatterns.Command
{
    public sealed class CommandDispatcher : ICommandDispatcher, IDisposable
    {
        private readonly ICommandFactory _commandFactory;
        private CancellationTokenSource _cancellationTokenSource;
        public CommandDispatcher(ICommandFactory commandFactory)
        {
            _commandFactory = commandFactory;
            RefreshCancellationToken();
        }
        ~CommandDispatcher()
        {
            Dispose();
        }

        public void RegisterAndExecuteSync(Func<ISyncCommand> command)
        {
            var commandToExecute = command.Invoke();

            try
            {
                _commandFactory.Populate(commandToExecute);
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.Log($"Failed to execute command: {ex.Message}");
                return;
            }

            commandToExecute.Execute();
        }

        public async Task RegisterAndExecuteAsync(Func<IAsyncCommand> command)
        {
            var commandToExecute = command.Invoke();

            try
            {
                _commandFactory.Populate(commandToExecute);
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.Log($"Failed to execute command: {ex.Message}");
                return;
            }

            await commandToExecute.ExecuteAsync(_cancellationTokenSource.Token);
        }

        public T RegisterAndExecuteSync<T>(Func<ISyncCommand<T>> command)
        {
            var commandToExecute = command.Invoke();

            try
            {
                _commandFactory.Populate(commandToExecute);
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.Log($"Failed to execute command: {ex.Message}");
                return default;
            }

            return commandToExecute.Execute();
        }

        public async Task<T> RegisterAndExecuteAsync<T>(Func<IAsyncCommand<T>> command)
        {
            var commandToExecute = command.Invoke();

            try
            {
                _commandFactory.Populate(commandToExecute);
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.Log($"Failed to execute command: {ex.Message}");
                return default;
            }

            var result = await commandToExecute.ExecuteAsync(_cancellationTokenSource.Token);

            return result;
        }
        private void RefreshCancellationToken()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();

            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void AbortAll()
        {
            RefreshCancellationToken();
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
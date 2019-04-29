using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FoodIdentifier.Common
{
    public class AsyncCommand : ICommand
    {
        private readonly Func<Task> _execute;
        private readonly Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public AsyncCommand(Func<Task> execute) : this(execute, null)
        {
        }

        public AsyncCommand(Func<Task> execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));

            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public async void Execute(object parameter)
        {
            await ExecuteAsync();
        }

        public async void Execute()
        {
            await ExecuteAsync();
        }


        public async Task ExecuteAsync()
        {
            await _execute();
        }

        public void RaiseCanExecuteChange()
        {
            var handler = CanExecuteChanged;
            handler?.Invoke(this, EventArgs.Empty);
        }
    }
}

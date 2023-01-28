using System;
using System.Windows.Input;

namespace Zameb.ChordFinder.App.Infra
{
    public class DelegateCommand : ICommand
    {
        private readonly Func<object?, bool>? canExecute;
        private readonly Action<object?> execute;

        public DelegateCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            execute(parameter);
        }
    }
}

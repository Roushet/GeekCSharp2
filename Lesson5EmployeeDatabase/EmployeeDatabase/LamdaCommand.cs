using System;
using System.Windows;
using System.Windows.Input;

namespace EmployeeDatabase
{
    class LamdaCommand : ICommand
    {
        /// <inheritdoc />
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;     // Делегируем генерацию этого события другому событию в менеджере команд
            remove => CommandManager.RequerySuggested -= value;
        }

        private readonly Action<object> _Action;

        private readonly Func<object, bool> _CanExecuteChecker;

        public LamdaCommand(Action<object> Execute, Func<object, bool> CanExecute = null)
        {
            _Action = Execute ?? throw new ArgumentNullException(nameof(Execute));
            _CanExecuteChecker = CanExecute;
        }

        /// <inheritdoc />
        public bool CanExecute(object parameter) => _CanExecuteChecker?.Invoke(parameter) ?? true;

        /// <inheritdoc />
        public void Execute(object parameter) => _Action(parameter);
    }

    class CloseApplication : LamdaCommand
    {
        public CloseApplication() : base(o => Application.Current.Shutdown()) { }
    }
}
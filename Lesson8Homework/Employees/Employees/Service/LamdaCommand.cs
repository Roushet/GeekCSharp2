using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Employees.Annotations;

namespace Employees
{
	/// <summary>Универсальная команда</summary>
	class LamdaCommand : ICommand
	{
		/// <summary>Событие, возникающее когда команда меняет свой статус (возможности выполнения)</summary>
		public event EventHandler CanExecuteChanged
		{
			add => CommandManager.RequerySuggested += value;	 // Делегируем генерацию события классу диспетчера команд
			remove => CommandManager.RequerySuggested -= value;
		}

		/// <summary>Метд выполнения команды</summary>
		[NotNull] private readonly Action<object> _Execute;
		/// <summary>МЕтод проверки возможности выполнения команды</summary>
		[NotNull] private readonly Func<object, bool> _CanExecute;

		/// <summary>Новая универсальная команда</summary>
		/// <param name="execute">Метод исполнения команды</param>
		/// <param name="can_execute">Метод проверки возможности исполнения команды</param>
		public LamdaCommand([NotNull] Action<object> execute, [CanBeNull] Func<object, bool> can_execute = null)
		{
			_Execute = execute ?? throw new ArgumentNullException(nameof(execute));
			_CanExecute = can_execute ?? (o => true);
		}

		/// <summary>Метод, который будет вызван, когда требуется выполнить команду</summary>
		/// <param name="parameter">Параметр выдова команды</param>
		public void Execute(object parameter)  => _Execute(parameter);

		/// <summary>Метод, Который будет вызван, когда потребуется проверить - можно ли выполнитьб данную команду</summary>
		/// <param name="parameter">Параметр команды</param>
		/// <returns>Истина, Если команда может быть выполнена</returns>
		public bool CanExecute(object parameter) => _CanExecute(parameter);
	}
}

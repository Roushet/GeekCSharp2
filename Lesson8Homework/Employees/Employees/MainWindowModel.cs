using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

/// <summary>
/// Сделано:
/// 1. Лист сотрудников берётся из базы
/// 2. Выводится через биндинг в главное окно программы
/// 3. Сделано добавление сотрудника
/// 4. Где то забыт связывющий эвент, поэтому после добавления сотрудника окно не обновляется, но после перезапуска программы он появляется в списке
/// Остальное сделать по человечески не успел 8(
/// </summary>

namespace Employees
{
    /// <summary>Модель представления главного окна программы</summary>
    [MarkupExtensionReturnType(typeof(MainWindowModel))] // Указываем процессору XAML тип объекта, который будет встроен в логическое дерево за счёт расширения разметки. Что бы он понимал, что это именно "MainWindowModel"
    class MainWindowModel : ViewModel
    {
        //Свойство заголовка окна модели отображения
        private string _WindowTitle;

        public string WindowTitle
        {
            get => _WindowTitle;
            set => Set(ref _WindowTitle, value);
        }

        private EmployeeList _list = new EmployeeList();
        public ObservableCollection<Employee> List => _list.List;


        public ICommand CloseApplicationCommand { get; }
        public ICommand AddEmployee { get; }

        public MainWindowModel()
		{
			CloseApplicationCommand = new LamdaCommand(OnCloseWindowCommandExecuted);
            AddEmployee = new LamdaCommand(OnAddEmployeeCommandExecuted);

        }

        private void OnAddEmployeeCommandExecuted(object obj)
        {
            //временная заглушка, нужна полноценная форма добавления сотрудника
            _list.AddEmployee(new Employee("Иван", 20, 60));
            OnPropertyChanged(nameof(List));
        }

        private void OnCloseWindowCommandExecuted(object parameter)
		{
			if (parameter is int exit_code)
				Application.Current.Shutdown(exit_code);
			else
				Application.Current.Shutdown();
		}
	}
}

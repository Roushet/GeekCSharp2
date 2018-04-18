using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    public class EmployeeList : ViewModel
    {
        //public event PropertyChangedEventHandler PropertyChanged;

        public readonly ObservableCollection<Employee> List;

        //конструктор без параметров
        public EmployeeList()
        {
            //заполняю список сотрудниками из базы данных в конструкторе класса
            List = new ObservableCollection<Employee>(DBconnector.GetPeople());
        }
        /// <summary>
        /// Публичный метод для добавления сотрудника в коллекцию
        /// </summary>
        /// <param name="employee">Тип сотрудник</param>
        public void AddEmployee(Employee employee)
        {
            List.Add(employee);
            //Заготовка для команды добавляения сотрудника
            DBconnector.AddEmployee(employee.Name, employee.Age, employee.Salary);
            OnPropertyChanged(nameof(List));
        }

        /// <summary>
        /// Публичный метод удаления сотрудника
        /// </summary>
        /// <param name="employee">Тип сотрудник, которого надо удалить</param>
        public void Remove(Employee employee)
        {
            if (List.Contains(employee))
                List.Remove(employee);
            OnPropertyChanged(nameof(List));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lesson5Homework
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Employee> Employes = new ObservableCollection<Employee>();
        ObservableCollection<Department> Departments = new ObservableCollection<Department>();

        public MainWindow()
        {
            InitializeComponent();


            Departments.Add(new Department("Охрана"));
            Departments.Add(new Department("Менеджеры"));

            Employes.Add(new Employee(1, "Антон", 10, 25, Departments[0]));
            Employes.Add(new Employee(2, "Иван", 10, 25, Departments[0]));
            Employes.Add(new Employee(3, "Алексей", 30, 40, Departments[1]));
            Employes.Add(new Employee(4, "Борис", 30, 36, Departments[1]));
            Employes.Add(new Employee(5, "Чингиз", 30, 32, Departments[1]));


            lvDepartment.ItemsSource = Departments;
            lvEmployees.ItemsSource = Employes;
        }

        private void lvEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //TODO Добавить вызов окна редактирования работника
            ListView list = sender as ListView;
            Employee item = list.SelectedItem as Employee;

            EmployeeEditor editor = new EmployeeEditor(item);
            editor.Show();
        }

        private void lvDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView list = sender as ListView;
            Department item = list.SelectedItem as Department;

            var empDept = (from x in Employes
                           where x.Department.DepartmentName == item.DepartmentName
                           select x);

            lvEmployees.ItemsSource = empDept;
        }
    }
}

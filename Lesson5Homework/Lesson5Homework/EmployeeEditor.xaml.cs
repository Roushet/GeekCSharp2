using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Lesson5Homework
{
    /// <summary>
    /// Логика взаимодействия для EmployeeEditor.xaml
    /// </summary>
    public partial class EmployeeEditor : Window
    {
        public Employee Emp;

        public EmployeeEditor(Employee employee)
        {
            InitializeComponent();

            this.empID.Text = employee.ID.ToString();
            this.empName.Text = employee.EmployeeName;
            this.empAge.Text = employee.Age.ToString();
            this.empSalary.Text = employee.Salary.ToString();
            this.empDepartment.Text = employee.Department.DepartmentName;

            Emp = employee;
        }

        private void empID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            TextBox tb = sender as TextBox;
            Emp.ID = Convert.ToInt32(tb.Text);
        }

        private void empSalary_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            TextBox tb = sender as TextBox;
            Emp.Salary = Convert.ToInt32(tb.Text);
        }

        private void empName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            TextBox tb = sender as TextBox;
            Emp.EmployeeName = tb.Text;
        }

        private void empAge_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            TextBox tb = sender as TextBox;
            Emp.Age = Convert.ToInt32(tb.Text);
        }

        //private void empID_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    TextBox tb = sender as TextBox;
        //    Emp.ID = Convert.ToInt32(tb.Text);

        //}

        //private void empName_TextChanged(object sender, TextChangedEventArgs e)
        //{

        //}

        //private void empSalary_TextChanged(object sender, TextChangedEventArgs e)
        //{

        //}

        //private void empAge_TextChanged(object sender, TextChangedEventArgs e)
        //{

        //}
    }
}

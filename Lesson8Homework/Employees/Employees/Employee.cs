using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Employees
{
    [DebuggerDisplay("Сотрудник {Name}, {Age} лет, зарплата {Salary} {Comment}")]
    public class Employee
    {
        private string _name;
        public string Name { get => _name; set => _name = value; }

        private int _age;
        public int Age { get => _age; set => _age = value; }

        private decimal _salary;
        public decimal Salary { get => _salary; set => _salary = value; }

        private string _comment;
        public string Comment { get => _comment; set => _comment = value; }


        public Employee() { }

        public Employee(string name, int age, decimal salary)
        {
            Name = name;
            Age = age;
            Salary = salary;
        }

    }
}

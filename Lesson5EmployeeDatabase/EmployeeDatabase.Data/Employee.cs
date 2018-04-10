using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EmployeeDatabase.Data
{
    [Serializable]
    [DebuggerDisplay("Сотрудник {Name}, {Age} лет, зарплата {Salary} {Comment}")]
    public class Employee
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public int Age { get; set; }

        [XmlAttribute]
        public decimal Salary { get; set; }
        
        public string Comment { get; set; }

        public Employee() { }

        public Employee(string name, int age, decimal salary)
        {
            Name = name;
            Age = age;
            Salary = salary;
        }
    }
}

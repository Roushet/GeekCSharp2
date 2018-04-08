using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5Homework
{
    public class Employee
    {
        public int ID { get; set; }
        public string EmployeeName { get; set; }
        public int Salary { get; set; }
        public int Age { get; set; }
        public  Department Department { get; set; }

        public Employee(int id, string name, int salary, int age, Department department)
        {
            ID = id;
            EmployeeName = name;
            Salary = salary;
            Age = age;
            Department = department;
        }

        public override string ToString()
        {
            return EmployeeName;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Serialization;

namespace EmployeeDatabase.Data
{
    [Serializable]
    [DebuggerDisplay("Отдел {Name}, число сотрудников {Employees.Count}")]
    public class Departament
    {
        [XmlAttribute]
        public string Name { get; set; }

        private List<Employee> _Employee;

        [XmlElement("Employee")]
        public List<Employee> Employees
        {
            get => _Employee ?? (_Employee = new List<Employee>());
            set => _Employee = value;
        }

        public Departament() { }
        public Departament(string name) => Name = name;
        public Departament(string name, IEnumerable<Employee> employees) : this(name) => _Employee = employees.ToList();
    }
}
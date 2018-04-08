using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5Homework
{
    public class Department
    {
        public string DepartmentName { get; set; }

        public Department(string name)
        {
            DepartmentName = name;
        }

        public override string ToString()
        {
            return DepartmentName;
        }
    }
}

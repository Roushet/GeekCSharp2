using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1___workers
{
    class Employee : Worker
    {
        public override float CalculateSalary()
        {
            return Salary;
        }
    }
}

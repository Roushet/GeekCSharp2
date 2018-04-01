using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1___workers
{
    class Freelancer : Worker
    {
        public float HourRate { get; set; }

        public override float CalculateSalary()
        {
            return 20.8f * 8f * HourRate;
        }
    }
}

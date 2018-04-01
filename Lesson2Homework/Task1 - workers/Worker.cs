using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1___workers
{
    /// <summary>
    /// Класс рабочего как вершина иерархии наследования, реализует интерфейс IComparable
    /// </summary>
    abstract class Worker : IComparable 
    {
        private string _FName;
        private string _SName;

        private float _WHours;
        private float _Salary;

        /// <summary>
        /// Метод для расчёта зарплаты
        /// </summary>
        /// <returns></returns>
        public abstract float CalculateSalary();

        /// <summary>
        /// Реализация интерфейса IComparable по полю фамилия
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            return SecondName.CompareTo(obj);
        }

        public string FirstName
        {
            get { return _FName; }
            set { _FName = value; }
        }
        public string SecondName
        {
            get { return _SName; }
            set { _SName = value; }
        }

        public float WorkingHours { get => _WHours; set => _WHours = value; }
        public float Salary { get => _Salary; set => _Salary = value; }


        public Worker()
        {
            FirstName = "";
            SecondName = "";
            Salary = 0f;
            WorkingHours = 0f;
        }

        public Worker(string firstName, string secondName, float salary, float workHours)
        {
            FirstName = firstName;
            SecondName = secondName;
            Salary = salary;
            WorkingHours = workHours;
        }

    }
}

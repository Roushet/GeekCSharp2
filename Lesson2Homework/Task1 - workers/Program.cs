using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task1___workers
{
    static class Program
    {

        //+ 1. Построить три класса(базовый и 2 потомка), описывающих некоторых работников с почасовой оплатой(один из потомков) и 
        //фиксированной оплатой(второй потомок).

        //+ а) Описать в базовом классе абстрактный метод для расчёта среднемесячной заработной платы. 
        //+ Для «повременщиков» формула для расчета такова: «среднемесячная заработная плата = 20.8 * 8 * почасовую ставку», 
        //+ для работников с фиксированной оплатой «среднемесячная заработная плата = фиксированной месячной оплате».

        //б) Создать на базе абстрактного класса массив сотрудников и заполнить его. -- не очень понял

        //в) + *Реализовать интерфейсы для возможности сортировки массива используя Array.Sort().  -- (сделал реализацию IComparable)

        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            Object[] workers = new Object[4];

            workers[0] = new Employee();
            workers[1] = new Employee();
            workers[2] = new Freelancer();
            workers[3] = new Freelancer();

        }
    }
}

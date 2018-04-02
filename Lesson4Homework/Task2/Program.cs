using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    class Program
    {

        //2. Дана коллекция List<T>, требуется подсчитать, сколько раз 
        //каждый элемент встречается в данной коллекции.

        //а) для целых чисел;
        //б) для обобщенной коллекции;
        //в)*используя Linq.

        static void Main(string[] args)
        {
            IEnumerable<int> en = Enumerable.Range(10, 1000);

            List<int> list = new List<int>(en);
            list.AddRange(list);

            //foreach(int i in list)
            //    Console.WriteLine("{0}", i);

            Console.WriteLine(list.Distinct().Count());

            int res = (from x in list
                       select x).Distinct().Count();

            Console.WriteLine(res);

            Console.ReadLine();

        }
    }
}

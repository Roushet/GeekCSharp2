using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//а) Свернуть обращение к OrderBy с использованием лямбда-выражения$
//б) * Развернуть обращение к OrderBy с использованием делегата Func<KeyValuePair<string, int>, int>.

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Задание
            Dictionary<string, int> dict = new Dictionary<string, int>()
                {
                    {"four",4 },
                    {"two",2 },
                    { "one",1 },
                    {"three",3 },
                };

            var d = dict.OrderBy(delegate (KeyValuePair<string, int> pair) { return pair.Value; });



            //foreach (var pair in d)
            //{
            //    Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            //}
            #endregion

            #region Решение
            //а) Свернуть обращение к OrderBy с использованием лямбда-выражения
            var lambda = dict.OrderBy(i => i.Value);
            Console.WriteLine("OrderBy LAMBDA");

            foreach (var entry in lambda)
            {
                Console.WriteLine("{0} - {1}", entry.Key, entry.Value);
            }

            //б) * Развернуть обращение к OrderBy с использованием делегата Func<KeyValuePair<string, int>, int>.

            Func<KeyValuePair<string, int>, int> func = GetValue;

            var del = dict.OrderBy(GetValue);
            Console.WriteLine("OrderBy FUNC delegate");

            foreach (var entry in del)
            {
                Console.WriteLine("{0} - {1}", entry.Key, entry.Value);
            }

            #endregion
            Console.ReadLine();
        }

        private static int GetValue(KeyValuePair<string, int> arg)
        {
            return arg.Value;
        }
    }
}

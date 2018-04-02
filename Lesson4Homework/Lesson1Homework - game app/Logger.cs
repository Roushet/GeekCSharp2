using System;
using System.Diagnostics;
using System.IO;

namespace GameApp
{
    static class Logger
    {
        private static int _logLenght = 100;

        private static LogEntry[] _log = new LogEntry[_logLenght];

        private static int cursor = 0;

        public static void LogAdd(LogEntry entry)
        {
            //если достигнут предел размера лога, сдвигает курсор в начало и перезаписывает вхождения там
            if (cursor >= _logLenght) cursor = 0;

            _log[cursor] = entry;
            cursor++;

            PrintMessage(entry);
        }

        private static void PrintMessage(LogEntry entry)
        {
            Debug.WriteLine(entry.ToString());
        }

        /// <summary>
        /// Выгружает лог в файл при закрытии приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void FlushLogToFile(object sender, EventArgs e)
        {
            using (StreamWriter sr = new StreamWriter("_log.log", true))
            {
                int temp = cursor;
                
                for(int i = 0; i<_logLenght; i++)
                {
                    if (_log[i] != null)
                        sr.WriteLine(_log[i]);
                }
            }
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp
{
    class LogEntry
    {
        private DateTime _date;
        private String _message;

        public LogEntry(DateTime date, String message)
        {
            _date = date;
            _message = message;
        }

        public override string ToString()
        {
            return _date.ToString() + ": " + _message.ToString();
        }
    }
}

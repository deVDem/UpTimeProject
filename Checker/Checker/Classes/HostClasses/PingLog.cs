using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Classes
{
    public class PingLog
    {
        public long Time;
        public string String;


        public PingLog(long time, string text)
        {
            Time = time;
            String = text;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Classes
{
    public class PingPoint
    {
        public long Time;
        public long Ping;

        public PingPoint(long time, long ping)
        {
            Time = time;
            Ping = ping;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Checker.Classes
{

    public class Host
    {
        public long id { get; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<PingPoint> PingPoints = new List<PingPoint>();
        public List<PingLog> pingLogs = new List<PingLog>();
        public Thread thread { get; }

        private bool stop = false;



        /// <summary>
        /// Использовать только с try/catch для ловли ошибок!
        /// </summary>
        /// <param name="id">ID хоста в БД</param>
        /// <param name="name">Имя хоста</param>
        /// <param name="address">Адрес хоста</param>
        /// <param name="PingPoints">Пинги</param>
        /// <param name="PingPoints">Логи ошибок</param>

        public Host(long id, string name, string address)
        {
            this.id = id;
            Name = name;
            Address = address;
            if (CheckHost())
            {
                thread = InitHost();
                thread.Start();
            }
            else
            {
                throw new Exception("Host not found.");
            }
        }

        private bool CheckHost()
        {
            bool pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(Address);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }

            return pingable;
        }

        private Thread InitHost()
        {
            Thread thread = new Thread(ThreadHost);

            return thread;
        }


        private void ThreadHost()
        {
            while (!stop)
            {
                bool pingable = false;
                Ping pinger = null;
                PingReply reply = null;
                DateTimeOffset timeOffset = DateTime.Now;
                long time = timeOffset.ToUnixTimeMilliseconds();

                try
                {
                    pinger = new Ping();
                    reply = pinger.Send(Address);
                    pingable = reply.Status == IPStatus.Success;
                }
                catch (PingException e)
                {
                    pingLogs.Add(new PingLog(time, e.Message));
                }
                finally
                {
                    if (pinger != null)
                    {
                        pinger.Dispose();
                    }
                }
                if (pingable)
                {
                    PingPoints.Add(new PingPoint(time, reply.RoundtripTime));
                }
                else
                {
                    PingPoints.Add(new PingPoint(time, -1));

                }
                Thread.Sleep(500);
            }
        }
    }
}

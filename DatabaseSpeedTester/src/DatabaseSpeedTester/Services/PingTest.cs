using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace DatabaseSpeedTester.Services
{
    public class PingTest
    {
        /// <summary>
        /// Returns an average time in ms of ping to specific host
        /// 
        /// Takes a host name and number of echos
        /// </summary>
        /// <param name="host"></param>
        /// <param name="echoNum"></param>
        /// <returns></returns>
        public static double PingTimeAverage(string host, int echoNum)
        {
            long totalTime = 0;
            Stopwatch watch = new Stopwatch();

            Task<IPAddress> ip = getIpFromHost(host);
            
            IPEndPoint ipep = new IPEndPoint(ip.Result, 1433);

            var sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sock.Blocking = true; // waits for result from socket

            sock.Connect(ipep); // wake server

            for (int i = 0; i < echoNum; i++)
            {
                sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sock.Blocking = true; // waits for result from socket

                watch.Start();
                sock.Connect(ipep);
                watch.Stop();

                totalTime += watch.ElapsedMilliseconds;
                watch.Reset();
            }
            return totalTime / echoNum;
        }

        /// <summary>
        /// Gets the ip address from a specific hosts dns name
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static async Task<IPAddress> getIpFromHost(string host)
        {
            IPAddress[] addresses = await Dns.GetHostAddressesAsync(host);
            return addresses[0];
        }
    }
}

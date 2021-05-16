using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var signalRConnection = new SignalRConnection();
            signalRConnection.Start();
            Console.ReadLine();
          
        }
    }
}

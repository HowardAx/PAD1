using System;

namespace Broker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Broker");

            BSocket bSocket = new BSocket();
            bSocket.Start("127.0.0.1", 9999);
            Console.ReadLine();
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Publisher");

            var publisherSocket = new PublisherSocket();
            publisherSocket.Connect("127.0.0.1", 9999);

            if (publisherSocket.isConnected)
            {
                while (true)
                {
                    var phandler = new PHandler();
                    Console.WriteLine("Enter the topic:");
                    phandler.Topic = Console.ReadLine().ToLower();
                    Console.WriteLine("Enter the message:");
                    phandler.Message = Console.ReadLine();
                    var loadString = JsonConvert.SerializeObject(phandler);

                    byte[] data = Encoding.UTF8.GetBytes(loadString);

                    publisherSocket.Send(data);
                }
            }



            Console.ReadLine();
        }
    }
}

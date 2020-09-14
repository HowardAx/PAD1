using System;

namespace Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, client!");

            string newsCategory;

            Console.WriteLine("Choose a news category to subscribe: ");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("News");
            Console.WriteLine("Sport");
            Console.WriteLine("Tech");
            Console.WriteLine("---------------------------------------------");
            newsCategory = Console.ReadLine().ToLower();
            
            Console.WriteLine("Successfully subscribed to " + newsCategory + "!");
            

            var subsSocket = new SSocket(newsCategory);

            subsSocket.connectToBroker("127.0.0.1", 9999);

            PExit();
            

        }


        public static void PExit()
        {
            Console.WriteLine("Press Q to exit");
            string input = Console.ReadLine().ToLower();
            string exit = "q";
            if (input.Equals(exit))
            {
                Environment.Exit(0);
            }
            else
            {
                PExit();
            }
        }
    }
}

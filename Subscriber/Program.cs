using System;

namespace Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, client!");

            string newsCategory;

            Console.Write("Enter news category: ");
            newsCategory = Console.ReadLine().ToLower();

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

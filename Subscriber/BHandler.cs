using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subscriber
{
    class BHandler
    {
        public static void Manage(byte[] messageBytes)
        {
            var messageString = Encoding.UTF8.GetString(messageBytes);
            var message = JsonConvert.DeserializeObject<PHandler>(messageString);

            Console.WriteLine($"Category: {message.newsCategory}");
            Console.WriteLine($"News body: {message.newsBody}");
        }
    }
}

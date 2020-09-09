using System;
using System.Collections.Generic;
using System.Text;

namespace Broker
{
    class BHandler
    {
        public static void Handle(byte[] payloadbytes, ConnInfo connInfo)
        {
            var payloadString = Encoding.UTF8.GetString(payloadbytes);
            Console.WriteLine(payloadString);
        }
    }
}

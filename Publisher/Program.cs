using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Xml;

namespace Publisher
{
    class Program
    {
        public static string title;
        public static string description;
        public static string url = "https://rss.publika.md/sport.xml";
        static void Main(string[] args)
        {
              
        Console.WriteLine("Publisher");

            var publisherSocket = new PublisherSocket();
            publisherSocket.Connect("127.0.0.1", 9999);

            if (publisherSocket.isConnected)
            {
                while (true)
                {
                    XmlDocument rssXmlDoc = new XmlDocument();

                    // Load the RSS file from the RSS URL
                    rssXmlDoc.Load(url);

                    // Parse the Items in the RSS file
                    XmlNodeList rssNodes = rssXmlDoc.SelectNodes("rss/channel/item");

                    var newsArr = new List<string>();

                    // Iterate through the items in the RSS file
                    int count = 0;
                    foreach (XmlNode rssNode in rssNodes)
                    {
                        count++;
                        XmlNode rssSubNode = rssNode.SelectSingleNode("title");
                        title = rssSubNode != null ? rssSubNode.InnerText : "";

                        rssSubNode = rssNode.SelectSingleNode("description");
                        description = rssSubNode != null ? rssSubNode.InnerText : "";

                        newsArr.Add( title + description);
                      
                        if (count == 3)
                        {
                            break;
                        }
                       
                    }


                    var phandler = new PHandler();
                    Console.WriteLine("Enter the topic:");
                    phandler.newsCategory = Console.ReadLine().ToLower();
                    //Console.WriteLine("Enter the message:");
                    foreach(string aux in newsArr)
                    {
                        phandler.newsBody = aux;
                        var loadString = JsonConvert.SerializeObject(phandler);

                        byte[] data = Encoding.UTF8.GetBytes(loadString);

                        publisherSocket.Send(data);
                    }
                       
                    
                    
                }
            }



            Console.ReadLine();
        }


        
       
        
    }

}

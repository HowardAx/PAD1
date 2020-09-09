using Broker;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Subscriber
{
    class SSocket
    {
        private Socket _socket;
        private string _newsCategory;

        public SSocket(string newsCategory)
        {
            _newsCategory = newsCategory;
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        }

        public void connectToBroker(string ipAddress, int port)
        {
            _socket.BeginConnect(new IPEndPoint(IPAddress.Parse(ipAddress), port), connectedCallBack, null);
            
        }

        private void connectedCallBack(IAsyncResult result)
        {
            if (_socket.Connected)
            {
                Console.WriteLine("Connection established.");
                SubscribeToNews();
                ReceiveData();
            }
            else
            {
                Console.WriteLine("Connection error.");
            }
        }

        private void ReceiveData()
        {
            ConnInfo connInfo = new ConnInfo();
            connInfo.Socket = _socket;
            _socket.BeginReceive(connInfo.Data, 0, connInfo.Data.Length, SocketFlags.None, 
                ReceivedCallBack, connInfo);
        }

        private void ReceivedCallBack(IAsyncResult result)
        {
            ConnInfo connInfo = result.AsyncState as ConnInfo;


            try
            {
                SocketError socketError;
                int socketSize = _socket.EndReceive(result, out socketError);

                if(socketError == SocketError.Success)
                {
                    byte[] messageBytes = new byte[socketSize];
                    Array.Copy(connInfo.Data, messageBytes, messageBytes.Length);

                    BHandler.Manage(messageBytes);
                
                }
            } catch(Exception e)
            {
                Console.WriteLine($"{e.Message}");
            }
            finally
            {
                try
                {
                    connInfo.Socket.BeginReceive(connInfo.Data, 0, connInfo.Data.Length,
                        SocketFlags.None, ReceivedCallBack, connInfo);
                } catch(Exception e)
                {
                    Console.WriteLine($"{e.Message}");
                    connInfo.Socket.Close();
                    Thread.Sleep(3000);
                    Environment.Exit(0);
                }
            }
        }

        private void SubscribeToNews()
        {
            var newsCat = Encoding.UTF8.GetBytes("#" + _newsCategory);

            try
            {
                _socket.Send(newsCat);
            } catch(Exception e)
            {
                Console.WriteLine($"{e.Message}");
            }

        }
    }
}

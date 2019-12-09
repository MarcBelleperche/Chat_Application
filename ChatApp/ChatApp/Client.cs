using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp
{
    public class Client
    {
        public TcpClient _tcpclient;
        public Channel channel;

        public Client(TcpClient client, Channel channel)
        {
            this.channel = channel;
            this._tcpclient = client;
        }
    }
}

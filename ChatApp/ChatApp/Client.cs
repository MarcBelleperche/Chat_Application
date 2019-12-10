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
        public Channels active_channels;

        public Client(TcpClient client)
        {
            this.active_channels = new Channels();
            this._tcpclient = client;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace Chat_Server
{
    [Serializable]
    public class Client
    {
        public string _name;
        public string _password;
        public Channel _currentchannel;
        public Channels _clientschannels;
        public Private _currentprivate;
        public Privates _clientsprivate;
        IPAddress ip;

        public Client(string name, string psw)
        {
            _clientschannels = new Channels();
            _clientsprivate = new Privates();

            this._name = name;
            this._password = psw;
           /* foreach (Channel c in _clientschannels._channels)
            {
                Console.WriteLine(c._name);
                //if (c._name == "defaut") this._currentchannel = c;
            }*/

            this.ip = IPAddress.Parse("127.0.0.1");

        }

        public IPAddress GetIp()
        {
            return this.ip;
        }


    }
}
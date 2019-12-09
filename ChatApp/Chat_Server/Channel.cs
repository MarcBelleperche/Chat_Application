using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Server
{
    [Serializable]
    public class Channel
    {
        public string _name;
        public int _port;
        public bool _locked;

        public Channel(string name, int port)
        {
            this._name = name;
            this._port = port;
        }
    }
}

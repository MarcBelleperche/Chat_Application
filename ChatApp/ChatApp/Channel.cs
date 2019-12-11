using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatApp
{
    [Serializable]
    public class Channel
    {


        public string _name;
        public bool _locked;

        public Channel(string name)
        {
            this._name = name;
        }

    }
}

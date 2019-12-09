using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Server
{
    [Serializable]
    public class Channels
    {
        public List<Channel> _channels;
        Channel defaut;
        Channel nitro;

        public Channels()
        {
            _channels = new List<Channel>();
            _channels.Add(defaut = new Channel("default", 5000));
            _channels.Add(nitro = new Channel("Nitro Gaming", 5001));
        }
    }
}

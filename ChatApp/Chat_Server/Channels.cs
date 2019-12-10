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
        public Channels()
        {
            _channels = new List<Channel>();
            _channels.Add(new Channel("Default"));
            _channels.Add(new Channel("Nitro Gaming"));
        }
    }
}

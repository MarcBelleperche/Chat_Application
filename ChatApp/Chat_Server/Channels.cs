using System;
using System.Collections.Generic;

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
        }
    }
}

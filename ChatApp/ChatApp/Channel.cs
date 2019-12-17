using System;
using System.Windows.Forms;

namespace ChatApp
{
    [Serializable]
    public class Channel
    {
        public string _name;
        public bool _locked;
        public string _channel_text;

        public Channel(string name)
        {
            this._name = name;
            this._channel_text = "Welcome on the "+_name+" channel" + Environment.NewLine;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp
{
    public class Private
    {
        public String _text;
        public List<Client> _two_concerned;

        public Private(Client first, Client second)
        {
            this._two_concerned = new List<Client>();
            this._two_concerned.Add(first);
            this._two_concerned.Add(second);
        }
        
    }
}

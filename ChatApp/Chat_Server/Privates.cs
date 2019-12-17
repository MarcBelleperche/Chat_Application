using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Server
{
    [Serializable]

    public class Privates
    {
        public List<Private> _private_list;

        public Privates()
        {
            this._private_list = new List<Private>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat_Server
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
            password.PasswordChar = '\u25CF';
            psw.PasswordChar = '\u25CF';
        }

        private void register_but_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("Enter your name");
            //string name = Console.ReadLine();
            string name = username.Text;
            //Console.WriteLine("Enter your password");
            //string psw = Console.ReadLine();
            string psws = password.Text;
            string pswconfirm = psw.Text;

            if (psws == pswconfirm)
            {
                Client client = new Client(name, psws);

                //Clients clients = Clients.deserialize();
                Clients clients = new Clients();
                clients._clients.Add(client);

                clients.serialize(clients);
                this.Dispose();
            }
            //if (password.StringValue == passwordcheck.StringValue) {
            //    Clients addlcient = new Clients();
            //    addlcient._clients.Add(new Client(username.StringValue, null));
            //    addlcient.serialize(addlcient);
            //}

        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Chat_Server
{
    [Serializable]
    public class Clients
    {
        public List<Client> _clients;

        public Clients()
        {
/*            this._clients = new List<Client>();
            _clients.Add(new Client("Marc", "marc"));*/
        }

        public void serialize(Clients clients)
        {
            IFormatter formatter = new BinaryFormatter();
            //We create a new file called "Clients.bin"
            FileStream stream = new FileStream("Clients.bin", FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, clients);
            stream.Close();
        }

        public Clients deserialize()
        {
            IFormatter formatter = new BinaryFormatter();
            //We call the .bin file 
            FileStream stream = new FileStream("Clients.bin", FileMode.Open, FileAccess.Read);
            Clients c = new Clients();
            c = (Clients)formatter.Deserialize(stream);
            stream.Close();
            return c;
        }

        public Client checkclients(string username, string psw, Label print)
        {
            Client current = null;
            int find = 0;
            foreach (Client clients in _clients)
            {
                if (clients._name == username)
                {
                    if (clients._password == psw)
                    {
                        find = 1;
                        current = clients;
                        break;
                    }
                    else
                    {
                        find = 2;
                    }
                }
                else
                {
                    find = 3;
                }
            }

            if (find == 1)
            {
                print.ForeColor = Color.White;
                //Console.WriteLine("Your connected");
                print.Text = "You are connected";
            }
            else if (find == 2)
            {
                print.ForeColor = Color.Red;
                //Console.WriteLine("Wrong password !!!");
                print.Text = "Wrong password !";

            }
            else if (find == 3)
            {
                print.ForeColor = Color.Red;
                //Console.WriteLine("You're not registed !!!");
                print.Text = "You are not registered !";
            }

            return current;
        }


    }
}
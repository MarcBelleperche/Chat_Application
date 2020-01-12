using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat_Server
{
    public partial class ChatWindow : Form
    {
        NetworkStream ns;
        TcpClient client;
        Thread thread;
        Client current;
        List<string> gchannels;
        List<string> gclients;

        // Classique constructor useless here
        public ChatWindow()
        {
            InitializeComponent();
        }

        //Used constructor
        public ChatWindow(Client current)
        {
           
            InitializeComponent();

            info.Text = current._name;
            rt_chat_text.Enabled = false;
            rt_chat_text.ForeColor = Color.Black;

            //Initialisation of windows component for a good way to work
            InitCheck();                

            this.current = current;

            //Etablising the connection 
            this.client = Initialize(current);

            LauchingConnection(this.client);
            
        }

        private void LauchingConnection(TcpClient client)
        {
            this.ns = client.GetStream();

            //I need to use a lamba in order to start a thread with a parameter
            Thread thread = new Thread(o => ReceiveData((TcpClient)o));
            this.thread = thread;

            //I start my thread
            thread.Start(client);

        }
        private TcpClient Initialize(Client current)
        {
            //Return a TCP and still initialyze the client channels.

            TcpClient client;
            current._currentchannel = current._clientschannels._channels[0];
            Console.WriteLine(current.GetIp());


             foreach (Channel c in current._clientschannels._channels)
             {
                     channels_list.Items.Add(c._name);
                     Console.WriteLine(c._name);
             }

            client = new TcpClient();

            // Try to lauch the connection with the server with the port 5000 and the Ip Address 127.0.0.1
            try
            {
                client.Connect(current.GetIp(), 5000);
                GetServerChannels(client);
                GetServerClient(client);
                NetworkStream ns = client.GetStream();
                this.ns = ns;
                //SendMessage("newbie", current._name.ToUpper(), ns);
                SendMessages("newbie", ns);
                SendMessages(current._name.ToUpper(),ns);
            }
            catch (SocketException)
            {
                Console.WriteLine("Channel do not exist");
            }

            return client;
        }


        // This function is used to get all the channels initialised on the server
        public void GetServerChannels(TcpClient client)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            gchannels = (List<string>)formatter.Deserialize(client.GetStream());
            foreach (String name in gchannels)
            {
                channels_to_select.Items.Add(name);
            }
        }

        public void GetServerClient(TcpClient client)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            gclients = (List<string>)formatter.Deserialize(client.GetStream());
            foreach (String name in gclients)
            {
                if (name == null) break;
                else client_to_connect.Items.Add(name);
            }
        }

        void ReceiveData(TcpClient client)
        {
            NetworkStream ns = client.GetStream();

            byte[] datarecieved = new byte[1024];
            int byte_count;

            string channel = "";

            int chan = 0;

            while ((byte_count = ns.Read(datarecieved, 0, datarecieved.Length)) > 0)
            {

                string data = Encoding.ASCII.GetString(datarecieved, 0, byte_count);
                //Console.Write(Encoding.ASCII.GetString(datarecieved, 0, byte_count));

                if (chan == 0)
                {
                    channel = data;
                    Console.WriteLine("Message pret a etre ecrit sur le channel : "+channel);
                    chan++;
                }
                else if (chan == 1)
                {
                    string action_to_do = channel.Trim();
                    Console.WriteLine("ACTION TO DO :"+action_to_do);
                    if (Equals(action_to_do, "newbie") == true)
                    {
                        //client_to_connect.Items.Add(data.Trim());
                        Console.WriteLine("CLient to connect add "+ data.Trim());
                        Appendnewprivate(data.Trim());
                        chan = 0;
                    }

                    else if (Equals(action_to_do, "get_text") == true)
                    {
                       
                        SetText(data.Trim(), false);
                        chan = 0;
                    }

                    else if (Equals(action_to_do, "get_ptext") == true)
                    {

                        SetText(data.Trim(), false);
                        chan = 0;
                    }

                    else
                    {
                        Console.WriteLine("Le channel courant est :" + current._currentchannel._name);
                        Console.WriteLine("Le channel OU prenom a comparé est : " + channel);

                        //Console.WriteLine((Equals(current._currentchannel._name, "Default")));

                        channel = channel.Trim();
                        if (acces_channel.Checked) {
                            if (Equals(channel, current._currentchannel._name) == true)
                            {
                                Console.WriteLine("I am in ...");
                                this.AppendText(data, false);
                            }
                            else
                            {
                                bool notachannel = true;
                                foreach (String names in gchannels)
                                {
                                    if (names == channel) notachannel = false;
                                }
                                Console.WriteLine("I got that far");
                                if (notachannel == true)
                                {
                                    if (current._clientsprivate._private_list.Count == 0) AddClient(channel, false);
                                    else
                                    {
                                        string channeltoadd = null;
                                        foreach (Private p in current._clientsprivate._private_list)
                                        {
                                            if (channel != p._name && channel != current._name.ToUpper())
                                            {
                                                //private_list.Items.Add(channel);
                                                channeltoadd = channel;

                                            }
                                        }
                                        if (channeltoadd != null) AddClient(channeltoadd, false);
                                    }
                                }
                                
                            }
                        }

                        else if (acces_private.Checked) {
                            //Console.WriteLine("Le nom de l'ecrivain : "+ channel +" la conversation selectionne : "+current._currentprivate._name);
                            if (Equals(channel.Trim(), current._currentprivate._name) == true)
                            {
                                Console.WriteLine("Got it ...");
                                this.AppendText(data, false);
                            }
                            else
                            {
                                bool notachannel = true;
                                foreach (String names in gchannels)
                                {
                                    if (names == channel) notachannel = false;
                                }
                                Console.WriteLine("I got that far");
                                if (notachannel == true)
                                {
                                    if (current._clientsprivate._private_list.Count == 0) AddClient(channel, false);
                                    else
                                    {
                                        string channeltoadd = null;
                                        foreach (Private p in current._clientsprivate._private_list)
                                        {
                                            if (channel != p._name && channel != current._name.ToUpper())
                                            {
                                                channeltoadd = channel;
                                                //private_list.Items.Add(channel);
                                            }
                                        }
                                        AddClient(channeltoadd, false);

                                    }
                                }
                            }
                        }
                        chan = 0;

                    }
                }
               
            }
      
        }

        //-------------------------6 Delegates functions that i needed to use in my thread to access windows components -----------
        public void AppendText(string what, bool debug = false)
        {
            if (debug)
                return;
            if (this.InvokeRequired)
            {
                this.Invoke(
                    new MethodInvoker(
                    delegate () { AppendText(what); }));
            }
            else
            {
                DateTime timestamp = DateTime.Now;
                rt_chat_text.AppendText(timestamp.ToLongTimeString() + "\t" + what + Environment.NewLine);
            }
        }

        public void AddClient(string what, bool debug = false)
        {
            if (debug)
                return;
            if (this.InvokeRequired)
            {
                this.Invoke(
                    new MethodInvoker(
                    delegate () { AddClient(what); }));
            }
            else
            {
                private_list.Items.Add(what);
                current._clientsprivate._private_list.Add(new Private(what));
                client_to_connect.Items.Remove(what);
            }
        }

        public void SetText(string what, bool debug = false)
        {
            if (debug)
                return;
            if (this.InvokeRequired)
            {
                this.Invoke(
                    new MethodInvoker(
                    delegate () { SetText(what); }));
            }
            else
            {
                rt_chat_text.Clear();
                rt_chat_text.AppendText(what + Environment.NewLine + Environment.NewLine);
            }
        }
        public void Appendnewprivate(string what, bool debug = false)
        {
            if (debug)
                return;
            if (this.InvokeRequired)
            {
                this.Invoke(
                    new MethodInvoker(
                    delegate () { Appendnewprivate(what); }));
            }
            else
            {
                client_to_connect.Items.Add(what);
            }
        }

        //--------------------------------------------------------------------------------------------------------



            // Send function usind the buffer first then the client stream to write on it 
        private void send_Click(object sender, EventArgs e)
        {
            if (send_message.Text == "exit")
            {
                SendMessages("exit", ns);
                SendMessages((string)channels_list.SelectedItem,ns);
                channels_list.Items.Remove(channels_list.SelectedItem);
                foreach(Channel c in current._clientschannels._channels)
                {
                    if (c._name == (string)channels_list.SelectedItem)
                    {
                        current._clientschannels._channels.Remove(c);

                    }
                }
                /*client.Client.Shutdown(SocketShutdown.Send);
                thread.Join();
                ns.Close();
                client.Close();*/
                Console.WriteLine("disconnect from server!!");
            }

            else
            {
                if (acces_channel.Checked) {
                    string current_channel_name = current._currentchannel._name;
                    string print = current._name.ToUpper() + " : " + send_message.Text + "\n";
                    SendMessages("msg", ns);
                    SendMessages(current_channel_name,ns);
                    SendMessages(print, ns);

                    this.AppendText(print, false);
                }

                else if (acces_private.Checked)
                {
                    string current_private_name = current._currentprivate._name;
                    string print = current._name.ToUpper() + " : " + send_message.Text + "\n";

                    SendMessages("pmsg", ns);
                    SendMessages(current_private_name, ns);
                    SendMessages(print, ns);

                    this.AppendText(print, false);
                }
               

            }
        }

        //Function doing basically the same thing that the one above but i use it for spleciale requests

        private void SendMessages(string data, NetworkStream ns)
        {
            byte[] databuffer = Encoding.ASCII.GetBytes(data);

            ns.Write(databuffer, 0, databuffer.Length);
            System.Threading.Thread.Sleep(10);

        }

        private void rt_chat_text_TextChanged(object sender, EventArgs e)
        {

        }

        private void private_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            private_list.Enabled = true;
            channels_list.Enabled = false;
            string current_private = (String)private_list.SelectedItem;
            Console.WriteLine(current_private);
            foreach (Private p in current._clientsprivate._private_list)
            {
                if (current_private == p._name)
                {
                    current._currentprivate = p;
                    Console.WriteLine("Current changed to : " + current._currentprivate._name);

                    SendMessages("get_ptext", ns);
                    SendMessages(current._currentprivate._name, ns);
                }
            }


        }

        private void channels_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            private_list.Enabled = false;
            channels_list.Enabled = true;
            string current_channel = (String)channels_list.SelectedItem;
            Console.WriteLine(current_channel);
            foreach(Channel c in current._clientschannels._channels)
            {
                if (current_channel == c._name)
                {
                    current._currentchannel = c;
                    Console.WriteLine("Current changed to : "+current._currentchannel._name);
                    SendMessages("get_text", ns);
                    SendMessages(current._currentchannel._name, ns);
                }
            }            
        }
        private void channels_list_Click(object sender, EventArgs e)
        {
            private_list.Enabled = false;
            channels_list.Enabled = true;
        }


        private void private_list_Click(object sender, EventArgs e)
        {
            private_list.Enabled = true;
            channels_list.Enabled = false;
        }


        private void channels_to_select_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void Connect_new_channel_Click(object sender, EventArgs e)
        {
            NetworkStream ns = client.GetStream();
            if (check_channel.Checked) 
            {
                SendMessages("conn", ns);
                string new_connexion_channel = (String)channels_to_select.SelectedItem;
  
                SendMessages(new_connexion_channel, ns);


                channels_list.Items.Add(new_connexion_channel);
                current._clientschannels._channels.Add(new Channel(new_connexion_channel));
                channels_to_select.Items.Remove(new_connexion_channel);
            }

            else if (check_private.Checked)
            {
                SendMessages("pconn", ns);
                string new_connexion_private = (String)client_to_connect.SelectedItem;

                SendMessages(new_connexion_private, ns);

                private_list.Items.Add(new_connexion_private);
                current._clientsprivate._private_list.Add(new Private(new_connexion_private));
                client_to_connect.Items.Remove(new_connexion_private);
            }
        }

        private void check_private_CheckedChanged(object sender, EventArgs e)
        {
            if (check_private.Checked == true)
            {
                channels_to_select.Enabled = false;
                client_to_connect.Enabled = true;
            }
        }


        private void check_channel_CheckedChanged(object sender, EventArgs e)
        {
            if (check_channel.Checked == true)
            {
                client_to_connect.Enabled = false;
                channels_to_select.Enabled = true;
            }
        }

        private void InitCheck()
        {
            check_channel.Checked = true;
            acces_channel.Checked = true;
        }

        private void client_to_connect_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void acces_channel_CheckedChanged(object sender, EventArgs e)
        {
            if (acces_channel.Checked)
            {
                acces_private.Checked = false;
                channels_list.Enabled = true;
                private_list.Enabled = false;
            }
        }

        private void acces_private_CheckedChanged(object sender, EventArgs e)
        {
            if (acces_private.Checked)
            {
                acces_channel.Checked = false;
                private_list.Enabled = true;
                channels_list.Enabled = false;
            }
        }
    }
}

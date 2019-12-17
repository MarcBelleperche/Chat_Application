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


        //static readonly Dictionary<String, TcpClient> active_channels = new Dictionary<String, TcpClient>();

        public ChatWindow()
        {
            InitializeComponent();
        }

        public ChatWindow(Client current)
        {
           
            InitializeComponent();
            InitCheck();
            //RichInit();

            this.current = current;

            this.client = Initialize(current);

            //current._currentchannel = current._clientschannels._channels[0];


            //this.client = active_channels.Values.ElementAt(0);

            LauchingConnection(this.client);

            //Console.WriteLine("Flag 1");

            
        }

        private void LauchingConnection(TcpClient client)
        {
            this.ns = client.GetStream();
            

           /* StreamWriter sW = new StreamWriter(client.GetStream());
            sW.AutoFlush = true;*/

            // StreamReader sR = new StreamReader(client.GetStream());
            //Console.WriteLine("Flag 2");

            //Console.WriteLine("Select the channel on wich you want to communicate");

            //string channel = Console.ReadLine();
            //Send the channel selected
            //sW.WriteLine(channel);
            // Send the username

            //sW.WriteLine(current._name);

            Thread thread = new Thread(o => ReceiveData((TcpClient)o));


            this.thread = thread;
            //Console.WriteLine("Flag 3");


            thread.Start(client);
            //Console.WriteLine("Flag 4");

            //Console.ReadKey();
        }
        private TcpClient Initialize(Client current)
        {
            TcpClient client;
            current._currentchannel = current._clientschannels._channels[0];
            //Console.WriteLine(5000);
            Console.WriteLine(current.GetIp());


             foreach (Channel c in current._clientschannels._channels)
             {
                     //active_channels.Add(c._name, client);
                     channels_list.Items.Add(c._name);
                     Console.WriteLine(c._name);
             }

            client = new TcpClient();

            try
            {
                client.Connect(current.GetIp(), 5000);
                ////active_channels.Add(c._name, client);
                GetServerChannels(client);
                NetworkStream ns = client.GetStream();
                this.ns = ns;
                SendMessage("newbie", current._name.ToUpper(), ns);
            }
            catch (SocketException)
            {
                Console.WriteLine("Channel do not exist");
            }

            return client;
        }

        public void GetServerChannels(TcpClient client)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            List<string> channels = (List<string>)formatter.Deserialize(client.GetStream());
            foreach (String name in channels)
            {
                channels_to_select.Items.Add(name);
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
                        //client_to_connect.Items.Add(data.Trim());
                        //Console.WriteLine("Setting text " + data.Trim());
                        SetText(data.Trim(), false);
                        chan = 0;
                    }

                    /*                    else if(Equals(action_to_do, "get_text") == true)
                                        {
                                            //rt_chat_text.Text = data;
                                            this.AppendText(data, false);
                                        }
                    */
                    else
                    {
                        Console.WriteLine("Le channel courant est :" + current._currentchannel._name);
                        Console.WriteLine("Le channel a comparé est : " + channel);
                        //Console.WriteLine((Equals(current._currentchannel._name, "Default")));
                        channel = channel.Trim();
                        if (Equals(channel, current._currentchannel._name) == true)
                        {
                            Console.WriteLine("I am in ...");
                            this.AppendText(data, false);
                        }

                        else if (Equals(channel, current._currentprivate)==true)
                        {
                            Console.WriteLine("Got it ...");
                            this.AppendText(data, false);
                        }

                        chan = 0;
                    }
                }
               
            }
      
        }

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
                //DateTime timestamp = DateTime.Now;
                //rt_chat_text.AppendText(timestamp.ToLongTimeString() + "\t" + what + Environment.NewLine);
                client_to_connect.Items.Add(what);
            }
        }

        private void send_Click(object sender, EventArgs e)
        {
            if (send_message.Text == "EXIT")
            {
                client.Client.Shutdown(SocketShutdown.Send);
                thread.Join();
                ns.Close();
                client.Close();
                Console.WriteLine("disconnect from server!!");
            }

            else
            {
                string action = "msg";
                byte[] action_buffer = Encoding.ASCII.GetBytes(action);


                string current_channel_name = current._currentchannel._name;
                string print = current._name.ToUpper() + " : " + send_message.Text + "\n";
                byte[] buffer = Encoding.ASCII.GetBytes(current_channel_name + "," + print);


                ns.Write(action_buffer, 0, action_buffer.Length);
                //ns.Flush();
                ns.Write(buffer, 0, buffer.Length);
                //ns.Flush();

                //string print = current._name.ToUpper() + " : " + send_message.Text + "\n";
                this.AppendText(print, false);

                //rt_chat_text.Text = rt_chat_text.Text  + current._name.ToUpper()+" : "+ send_message.Text + "\n";
                //byte[] check = new byte[1024];

                //string action = "msg";

            }
        }

        private void SendMessage(string action, string data, NetworkStream ns)
        {
            byte[] action_buffer = Encoding.ASCII.GetBytes(action+",");

            byte[] buffer = Encoding.ASCII.GetBytes(data);


            ns.Write(action_buffer, 0, action_buffer.Length);
            //ns.Flush();
            ns.Write(buffer, 0, buffer.Length);
            //ns.Flush();

            //string print = current._name.ToUpper() + " : " + send_message.Text + "\n";

            //rt_chat_text.Text = rt_chat_text.Text  + current._name.ToUpper()+" : "+ send_message.Text + "\n";
        }

        private void rt_chat_text_TextChanged(object sender, EventArgs e)
        {

        }

        private void channels_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            string current_channel = (String)channels_list.SelectedItem;
            Console.WriteLine(current_channel);
            foreach(Channel c in current._clientschannels._channels)
            {
                if (current_channel == c._name)
                {
                    current._currentchannel = c;
                    Console.WriteLine("Current changed to : "+current._currentchannel._name);
                    SendMessage("get_text", current._currentchannel._name, ns);

                     //SendMessage("gct",c._name,ns);
                     /*BinaryFormatter formatter = new BinaryFormatter();
                     rt_chat_text = (RichTextBox)formatter.Deserialize(client.GetStream());*/
                }
            }            
        }

        private void channels_to_select_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Connect_new_channel_Click(object sender, EventArgs e)
        {
            NetworkStream ns = client.GetStream();
            if (check_channel.Checked) 
            {
                string action = "conn";
                byte[] action_buffer = Encoding.ASCII.GetBytes(action);
                ns.Write(action_buffer, 0, action_buffer.Length);
                //ns.Flush();

                string new_connexion_channel = (String)channels_to_select.SelectedItem;
                byte[] new_channel = Encoding.ASCII.GetBytes(new_connexion_channel);
                ns.Write(new_channel, 0, new_channel.Length);
                //ns.Flush();

                channels_list.Items.Add(new_connexion_channel);
                current._clientschannels._channels.Add(new Channel(new_connexion_channel));
                channels_to_select.Items.Remove(new_connexion_channel);
            }

            else if (check_private.Checked)
            {
                string action = "pconn";
                byte[] action_buffer = Encoding.ASCII.GetBytes(action);
                ns.Write(action_buffer, 0, action_buffer.Length);
                //ns.Flush();

                string new_connexion_private = (String)client_to_connect.SelectedItem;
                byte[] new_private = Encoding.ASCII.GetBytes(new_connexion_private);
                ns.Write(new_private, 0, new_private.Length);
                //ns.Flush();

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
        }

        private void client_to_connect_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void RichInit()
        {
            string test = "YOUYOU" + Environment.NewLine + "YOUYOU2"+Environment.NewLine;
            rt_chat_text.Text = test;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
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


        static readonly Dictionary<String, TcpClient> active_channels = new Dictionary<String, TcpClient>();

        public ChatWindow()
        {
            InitializeComponent();
        }

        public ChatWindow(Client current)
        {
           
            InitializeComponent();

            this.current = current;

            InitializeChannels(current);


            this.client = active_channels.Values.ElementAt(0);

            LauchingConnection(this.client);

            //Console.WriteLine("Flag 1");

            
        }

        private void LauchingConnection(TcpClient client)
        {
            this.ns = client.GetStream();

            StreamWriter sW = new StreamWriter(client.GetStream());
            sW.AutoFlush = true;

            // StreamReader sR = new StreamReader(client.GetStream());
            //Console.WriteLine("Flag 2");

            //Console.WriteLine("Select the channel on wich you want to communicate");

            //string channel = Console.ReadLine();
            //Send the channel selected
            //sW.WriteLine(channel);
            // Send the username

            sW.WriteLine(current._name);

            Thread thread = new Thread(o => ReceiveData((TcpClient)o));


            this.thread = thread;
            //Console.WriteLine("Flag 3");


            thread.Start(client);
            //Console.WriteLine("Flag 4");

            //Console.ReadKey();
        }
        private void InitializeChannels(Client current)
        {
            int count = 0;
            foreach (Channel c in current._clientschannels._channels)
            {
                TcpClient client = new TcpClient();
                current._currentchannel = current._clientschannels._channels[0];
                Console.WriteLine(current._currentchannel._port);
                Console.WriteLine(current.GetIp());

                try
                {
                    client.Connect(current.GetIp(), current._currentchannel._port);
                    active_channels.Add(c._name, client);
                    channels_list.Items.Add(c._name);
                    count++;
                    Console.WriteLine(c._name);
                }
                catch (SocketException)
                {
                    Console.WriteLine("Channel do not exist");
                }
               
            }
        }

         void ReceiveData(TcpClient client)
        {
            NetworkStream ns = client.GetStream();
            byte[] receivedBytes = new byte[1024];
            int byte_count;

            while ((byte_count = ns.Read(receivedBytes, 0, receivedBytes.Length)) > 0)
            {
                string msg = Encoding.ASCII.GetString(receivedBytes, 0, byte_count);
                Console.Write(Encoding.ASCII.GetString(receivedBytes, 0, byte_count));
                this.AppendText(msg, false);

                //rt_chat_text.Text = msg;
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
                string print = current._name.ToUpper() + " : " + send_message.Text + "\n";
                byte[] buffer = Encoding.ASCII.GetBytes(print);
                //byte[] bufferuser = Encoding.ASCII.GetBytes(current._name);
                //ns.Write(bufferuser, 0, bufferuser.Length);
                ns.Write(buffer, 0, buffer.Length);
                //string print = current._name.ToUpper() + " : " + send_message.Text + "\n";
                this.AppendText(print, false);
                //rt_chat_text.Text = rt_chat_text.Text  + current._name.ToUpper()+" : "+ send_message.Text + "\n";
            }
        }

        private void rt_chat_text_TextChanged(object sender, EventArgs e)
        {

        }

        private void channels_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            int count = 0;
            string current_channel = (String)channels_list.SelectedItem;
            Console.WriteLine(current_channel);
            /* foreach(Channel channel in current._clientschannels._channels)
             {
                 if (current_channel == channel._name)
                 {
                     break;
                 }
                 else count++;
             }*/

            foreach (String name in active_channels.Keys)
            {
                if (current_channel == name) 
                {
                    client = active_channels[name];
                    LauchingConnection(client);
                }
            }
        }
    }
}

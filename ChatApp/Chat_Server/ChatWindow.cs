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


        //static readonly Dictionary<String, TcpClient> active_channels = new Dictionary<String, TcpClient>();

        public ChatWindow()
        {
            InitializeComponent();
        }

        public ChatWindow(Client current)
        {
           
            InitializeComponent();

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

            StreamWriter sW = new StreamWriter(client.GetStream());
            sW.AutoFlush = true;

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
            Console.WriteLine(5000);
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
            }
            catch (SocketException)
            {
                Console.WriteLine("Channel do not exist");
            }

            return client;
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
                    Console.WriteLine("Le channel courant est :" + current._currentchannel._name);
                    if (channel == current._currentchannel._name)
                    { this.AppendText(data, false); }
                        chan = 0;
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
                string current_channel_name = current._currentchannel._name;
                byte[] bufferchannel = Encoding.ASCII.GetBytes(current_channel_name);

                string print = current._name.ToUpper() + " : " + send_message.Text + "\n";
                byte[] buffer = Encoding.ASCII.GetBytes(print);

                //byte[] bufferuser = Encoding.ASCII.GetBytes(current._name);
                //ns.Write(bufferuser, 0, bufferuser.Length);
                ns.Write(bufferchannel, 0, bufferchannel.Length);

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
            string current_channel = (String)channels_list.SelectedItem;
            Console.WriteLine(current_channel);
            foreach(Channel c in current._clientschannels._channels)
            {
                if (current_channel == c._name)
                {
                    current._currentchannel = c;
                }
            }            
        }
    }
}

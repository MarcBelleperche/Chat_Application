using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatApp
{
    public partial class Server_info : Form
    {

        static readonly object _lock = new object();
        //Creating a dictionnay to stock clients accordings to their id
        static readonly Dictionary<int, Client> list_clients = new Dictionary<int, Client>();

        public enum actions { newbie, get_text ,get_ptext, msg,  pmsg, conn, pconn };

        Channels server_channels;

        public Server_info()
        {

            InitializeComponent();

            server_channels = new Channels();
            server_channels._channels.Add(new Channel("Nine Gag"));
            server_channels._channels.Add(new Channel("Nitro Gaming"));
            server_channels._channels.Add(new Channel("C# broke my brain"));
            server_channels._channels.Add(new Channel("ITF PROMO 2021"));


            Connection(5000);

        }

        public void Connection(int port)
        {
            int count = 1;

            //Initialize server sockets and listener waiting for client(s)
            IPAddress.Parse("127.0.0.1");
            TcpListener ServerSocket = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
            ServerSocket.Start();


            //While loop for the server wait for clients over and over
            while (true)
            {
                Client client = new Client(ServerSocket.AcceptTcpClient());
                //TcpClient client = ServerSocket.AcceptTcpClient();

                //Adding the lock to the client
                lock (_lock) list_clients.Add(count, client);
                Console.WriteLine("Client number : " + count + " connected!!");

                // ATTENTION MOFIF ------------------------------------------------------------------------------------
                // HERE I SEND ALL THE SERVER ACCESSIBLE CHANNEL
                List<string> string_server_channels = Convert_to_string();

                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(client._tcpclient.GetStream(), string_server_channels);

                //-----------------------------------------------------------------------------------------------------
                // HERE I SEND ALL THE CONNECTED CLIENT TO THE NEW ONE
                List<string> string_server_clients = Convert_client_to_string();
                BinaryFormatter bfs = new BinaryFormatter();
                bfs.Serialize(client._tcpclient.GetStream(), string_server_clients);

                //New thread for the connected client 
                Thread t = new Thread(handle_clients);
                t.Start(count);
                count++;
            }

        }

        // FUNCTIONS USED TO SEND THE LISTS TO THE CLIENT (Clients and Connected clients)
        public List<string> Convert_to_string()
        {
            List<string> stringlist = new List<string>();
            foreach (Channel c in server_channels._channels)
            {
                stringlist.Add(c._name);
            }
            return stringlist;
        }

        public List<string> Convert_client_to_string()
        {
            List<string> stringlist = new List<string>();
            foreach (Client c in list_clients.Values)
            {
                stringlist.Add(c._name);
            }
            return stringlist;
        }

        //--------------------------------------------------------------------------------

        // THREAD CREATED FOR EACH CLIENT TO RECEIVE AND GO THROUGH THE DATA THE SERVER RECEIVES
        public void handle_clients(object o)
        {
            int id = (int)o;
            TcpClient client;


            lock (_lock) client = list_clients[id]._tcpclient;



            while (true)
            {
                NetworkStream stream = client.GetStream();

                string action = Receivedata(stream);

                Console.WriteLine(action);

                //Switch case used to execute different types of action due to the pre message it received

                switch (action)
                {
                    case "newbie":
                        Console.WriteLine("I am adding a new client");
                        string newbiename = Receivedata(stream);
                        list_clients[id]._name = newbiename;
                        addnewbie(newbiename, client);
                        break;
                        
                    case "get_text":
                        string data = Receivedata(stream);
                        Console.WriteLine("Le texte du channel :" + data + " va etre transmit");
                        foreach (Channel sc in list_clients[id].active_channels._channels)
                        {
                            if (sc._name == data)
                            {
                                Console.WriteLine("FOUNDDDDDD");
                                Console.WriteLine(sc._channel_text);
                                send_channel_text(sc._channel_text, client);
                            }
                        }
                        break;

                    case "get_ptext":
                        string pdata = Receivedata(stream);
                        Console.WriteLine("Le texte de :" + pdata + " va etre transmit");
                        foreach (Private p in list_clients[id].privates._privates)
                        {
                            foreach (Client cc in p._two_concerned)
                            {
                                if (cc._name == pdata)
                                {
                                    Console.WriteLine("FOUNDDDDDD");
                                    Console.WriteLine(p._text);
                                    send_private_text(p._text, list_clients[id]._tcpclient);
                                }

                                //else send_private_text(p._text, cc._tcpclient);
                            }
                        }
                        break;

                    case "msg":
                        string channel = Receivedata(stream);
                        string msg = Receivedata(stream);
                        Console.WriteLine("Le message a ete bien recu, le channel est :" + channel + " message : " + msg);
                        foreach (Channel ccc in list_clients[id].active_channels._channels)
                        {
                            if (Equals(channel.Trim(), ccc._name))
                            {
                                DateTime timestamp = DateTime.Now;
                                ccc._channel_text += Environment.NewLine + timestamp.ToLongTimeString() + "\t" + msg;

                            }
                        }

                        broadcast(msg, client, channel);
                        break;

                    case "pmsg":

                        string pchannel = Receivedata(stream);
                        string pmsg = Receivedata(stream);
                        Console.WriteLine("Le message a ete bien recu, le client qui va recevoir est :" + pchannel + " message : " + pmsg);
                        foreach (Private p in list_clients[id].privates._privates)
                        {
                            foreach (Client cccc in p._two_concerned)
                            {
                                if (Equals(pchannel.Trim(), cccc._name))
                                {
                                    DateTime timestamp = DateTime.Now;
                                    p._text += Environment.NewLine + timestamp.ToLongTimeString() + "\t" + pmsg;
                                    Console.WriteLine("VERIFICATIONNNNNNNNNNNNNNNNNNNNNNNNN");
                                    privatesend(pmsg, list_clients[id],cccc);
                                }
                                else
                                {
                                    //privatesend(pmsg, cccc);
                                }
                            }
                        }
                        break;

                    case "conn":
                        string datas = Receivedata(stream);

                        Client c = list_clients[id];
                        foreach (Channel _channel in server_channels._channels)
                        {
                            if (Equals(_channel._name, datas.Trim()) == true)
                            {
                                c.active_channels._channels.Add(_channel);
                                Console.WriteLine("Channel " + _channel._name + " added to the active clients");
                                string newconn = c._name + " is now connected";
                                _channel._channel_text += c._name + " is now connected" + Environment.NewLine;
                                broadcast(newconn, client, datas.Trim());
                            }
                        }
                        break;

                    case "pconn":

                        string datass = Receivedata(stream);

                        Client cs = list_clients[id];

                        foreach (Client clients in list_clients.Values)
                        {
                            if (Equals(clients._name, datass.Trim()) == true)
                            {
                                Private privateconn = new Private(cs, clients);
                                cs.privates._privates.Add(privateconn);
                                clients.privates._privates.Add(privateconn);
                                Console.WriteLine("The new private have been add to " + cs._name + " for a communication with " + clients._name);
                                string newconn = cs._name + " want to speak with you";
                                privateconn._text += cs._name + " want to speak with you";
                                privatesend(newconn, cs, clients);
                            }
                        }
                        break;

                    case "exit":
                        string cantd = Receivedata(stream);
                        Client css = list_clients[id];
                        Channel tormeove = null ;
                        foreach(Channel chann in css.active_channels._channels)
                        {
                            if(chann._name == cantd)
                            {
                                tormeove = chann;
                                string deco = css._name + " has leaved the chat";
                                broadcast(deco, client, cantd);
                            }
                        }

                        css.active_channels._channels.Remove(tormeove);

                        break;
                    default:
                        Console.WriteLine("No defined action for :"+ action);
                        break;
                }
            }

            lock (_lock) list_clients.Remove(id);
            client.Client.Shutdown(SocketShutdown.Both);
            client.Close();
        }


        // RECEIVE FUNCTION RETURN A STRING AND USING NETWORKSTREAM
        public string Receivedata(NetworkStream stream)
        {
            byte[] msg = new byte[1024]; ;
            int action_count = stream.Read(msg, 0, msg.Length);
            if (action_count == 0)
            {
                msg = null;
            }
            string data = Encoding.ASCII.GetString(msg, 0, action_count);
            return data;
        }

        public static void broadcast(string data, TcpClient nosend, string channel)
        {
            Console.WriteLine("I want to know : " + channel);

            lock (_lock)
            {
                foreach (Client c in list_clients.Values)
                {
                    TcpClient cou = c._tcpclient;
                    if (cou != nosend)
                    {
                        Console.WriteLine("Message pret a etre envoye");
                        foreach (Channel chan in c.active_channels._channels)
                        {
                            if (chan._name == channel.Trim())
                            {
                                Console.WriteLine("Nous pouvons envoyé sur le channel :" + chan._name);
                                NetworkStream stream = cou.GetStream();
                                SendMessages(channel, stream);
                                SendMessages(data, stream);
                            }
                        }
                    }
                }
            }
        }

        public static void privatesend(string data, Client nosend, Client tosend)
        {
            Console.WriteLine("Private send tcp name transmitted for the function : " + nosend._name);

            lock (_lock)
            {
              Console.WriteLine("Message pret a etre envoye a l'autre client");

               foreach (Private pr in nosend.privates._privates)
                {
                    foreach (Client cl in pr._two_concerned)
                    {
                        Console.WriteLine("Les clients de the two concerned are : "+cl._name);
                  if (cl._name != nosend._name.Trim() && cl._name == tosend._name)
                    {
                        Console.WriteLine("Nous pouvons envoyé a : " + cl._name);
                        NetworkStream stream = cl._tcpclient.GetStream();
                            SendMessages(nosend._name, stream);
                            SendMessages(data, stream);
                     }
                   }
                            
                } 
            }
        }

        public static void send_channel_text(string data, TcpClient send)
        {
            lock (_lock)
            {
                NetworkStream stream = send.GetStream();
                SendMessages("get_text", stream);
                SendMessages(data, stream);
            }
        }

        public static void send_private_text(string data, TcpClient send)
        {
            lock (_lock)
            {
                NetworkStream stream = send.GetStream();
                SendMessages("get_ptext", stream);
                SendMessages(data, stream);
            }
        }

        public static void addnewbie(string data, TcpClient nosend)
        {
            lock (_lock)
            {
                foreach (Client c in list_clients.Values)
                {
                    TcpClient cou = c._tcpclient;
                    if (cou != nosend)
                    {

                        Console.WriteLine("Transfert et ajout du client :" + data);
                        NetworkStream stream = cou.GetStream();
                        SendMessages("newbie", stream);
                        SendMessages(data, stream);

                    }
                }
            }
        }


        private static void SendMessages(string data, NetworkStream ns)
        {
            byte[] databuffer = Encoding.ASCII.GetBytes(data);

            ns.Write(databuffer, 0, databuffer.Length);
            System.Threading.Thread.Sleep(10);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

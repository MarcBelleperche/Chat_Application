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

        //Here strats the main secction
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        /// 
        Channels server_channels;

        public Server_info()
        {

            InitializeComponent();

            server_channels = new Channels();
            server_channels._channels.Add(new Channel("Les Gentlemen du sexe"));
            server_channels._channels.Add(new Channel("Nitro Gaming"));
            server_channels._channels.Add(new Channel("VIVE les glups"));
            server_channels._channels.Add(new Channel("ITF PROMO 2021"));


            Connection(5000);

            //Instancing objetcs
            //Here strats the main secction


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

                //ChooseCorP();

                //New thread for the client connected
                Thread t = new Thread(handle_clients);
                t.Start(count);
                count++;
            }

        }

        public List<string> Convert_to_string()
        {
            List<string> stringlist = new List<string>();
            foreach (Channel c in server_channels._channels)
            {
                stringlist.Add(c._name);
            }
            return stringlist;
        }


        public void handle_clients(object o)
        {
            int id = (int)o;
            TcpClient client;


            lock (_lock) client = list_clients[id]._tcpclient;


            StreamWriter sW = new StreamWriter(client.GetStream());
            //sW.WriteLine(sendchannels());

            // Read the username (waiting for the client to use WriteLine())
            //string channel = sR.ReadLine();
            //Console.WriteLine(channel);
            //string channel = null;
            //list_clients[id].channel._name = channel;

            while (true)
            {
                //StreamReader sR = new StreamReader(client.GetStream());
                ///string username = sR.ReadLine().ToUpper();
                //string username = "FIONA";

                NetworkStream stream = client.GetStream();

                //string username = "<" + sR.ReadLine().ToUpper() + ">";
                byte[] msg = new byte[1024]; ;

                int action_count = stream.Read(msg, 0, msg.Length);
                if (action_count == 0)
                {
                    break;
                }
                string action = Encoding.ASCII.GetString(msg, 0, action_count);
                Console.WriteLine(action);


                /* byte[] bufferchannel = new byte[1024]; ;


                 int channel_byte_count = stream.Read(bufferchannel, 0, bufferchannel.Length);
                 if (channel_byte_count == 0)
                 {
                     break;
                 }*/

                string[] separator = { "," };
                int count = 2;
                string[] actions = action.Split(separator, count, StringSplitOptions.RemoveEmptyEntries);
                Console.WriteLine(actions[0]);

                if (Equals(actions[0].Trim(), "newbie") == true)
                {
                    //action[1] = action[1].Trim();
                    //data = data.ToUpper();
                    list_clients[id]._name = actions[1];
                    Console.WriteLine(actions[1]);
                    addnewbie(actions[1], client);
                }

               /* else if (Equals(actions[0].Trim(), "get_text")== true)
                {
                    byte[] buffer = new byte[1024];
                    int byte_count = stream.Read(buffer, 0, buffer.Length);
                    string data = Encoding.ASCII.GetString(buffer, 0, byte_count);


                    if (byte_count == 0)
                    {
                        break;
                    }

                    foreach (Channel c in list_clients[id].active_channels._channels)
                    {
                        Console.WriteLine("The get text is ok :"+actions[0]);
                        Console.WriteLine("The channel we want to get text of :" + data);
                        //Console.WriteLine(actions[1]);
                        if (Equals(c._name, data)==true)
                        {
                            Console.WriteLine("Transmission du text du channel :"+ c._name);
                            send_channel_text(c._channel_text, client); ;
                        }
                    }
                }*/

                /* else if(Equals(actions[0].Trim(), "gct") == true)
                 {

                     BinaryFormatter bf = new BinaryFormatter();
                     foreach(Channel c in list_clients[id].active_channels._channels)
                     {
                         if (Equals(c._name,actions[1].Trim()) == true) 
                         {
                             bf.Serialize(client.GetStream(), c._channel_rt);
                         }
                     }
                 }*/

                else
                {
                    byte[] buffer = new byte[1024];
                    int byte_count = stream.Read(buffer, 0, buffer.Length);

                    if (byte_count == 0)
                    {
                        break;
                    }

                    else if (Equals(action.Trim(), "get_text,") == true)
                    {
                        string data = Encoding.ASCII.GetString(buffer, 0, byte_count);
                        Console.WriteLine("Le texte du channel :" +data+ " va etre transmit");
                        foreach (Channel c in list_clients[id].active_channels._channels)
                        {
                            if (c._name == data)
                            {
                                Console.WriteLine("FOUNDDDDDD");
                                Console.WriteLine(c._channel_text);
                                send_channel_text(c._channel_text, client); 
                            }
                        }
                        //send_channel_text();

                    }

                    else if (Equals(action.Trim(), "msg") == true)
                    {
                        string data = Encoding.ASCII.GetString(buffer, 0, byte_count);


                        string[] separeted = data.Split(separator, count, StringSplitOptions.RemoveEmptyEntries);
                        string channel = separeted[0];
                        data = separeted[1];
                        Console.WriteLine("Le message a ete bien recu, le channel est :" + channel + " message : " + data);
                        foreach (Channel c in list_clients[id].active_channels._channels)
                        {
                            if (Equals(channel.Trim(), c._name))
                            {
                                DateTime timestamp = DateTime.Now;
                                c._channel_text += Environment.NewLine + timestamp.ToLongTimeString() + "\t" + data ;

                            }
                        }
                        broadcast(data, client, channel);
                    }


                    else if (Equals(action.Trim(), "conn") == true)
                    {
                        string data = Encoding.ASCII.GetString(buffer, 0, byte_count);

                        Client c = list_clients[id];
                        foreach (Channel channel in server_channels._channels)
                        {
                            if (Equals(channel._name, data.Trim()) == true)
                            {
                                c.active_channels._channels.Add(channel);
                                Console.WriteLine("Channel " + channel._name + " added to the active clients");
                                string newconn = c._name + " is now connected";
                                channel._channel_text += c._name + " is now connected" + Environment.NewLine;
                                broadcast(newconn, client, data.Trim());
                            }
                        }
                    }

                    else if (Equals(action.Trim(), "pconn") == true)
                    {
                        string data = Encoding.ASCII.GetString(buffer, 0, byte_count);

                        Client c = list_clients[id];

                        foreach (Client clients in list_clients.Values)
                        {
                            if (Equals(clients._name, data.Trim()) == true)
                            {
                                Private privateconn = new Private(c, clients);
                                c.privates._privates.Add(privateconn);
                                clients.privates._privates.Add(privateconn);
                                //clients.privates._privates.Add(new Private(clients, c));
                                Console.WriteLine("The new private have been add to " + c._name + " for a communication with " + clients._name);
                                //c.active_channels._channels.Add(channel);
                                //Console.WriteLine("Channel " + channel._name + " added to the active clients");
                                string newconn = c._name + " want to speak with you";
                                privateconn._text += c._name + "want to speak with you";
                                //channel._channel_text += c._name + " is now connected" + Environment.NewLine;
                                privatesend(newconn, c, data.Trim());
                            }
                        }
                    }
                }
                //Console.WriteLine(username);
                //Console.WriteLine(data);
                /*string ms = Encoding.ASCII.GetString(msg, 0, channel_byte_count_u);
                Console.WriteLine(ms);
*/



                // string channel = Encoding.ASCII.GetString(bufferchannel, 0, channel_byte_count);

                /* string actionok = "OK";
                 byte[] check = Encoding.ASCII.GetBytes(actionok);
                 stream.Write(check, 0, check.Length);*/



            }

            lock (_lock) list_clients.Remove(id);
            client.Client.Shutdown(SocketShutdown.Both);
            client.Close();
        }

        public static void broadcast(string data, TcpClient nosend, string channel)
        {
            //byte[] buffer = Encoding.ASCII.GetBytes(author + " : "+data + Environment.NewLine);
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            byte[] bufferchannel = Encoding.ASCII.GetBytes(channel);
            Console.WriteLine("I want to know : " + channel);

            //byte[] bufferuser = Encoding.ASCII.GetBytes(author + Environment.NewLine);

            lock (_lock)
            {
                foreach (Client c in list_clients.Values)
                {
                    //string channel = c.channel._name;
                    //if (channel == commchan ) {
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
                                //stream.Write(bufferuser, 0, bufferuser.Length);
                                stream.Write(bufferchannel, 0, bufferchannel.Length);
                                stream.Write(buffer, 0, buffer.Length);
                            }
                        }
                    }
                }
            }
        }

        public static void privatesend(string data, Client nosend, string name_to_send)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            byte[] bufferchannel = Encoding.ASCII.GetBytes(name_to_send);
            Console.WriteLine("I want to know : " + name_to_send);

            //byte[] bufferuser = Encoding.ASCII.GetBytes(author + Environment.NewLine);

            lock (_lock)
            {
              Console.WriteLine("Message pret a etre envoye");

               foreach (Private pr in nosend.privates._privates)
                {
                 foreach (Client cl in pr._two_concerned)
                 {
                  if (cl._name == name_to_send.Trim())
                    {
                        Console.WriteLine("Nous pouvons envoyé a :" + cl._name);
                        NetworkStream stream = cl._tcpclient.GetStream();
                        stream.Write(bufferchannel, 0, bufferchannel.Length);
                        stream.Write(buffer, 0, buffer.Length);
                     }
                   }
                            
                } 
            }
        }

        public static void send_channel_text(string data, TcpClient send)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            byte[] bufferchannel = Encoding.ASCII.GetBytes("get_text");

            lock (_lock)
            {
                NetworkStream stream = send.GetStream();
                //stream.Write(bufferuser, 0, bufferuser.Length);
                stream.Write(bufferchannel, 0, bufferchannel.Length);
                //stream.Flush();
                stream.Write(buffer, 0, buffer.Length);
                //stream.Flush();
            }
        }

        public static void addnewbie(string data, TcpClient nosend)
        {
            //byte[] buffer = Encoding.ASCII.GetBytes(author + " : "+data + Environment.NewLine);
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            byte[] bufferchannel = Encoding.ASCII.GetBytes("newbie");

            //byte[] bufferuser = Encoding.ASCII.GetBytes(author + Environment.NewLine);

            lock (_lock)
            {
                foreach (Client c in list_clients.Values)
                {
                    //string channel = c.channel._name;
                    //if (channel == commchan ) {
                    TcpClient cou = c._tcpclient;
                    if (cou != nosend)
                    {

                        Console.WriteLine("Transfert et ajout du client :" + data);
                        NetworkStream stream = cou.GetStream();
                        //stream.Write(bufferuser, 0, bufferuser.Length);
                        stream.Write(bufferchannel, 0, bufferchannel.Length);
                        stream.Write(buffer, 0, buffer.Length);

                    }
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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


        public Server_info()
        {

            InitializeComponent();

            Channels server_channels = new Channels();
            server_channels._channels.Add(new Channel("Default"));
            server_channels._channels.Add(new Channel("Radio sexe"));
            server_channels._channels.Add(new Channel("NitroGaming"));


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

                //ChooseCorP();

                //New thread for the client connected
                Thread t = new Thread(handle_clients);
                t.Start(count);
                count++;
            }

        }

        public static void handle_clients(object o)
        {
            int id = (int)o;
            TcpClient client;

            lock (_lock) client = list_clients[id]._tcpclient;

            //StreamWriter sW = new StreamWriter(client.GetStream());
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
                byte[] bufferchannel = new byte[1024];;

                int channel_byte_count = stream.Read(bufferchannel, 0, bufferchannel.Length);
                if (channel_byte_count == 0)
                {
                    break;
                }

                string channel = Encoding.ASCII.GetString(bufferchannel, 0, channel_byte_count);

                byte[] buffer = new byte[1024];
                int byte_count = stream.Read(buffer, 0, buffer.Length);

                if (byte_count == 0)
                {
                    break;
                }

                string data = Encoding.ASCII.GetString(buffer, 0, byte_count);
                Console.WriteLine("Le message a ete bien recu, le channel est :"+channel+" message : "+data);
                broadcast(data, client, channel);
                //Console.WriteLine(username);
                //Console.WriteLine(data);
            }

            lock (_lock) list_clients.Remove(id);
            client.Client.Shutdown(SocketShutdown.Both);
            client.Close();
        }

        public static void broadcast(string data, TcpClient nosend, string channel)
        {
            //byte[] buffer = Encoding.ASCII.GetBytes(author + " : "+data + Environment.NewLine);
            byte[] buffer = Encoding.ASCII.GetBytes(data + Environment.NewLine);
            byte[] bufferchannel = Encoding.ASCII.GetBytes(channel + Environment.NewLine);

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
                        foreach (Channel chan in c.active_channels._channels) {
                            if (chan._name == channel)
                            {
                                Console.WriteLine("Nous pouvons envoyé sur le channel :" +chan._name);
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




        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

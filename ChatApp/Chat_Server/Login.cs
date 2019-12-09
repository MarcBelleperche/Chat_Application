﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat_Server
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void login_but_Click(object sender, EventArgs e)
        {            

            Client current = Loginfun();
            if (current != null)
            {
                try
                {
                    ChatWindow chat = new ChatWindow(current);
                    //ChatWindow chat = new ChatWindow();
                    chat.Show();
                    label_info.Text = "client connected!!";
                }

                catch (SocketException)
                {
                    label_info.Text = "Please lauch server";
                }
            }
        }

        

        public Client Loginfun()
        {
            //addlcient._clients.Add(new Client(username.StringValue, null));
            //Clients check = new Clients();
            //Clients list = check.deserialize();
            Client use = null;
            Clients c = Clients.deserialize();
            //Console.WriteLine("Enter your username :");
            string user_name = username.Text;
            //Console.WriteLine("Enter your password :");
            string pass_word = password.Text;
            use = c.checkclients(user_name, pass_word, label_info);
            //if (use == null) wrongpwd.StringValue = "Wrong password or not registered";
            return use;

        }


        private void register_but_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
        }
}
}
namespace Chat_Server
{
    partial class ChatWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rt_chat_text = new System.Windows.Forms.RichTextBox();
            this.send_message = new System.Windows.Forms.TextBox();
            this.send = new System.Windows.Forms.Button();
            this.channels_to_select = new System.Windows.Forms.ComboBox();
            this.Connect_new_channel = new System.Windows.Forms.Button();
            this.channels_list = new System.Windows.Forms.ListBox();
            this.info = new System.Windows.Forms.Label();
            this.client_to_connect = new System.Windows.Forms.ComboBox();
            this.check_private = new System.Windows.Forms.RadioButton();
            this.check_channel = new System.Windows.Forms.RadioButton();
            this.private_list = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.acces_channel = new System.Windows.Forms.CheckBox();
            this.acces_private = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // rt_chat_text
            // 
            this.rt_chat_text.Location = new System.Drawing.Point(291, 112);
            this.rt_chat_text.Name = "rt_chat_text";
            this.rt_chat_text.Size = new System.Drawing.Size(497, 265);
            this.rt_chat_text.TabIndex = 0;
            this.rt_chat_text.Text = "";
            this.rt_chat_text.TextChanged += new System.EventHandler(this.rt_chat_text_TextChanged);
            // 
            // send_message
            // 
            this.send_message.Location = new System.Drawing.Point(291, 394);
            this.send_message.Name = "send_message";
            this.send_message.Size = new System.Drawing.Size(395, 22);
            this.send_message.TabIndex = 1;
            // 
            // send
            // 
            this.send.Location = new System.Drawing.Point(713, 393);
            this.send.Name = "send";
            this.send.Size = new System.Drawing.Size(75, 23);
            this.send.TabIndex = 2;
            this.send.Text = "send";
            this.send.UseVisualStyleBackColor = true;
            this.send.Click += new System.EventHandler(this.send_Click);
            // 
            // channels_to_select
            // 
            this.channels_to_select.FormattingEnabled = true;
            this.channels_to_select.Location = new System.Drawing.Point(490, 35);
            this.channels_to_select.Name = "channels_to_select";
            this.channels_to_select.Size = new System.Drawing.Size(124, 24);
            this.channels_to_select.TabIndex = 4;
            this.channels_to_select.Text = "Choose channel";
            this.channels_to_select.SelectedIndexChanged += new System.EventHandler(this.channels_to_select_SelectedIndexChanged);
            // 
            // Connect_new_channel
            // 
            this.Connect_new_channel.Location = new System.Drawing.Point(682, 36);
            this.Connect_new_channel.Name = "Connect_new_channel";
            this.Connect_new_channel.Size = new System.Drawing.Size(75, 23);
            this.Connect_new_channel.TabIndex = 5;
            this.Connect_new_channel.Text = "Connect";
            this.Connect_new_channel.UseVisualStyleBackColor = true;
            this.Connect_new_channel.Click += new System.EventHandler(this.Connect_new_channel_Click);
            // 
            // channels_list
            // 
            this.channels_list.FormattingEnabled = true;
            this.channels_list.ItemHeight = 16;
            this.channels_list.Location = new System.Drawing.Point(12, 36);
            this.channels_list.Name = "channels_list";
            this.channels_list.Size = new System.Drawing.Size(167, 180);
            this.channels_list.TabIndex = 6;
            this.channels_list.SelectedIndexChanged += new System.EventHandler(this.channels_list_SelectedIndexChanged);
            // 
            // info
            // 
            this.info.AutoSize = true;
            this.info.Location = new System.Drawing.Point(247, 112);
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(38, 17);
            this.info.TabIndex = 7;
            this.info.Text = "infos";
            // 
            // client_to_connect
            // 
            this.client_to_connect.FormattingEnabled = true;
            this.client_to_connect.Location = new System.Drawing.Point(316, 35);
            this.client_to_connect.Name = "client_to_connect";
            this.client_to_connect.Size = new System.Drawing.Size(121, 24);
            this.client_to_connect.TabIndex = 8;
            this.client_to_connect.Text = "Choose client ";
            this.client_to_connect.UseWaitCursor = true;
            this.client_to_connect.SelectedIndexChanged += new System.EventHandler(this.client_to_connect_SelectedIndexChanged);
            // 
            // check_private
            // 
            this.check_private.AutoSize = true;
            this.check_private.Location = new System.Drawing.Point(316, 66);
            this.check_private.Name = "check_private";
            this.check_private.Size = new System.Drawing.Size(73, 21);
            this.check_private.TabIndex = 9;
            this.check_private.TabStop = true;
            this.check_private.Text = "Private";
            this.check_private.UseVisualStyleBackColor = true;
            this.check_private.CheckedChanged += new System.EventHandler(this.check_private_CheckedChanged);
            // 
            // check_channel
            // 
            this.check_channel.AutoSize = true;
            this.check_channel.Location = new System.Drawing.Point(490, 66);
            this.check_channel.Name = "check_channel";
            this.check_channel.Size = new System.Drawing.Size(81, 21);
            this.check_channel.TabIndex = 10;
            this.check_channel.TabStop = true;
            this.check_channel.Text = "Channel";
            this.check_channel.UseVisualStyleBackColor = true;
            this.check_channel.CheckedChanged += new System.EventHandler(this.check_channel_CheckedChanged);
            // 
            // private_list
            // 
            this.private_list.FormattingEnabled = true;
            this.private_list.ItemHeight = 16;
            this.private_list.Location = new System.Drawing.Point(12, 263);
            this.private_list.Name = "private_list";
            this.private_list.Size = new System.Drawing.Size(167, 180);
            this.private_list.TabIndex = 11;
            this.private_list.SelectedIndexChanged += new System.EventHandler(this.private_list_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 17);
            this.label1.TabIndex = 12;
            // 
            // acces_channel
            // 
            this.acces_channel.AutoSize = true;
            this.acces_channel.Location = new System.Drawing.Point(12, 15);
            this.acces_channel.Name = "acces_channel";
            this.acces_channel.Size = new System.Drawing.Size(89, 21);
            this.acces_channel.TabIndex = 14;
            this.acces_channel.Text = "Channels";
            this.acces_channel.UseVisualStyleBackColor = true;
            this.acces_channel.CheckedChanged += new System.EventHandler(this.acces_channel_CheckedChanged);
            // 
            // acces_private
            // 
            this.acces_private.AutoSize = true;
            this.acces_private.Location = new System.Drawing.Point(12, 236);
            this.acces_private.Name = "acces_private";
            this.acces_private.Size = new System.Drawing.Size(81, 21);
            this.acces_private.TabIndex = 15;
            this.acces_private.Text = "Privates";
            this.acces_private.UseVisualStyleBackColor = true;
            this.acces_private.CheckedChanged += new System.EventHandler(this.acces_private_CheckedChanged);
            // 
            // ChatWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.acces_private);
            this.Controls.Add(this.acces_channel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.private_list);
            this.Controls.Add(this.check_channel);
            this.Controls.Add(this.check_private);
            this.Controls.Add(this.client_to_connect);
            this.Controls.Add(this.info);
            this.Controls.Add(this.channels_list);
            this.Controls.Add(this.Connect_new_channel);
            this.Controls.Add(this.channels_to_select);
            this.Controls.Add(this.send);
            this.Controls.Add(this.send_message);
            this.Controls.Add(this.rt_chat_text);
            this.Name = "ChatWindow";
            this.Text = "Chat Windows";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rt_chat_text;
        private System.Windows.Forms.TextBox send_message;
        private System.Windows.Forms.Button send;
        private System.Windows.Forms.ComboBox channels_to_select;
        private System.Windows.Forms.Button Connect_new_channel;
        private System.Windows.Forms.ListBox channels_list;
        private System.Windows.Forms.Label info;
        private System.Windows.Forms.ComboBox client_to_connect;
        private System.Windows.Forms.RadioButton check_private;
        private System.Windows.Forms.RadioButton check_channel;
        private System.Windows.Forms.ListBox private_list;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox acces_channel;
        private System.Windows.Forms.CheckBox acces_private;
    }
}
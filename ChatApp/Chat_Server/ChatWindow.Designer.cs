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
            this.port_to_connect = new System.Windows.Forms.TextBox();
            this.channels_to_select = new System.Windows.Forms.ComboBox();
            this.Connect_new_channel = new System.Windows.Forms.Button();
            this.channels_list = new System.Windows.Forms.ListBox();
            this.info = new System.Windows.Forms.Label();
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
            // port_to_connect
            // 
            this.port_to_connect.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.port_to_connect.Location = new System.Drawing.Point(390, 56);
            this.port_to_connect.Name = "port_to_connect";
            this.port_to_connect.Size = new System.Drawing.Size(100, 22);
            this.port_to_connect.TabIndex = 3;
            this.port_to_connect.Text = "port to connect";
            // 
            // channels_to_select
            // 
            this.channels_to_select.FormattingEnabled = true;
            this.channels_to_select.Location = new System.Drawing.Point(537, 54);
            this.channels_to_select.Name = "channels_to_select";
            this.channels_to_select.Size = new System.Drawing.Size(121, 24);
            this.channels_to_select.TabIndex = 4;
            this.channels_to_select.Text = "Choose channel";
            this.channels_to_select.SelectedIndexChanged += new System.EventHandler(this.channels_to_select_SelectedIndexChanged);
            // 
            // Connect_new_channel
            // 
            this.Connect_new_channel.Location = new System.Drawing.Point(694, 54);
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
            this.channels_list.Location = new System.Drawing.Point(12, 12);
            this.channels_list.Name = "channels_list";
            this.channels_list.Size = new System.Drawing.Size(113, 436);
            this.channels_list.TabIndex = 6;
            this.channels_list.SelectedIndexChanged += new System.EventHandler(this.channels_list_SelectedIndexChanged);
            // 
            // info
            // 
            this.info.AutoSize = true;
            this.info.Location = new System.Drawing.Point(185, 221);
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(38, 17);
            this.info.TabIndex = 7;
            this.info.Text = "infos";
            // 
            // ChatWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.info);
            this.Controls.Add(this.channels_list);
            this.Controls.Add(this.Connect_new_channel);
            this.Controls.Add(this.channels_to_select);
            this.Controls.Add(this.port_to_connect);
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
        private System.Windows.Forms.TextBox port_to_connect;
        private System.Windows.Forms.ComboBox channels_to_select;
        private System.Windows.Forms.Button Connect_new_channel;
        private System.Windows.Forms.ListBox channels_list;
        private System.Windows.Forms.Label info;
    }
}
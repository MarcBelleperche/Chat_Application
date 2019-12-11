namespace Chat_Server
{
    partial class Login
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.l_username = new System.Windows.Forms.Label();
            this.l_password = new System.Windows.Forms.Label();
            this.username = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label_info = new System.Windows.Forms.Label();
            this.login_but = new System.Windows.Forms.Button();
            this.register_but = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // l_username
            // 
            this.l_username.AutoSize = true;
            this.l_username.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.l_username.Location = new System.Drawing.Point(230, 147);
            this.l_username.Name = "l_username";
            this.l_username.Size = new System.Drawing.Size(81, 17);
            this.l_username.TabIndex = 0;
            this.l_username.Text = "Username :";
            // 
            // l_password
            // 
            this.l_password.AutoSize = true;
            this.l_password.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.l_password.Location = new System.Drawing.Point(230, 222);
            this.l_password.Name = "l_password";
            this.l_password.Size = new System.Drawing.Size(77, 17);
            this.l_password.TabIndex = 1;
            this.l_password.Text = "Password :";
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(407, 142);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(100, 22);
            this.username.TabIndex = 2;
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(407, 222);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(100, 22);
            this.password.TabIndex = 3;
            this.password.TextChanged += new System.EventHandler(this.password_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(-2, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(280, 103);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label_info
            // 
            this.label_info.AutoSize = true;
            this.label_info.Location = new System.Drawing.Point(244, 297);
            this.label_info.Name = "label_info";
            this.label_info.Size = new System.Drawing.Size(279, 17);
            this.label_info.TabIndex = 5;
            this.label_info.Text = "Please enter your username and password";
            this.label_info.Click += new System.EventHandler(this.label2_Click);
            // 
            // login_but
            // 
            this.login_but.Location = new System.Drawing.Point(203, 340);
            this.login_but.Name = "login_but";
            this.login_but.Size = new System.Drawing.Size(75, 28);
            this.login_but.TabIndex = 6;
            this.login_but.Text = "Login";
            this.login_but.UseVisualStyleBackColor = true;
            this.login_but.Click += new System.EventHandler(this.login_but_Click);
            // 
            // register_but
            // 
            this.register_but.Location = new System.Drawing.Point(463, 340);
            this.register_but.Name = "register_but";
            this.register_but.Size = new System.Drawing.Size(75, 28);
            this.register_but.TabIndex = 7;
            this.register_but.Text = "Register";
            this.register_but.UseVisualStyleBackColor = true;
            this.register_but.Click += new System.EventHandler(this.register_but_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.register_but);
            this.Controls.Add(this.login_but);
            this.Controls.Add(this.label_info);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.password);
            this.Controls.Add(this.username);
            this.Controls.Add(this.l_password);
            this.Controls.Add(this.l_username);
            this.Name = "Login";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label l_username;
        private System.Windows.Forms.Label l_password;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label_info;
        private System.Windows.Forms.Button login_but;
        private System.Windows.Forms.Button register_but;
    }
}


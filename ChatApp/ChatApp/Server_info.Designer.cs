namespace ChatApp
{
    partial class Server_info
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
            this.server_infos = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // server_infos
            // 
            this.server_infos.AutoSize = true;
            this.server_infos.Location = new System.Drawing.Point(305, 150);
            this.server_infos.Name = "server_infos";
            this.server_infos.Size = new System.Drawing.Size(85, 17);
            this.server_infos.TabIndex = 0;
            this.server_infos.Text = "Informations";
            this.server_infos.Click += new System.EventHandler(this.label1_Click);
            // 
            // Server_info
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.server_infos);
            this.Name = "Server_info";
            this.Text = "Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label server_infos;
    }
}


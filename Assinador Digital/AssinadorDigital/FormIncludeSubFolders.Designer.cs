namespace AssinadorDigital
{
    partial class frmIncludeSubFolders
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        public System.ComponentModel.IContainer components = new System.ComponentModel.Container();

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIncludeSubFolders));
            this.chkIncludeSubfolders = new System.Windows.Forms.CheckBox();
            this.btnYes = new System.Windows.Forms.Button();
            this.btnNo = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblAlert = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // chkIncludeSubfolders
            // 
            this.chkIncludeSubfolders.AutoSize = true;
            this.chkIncludeSubfolders.Checked = true;
            this.chkIncludeSubfolders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeSubfolders.Location = new System.Drawing.Point(68, 45);
            this.chkIncludeSubfolders.Name = "chkIncludeSubfolders";
            this.chkIncludeSubfolders.Size = new System.Drawing.Size(107, 17);
            this.chkIncludeSubfolders.TabIndex = 1;
            this.chkIncludeSubfolders.Text = "Incluir Subpastas";
            this.chkIncludeSubfolders.UseVisualStyleBackColor = true;
            // 
            // btnYes
            // 
            this.btnYes.Location = new System.Drawing.Point(277, 41);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(75, 23);
            this.btnYes.TabIndex = 2;
            this.btnYes.Text = "Sim";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // btnNo
            // 
            this.btnNo.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnNo.Location = new System.Drawing.Point(196, 41);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(75, 23);
            this.btnNo.TabIndex = 3;
            this.btnNo.Text = "Não";
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AssinadorDigital.Properties.Resources._161;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(47, 50);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // lblAlert
            // 
            this.lblAlert.AutoSize = true;
            this.lblAlert.Location = new System.Drawing.Point(65, 12);
            this.lblAlert.Name = "lblAlert";
            this.lblAlert.Size = new System.Drawing.Size(287, 13);
            this.lblAlert.TabIndex = 5;
            this.lblAlert.Text = "Um lote de documentos foi selecionado, deseja prosseguir?";
            // 
            // frmIncludeSubFolders
            // 
            this.AcceptButton = this.btnYes;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnNo;
            this.ClientSize = new System.Drawing.Size(364, 81);
            this.Controls.Add(this.lblAlert);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.btnYes);
            this.Controls.Add(this.chkIncludeSubfolders);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(372, 109);
            this.MinimumSize = new System.Drawing.Size(372, 109);
            this.Name = "frmIncludeSubFolders";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Atenção";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkIncludeSubfolders;
        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblAlert;

    }
}
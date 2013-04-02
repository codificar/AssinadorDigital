namespace AssinadorDigital
{
    partial class frmRemoveDigitalSignatures
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRemoveDigitalSignatures));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gpbCopyDoduments = new System.Windows.Forms.GroupBox();
            this.chkOverwrite = new System.Windows.Forms.CheckBox();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnChangeFolder = new System.Windows.Forms.Button();
            this.chkCopyDocuments = new System.Windows.Forms.CheckBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.fbdSelectNewPath = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.gpbCopyDoduments.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AssinadorDigital.Properties.Resources._161;
            this.pictureBox1.InitialImage = global::AssinadorDigital.Properties.Resources._161;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(54, 49);
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(243, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Deseja  remover a(s) assinatura(s) selecionada(s)?";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(231, 141);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gpbCopyDoduments
            // 
            this.gpbCopyDoduments.Controls.Add(this.chkOverwrite);
            this.gpbCopyDoduments.Controls.Add(this.txtPath);
            this.gpbCopyDoduments.Controls.Add(this.btnChangeFolder);
            this.gpbCopyDoduments.Enabled = false;
            this.gpbCopyDoduments.Location = new System.Drawing.Point(12, 67);
            this.gpbCopyDoduments.Name = "gpbCopyDoduments";
            this.gpbCopyDoduments.Size = new System.Drawing.Size(375, 68);
            this.gpbCopyDoduments.TabIndex = 15;
            this.gpbCopyDoduments.TabStop = false;
            this.gpbCopyDoduments.Text = "Salvar em:";
            // 
            // chkOverwrite
            // 
            this.chkOverwrite.AutoSize = true;
            this.chkOverwrite.Checked = true;
            this.chkOverwrite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOverwrite.Location = new System.Drawing.Point(6, 45);
            this.chkOverwrite.Name = "chkOverwrite";
            this.chkOverwrite.Size = new System.Drawing.Size(200, 17);
            this.chkOverwrite.TabIndex = 4;
            this.chkOverwrite.Text = "Sobreescrever arquivo(s) existente(s)";
            this.chkOverwrite.UseVisualStyleBackColor = true;
            this.chkOverwrite.CheckedChanged += new System.EventHandler(this.chkOverwrite_CheckedChanged);
            // 
            // txtPath
            // 
            this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPath.Location = new System.Drawing.Point(6, 19);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(282, 20);
            this.txtPath.TabIndex = 2;
            // 
            // btnChangeFolder
            // 
            this.btnChangeFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangeFolder.Location = new System.Drawing.Point(294, 17);
            this.btnChangeFolder.Name = "btnChangeFolder";
            this.btnChangeFolder.Size = new System.Drawing.Size(75, 23);
            this.btnChangeFolder.TabIndex = 3;
            this.btnChangeFolder.Text = "Procurar";
            this.btnChangeFolder.UseVisualStyleBackColor = true;
            this.btnChangeFolder.Click += new System.EventHandler(this.btnChangeFolder_Click);
            // 
            // chkCopyDocuments
            // 
            this.chkCopyDocuments.AutoSize = true;
            this.chkCopyDocuments.Location = new System.Drawing.Point(75, 44);
            this.chkCopyDocuments.Name = "chkCopyDocuments";
            this.chkCopyDocuments.Size = new System.Drawing.Size(192, 17);
            this.chkCopyDocuments.TabIndex = 14;
            this.chkCopyDocuments.Text = "Salvar documento(s) em outro local";
            this.chkCopyDocuments.UseVisualStyleBackColor = true;
            this.chkCopyDocuments.CheckedChanged += new System.EventHandler(this.chkCopyDocuments_CheckedChanged);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(312, 141);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 13;
            this.btnRemove.Text = "Remover";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // frmRemoveDigitalSignatures
            // 
            this.AcceptButton = this.btnRemove;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(399, 182);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gpbCopyDoduments);
            this.Controls.Add(this.chkCopyDocuments);
            this.Controls.Add(this.btnRemove);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(407, 210);
            this.MinimumSize = new System.Drawing.Size(407, 210);
            this.Name = "frmRemoveDigitalSignatures";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Atenção";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.gpbCopyDoduments.ResumeLayout(false);
            this.gpbCopyDoduments.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox gpbCopyDoduments;
        private System.Windows.Forms.CheckBox chkOverwrite;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnChangeFolder;
        private System.Windows.Forms.CheckBox chkCopyDocuments;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.FolderBrowserDialog fbdSelectNewPath;
    }
}
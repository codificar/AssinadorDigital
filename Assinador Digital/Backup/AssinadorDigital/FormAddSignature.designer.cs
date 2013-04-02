namespace AssinadorDigital
{
    partial class frmAddDigitalSignature
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddDigitalSignature));
            this.btnSign = new System.Windows.Forms.Button();
            this.chkCopyDocuments = new System.Windows.Forms.CheckBox();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnChangeFolder = new System.Windows.Forms.Button();
            this.fbdSelectNewPath = new System.Windows.Forms.FolderBrowserDialog();
            this.chkViewDocuments = new System.Windows.Forms.CheckBox();
            this.gpbCopyDoduments = new System.Windows.Forms.GroupBox();
            this.chkOverwrite = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkIncludeSubfolders = new System.Windows.Forms.CheckBox();
            this.chkNotSignIfAlreadyExists = new System.Windows.Forms.CheckBox();
            this.gpbCopyDoduments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSign
            // 
            this.btnSign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSign.Location = new System.Drawing.Point(312, 183);
            this.btnSign.Name = "btnSign";
            this.btnSign.Size = new System.Drawing.Size(75, 23);
            this.btnSign.TabIndex = 0;
            this.btnSign.Text = "Assinar";
            this.btnSign.UseVisualStyleBackColor = true;
            this.btnSign.Click += new System.EventHandler(this.btnSign_Click);
            // 
            // chkCopyDocuments
            // 
            this.chkCopyDocuments.AutoSize = true;
            this.chkCopyDocuments.Location = new System.Drawing.Point(75, 44);
            this.chkCopyDocuments.Name = "chkCopyDocuments";
            this.chkCopyDocuments.Size = new System.Drawing.Size(192, 17);
            this.chkCopyDocuments.TabIndex = 1;
            this.chkCopyDocuments.Text = "Salvar documento(s) em outro local";
            this.chkCopyDocuments.UseVisualStyleBackColor = true;
            this.chkCopyDocuments.CheckedChanged += new System.EventHandler(this.chkCopyDocuments_CheckedChanged);
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
            // chkViewDocuments
            // 
            this.chkViewDocuments.AutoSize = true;
            this.chkViewDocuments.Checked = true;
            this.chkViewDocuments.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkViewDocuments.Location = new System.Drawing.Point(18, 187);
            this.chkViewDocuments.Name = "chkViewDocuments";
            this.chkViewDocuments.Size = new System.Drawing.Size(193, 17);
            this.chkViewDocuments.TabIndex = 4;
            this.chkViewDocuments.Text = "Visualizar documento(s) assinado(s)";
            this.chkViewDocuments.UseVisualStyleBackColor = true;
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
            this.gpbCopyDoduments.TabIndex = 5;
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
            this.chkOverwrite.Size = new System.Drawing.Size(194, 17);
            this.chkOverwrite.TabIndex = 6;
            this.chkOverwrite.Text = "Sobrescrever arquivo(s) existente(s)";
            this.chkOverwrite.UseVisualStyleBackColor = true;
            this.chkOverwrite.CheckedChanged += new System.EventHandler(this.chkOverwrite_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AssinadorDigital.Properties.Resources._161;
            this.pictureBox1.InitialImage = global::AssinadorDigital.Properties.Resources._161;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(54, 49);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(307, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "A assinatura será adicionada ao(s) documento(s) selecionado(s)";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(231, 183);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkIncludeSubfolders
            // 
            this.chkIncludeSubfolders.AutoSize = true;
            this.chkIncludeSubfolders.Checked = true;
            this.chkIncludeSubfolders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeSubfolders.Location = new System.Drawing.Point(18, 164);
            this.chkIncludeSubfolders.Name = "chkIncludeSubfolders";
            this.chkIncludeSubfolders.Size = new System.Drawing.Size(105, 17);
            this.chkIncludeSubfolders.TabIndex = 16;
            this.chkIncludeSubfolders.Text = "Incluir subpastas";
            this.chkIncludeSubfolders.UseVisualStyleBackColor = true;
            // 
            // chkNotSignIfAlreadyExists
            // 
            this.chkNotSignIfAlreadyExists.AutoSize = true;
            this.chkNotSignIfAlreadyExists.Checked = true;
            this.chkNotSignIfAlreadyExists.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNotSignIfAlreadyExists.Location = new System.Drawing.Point(18, 141);
            this.chkNotSignIfAlreadyExists.Name = "chkNotSignIfAlreadyExists";
            this.chkNotSignIfAlreadyExists.Size = new System.Drawing.Size(199, 17);
            this.chkNotSignIfAlreadyExists.TabIndex = 17;
            this.chkNotSignIfAlreadyExists.Text = "Não adicionar assinatura já existente";
            this.chkNotSignIfAlreadyExists.UseVisualStyleBackColor = true;
            // 
            // frmAddDigitalSignature
            // 
            this.AcceptButton = this.btnSign;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(399, 212);
            this.Controls.Add(this.chkNotSignIfAlreadyExists);
            this.Controls.Add(this.chkIncludeSubfolders);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.gpbCopyDoduments);
            this.Controls.Add(this.chkViewDocuments);
            this.Controls.Add(this.chkCopyDocuments);
            this.Controls.Add(this.btnSign);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(407, 246);
            this.MinimumSize = new System.Drawing.Size(407, 246);
            this.Name = "frmAddDigitalSignature";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Atenção";
            this.gpbCopyDoduments.ResumeLayout(false);
            this.gpbCopyDoduments.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSign;
        private System.Windows.Forms.CheckBox chkCopyDocuments;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnChangeFolder;
        private System.Windows.Forms.FolderBrowserDialog fbdSelectNewPath;
        private System.Windows.Forms.CheckBox chkViewDocuments;
        private System.Windows.Forms.GroupBox gpbCopyDoduments;
        private System.Windows.Forms.CheckBox chkOverwrite;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkIncludeSubfolders;
        private System.Windows.Forms.CheckBox chkNotSignIfAlreadyExists;
    }
}
namespace AssinadorDigital
{
    partial class FormSignatureDetails
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSignatureDetails));
            this.lstDetails = new System.Windows.Forms.ListView();
            this.headerCampo = new System.Windows.Forms.ColumnHeader();
            this.headerValor = new System.Windows.Forms.ColumnHeader();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnViewDetails = new System.Windows.Forms.Button();
            this.gpbValidation = new System.Windows.Forms.GroupBox();
            this.pctValidate = new System.Windows.Forms.PictureBox();
            this.ilistValidate = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1.SuspendLayout();
            this.gpbValidation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctValidate)).BeginInit();
            this.SuspendLayout();
            // 
            // lstDetails
            // 
            this.lstDetails.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.lstDetails.AllowColumnReorder = true;
            this.lstDetails.AllowDrop = true;
            this.lstDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.headerCampo,
            this.headerValor});
            this.lstDetails.FullRowSelect = true;
            this.lstDetails.GridLines = true;
            this.lstDetails.Location = new System.Drawing.Point(6, 19);
            this.lstDetails.Name = "lstDetails";
            this.lstDetails.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lstDetails.Size = new System.Drawing.Size(385, 213);
            this.lstDetails.TabIndex = 1;
            this.lstDetails.UseCompatibleStateImageBehavior = false;
            this.lstDetails.View = System.Windows.Forms.View.Details;
            // 
            // headerCampo
            // 
            this.headerCampo.Text = "Campo";
            this.headerCampo.Width = 138;
            // 
            // headerValor
            // 
            this.headerValor.Text = "Valor";
            this.headerValor.Width = 227;
            // 
            // txtStatus
            // 
            this.txtStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatus.BackColor = System.Drawing.SystemColors.Control;
            this.txtStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStatus.Location = new System.Drawing.Point(28, 19);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(357, 62);
            this.txtStatus.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnViewDetails);
            this.groupBox1.Controls.Add(this.lstDetails);
            this.groupBox1.Location = new System.Drawing.Point(12, 105);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(397, 267);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Resumo do Certificado";
            // 
            // btnViewDetails
            // 
            this.btnViewDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewDetails.Location = new System.Drawing.Point(295, 238);
            this.btnViewDetails.Name = "btnViewDetails";
            this.btnViewDetails.Size = new System.Drawing.Size(96, 23);
            this.btnViewDetails.TabIndex = 2;
            this.btnViewDetails.Text = "Abrir Certificado";
            this.btnViewDetails.UseVisualStyleBackColor = true;
            this.btnViewDetails.Click += new System.EventHandler(this.btnViewDetails_Click);
            // 
            // gpbValidation
            // 
            this.gpbValidation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gpbValidation.Controls.Add(this.pctValidate);
            this.gpbValidation.Controls.Add(this.txtStatus);
            this.gpbValidation.Location = new System.Drawing.Point(12, 12);
            this.gpbValidation.Name = "gpbValidation";
            this.gpbValidation.Size = new System.Drawing.Size(391, 87);
            this.gpbValidation.TabIndex = 5;
            this.gpbValidation.TabStop = false;
            this.gpbValidation.Text = "Validação";
            // 
            // pctValidate
            // 
            this.pctValidate.Location = new System.Drawing.Point(6, 19);
            this.pctValidate.Name = "pctValidate";
            this.pctValidate.Size = new System.Drawing.Size(16, 16);
            this.pctValidate.TabIndex = 4;
            this.pctValidate.TabStop = false;
            // 
            // ilistValidate
            // 
            this.ilistValidate.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilistValidate.ImageStream")));
            this.ilistValidate.TransparentColor = System.Drawing.Color.Transparent;
            this.ilistValidate.Images.SetKeyName(0, "signinvalid.gif");
            this.ilistValidate.Images.SetKeyName(1, "signalert.gif");
            this.ilistValidate.Images.SetKeyName(2, "signvalid.gif");
            // 
            // FormSignatureDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 384);
            this.Controls.Add(this.gpbValidation);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSignatureDetails";
            this.Text = "Detalhes do Certificado";
            this.Load += new System.EventHandler(this.FormSignatureDetails_Load);
            this.groupBox1.ResumeLayout(false);
            this.gpbValidation.ResumeLayout(false);
            this.gpbValidation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctValidate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstDetails;
        private System.Windows.Forms.ColumnHeader headerCampo;
        private System.Windows.Forms.ColumnHeader headerValor;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnViewDetails;
        private System.Windows.Forms.GroupBox gpbValidation;
        private System.Windows.Forms.ImageList ilistValidate;
        private System.Windows.Forms.PictureBox pctValidate;

    }
}
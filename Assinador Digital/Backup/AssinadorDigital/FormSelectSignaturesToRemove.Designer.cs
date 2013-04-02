namespace AssinadorDigital
{
    partial class frmSelectDigitalSignatureToRemove
    {

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectDigitalSignatureToRemove));
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnRemoveAll = new System.Windows.Forms.Button();
            this.gpbFiles = new System.Windows.Forms.GroupBox();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.gpbDescription = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lblSelected = new System.Windows.Forms.Label();
            this.lblDName = new System.Windows.Forms.Label();
            this.lblDPath = new System.Windows.Forms.Label();
            this.lblModifiedBy = new System.Windows.Forms.Label();
            this.lblModified = new System.Windows.Forms.Label();
            this.lblDModified = new System.Windows.Forms.Label();
            this.lblCreated = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblCreator = new System.Windows.Forms.Label();
            this.lblSubject = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblDCreator = new System.Windows.Forms.Label();
            this.lblDCreated = new System.Windows.Forms.Label();
            this.lblDModifiedBy = new System.Windows.Forms.Label();
            this.lblDTitle = new System.Windows.Forms.Label();
            this.lblDDescription = new System.Windows.Forms.Label();
            this.lblDSubject = new System.Windows.Forms.Label();
            this.lstDocuments = new System.Windows.Forms.ListView();
            this.flName = new System.Windows.Forms.ColumnHeader();
            this.flType = new System.Windows.Forms.ColumnHeader();
            this.ilistFiles = new System.Windows.Forms.ImageList(this.components);
            this.gpbSignatures = new System.Windows.Forms.GroupBox();
            this.lstSigners = new System.Windows.Forms.ListView();
            this.sgName = new System.Windows.Forms.ColumnHeader();
            this.sgIssuer = new System.Windows.Forms.ColumnHeader();
            this.sgDate = new System.Windows.Forms.ColumnHeader();
            this.ilistValidate = new System.Windows.Forms.ImageList(this.components);
            this.formHeaderImage = new System.Windows.Forms.PictureBox();
            this.ctxArquivo = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.abrirArquivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirLocalDoArquivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxAssinatura = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.visualizarCertificadoDigitalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visualizarXMLDaAssinaturaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gpbFiles.SuspendLayout();
            this.gpbDescription.SuspendLayout();
            this.gpbSignatures.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.formHeaderImage)).BeginInit();
            this.ctxArquivo.SuspendLayout();
            this.ctxAssinatura.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Enabled = false;
            this.btnRemove.Location = new System.Drawing.Point(180, 213);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(165, 23);
            this.btnRemove.TabIndex = 4;
            this.btnRemove.Text = "Remover Selecionada(s)";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnRemoveAll
            // 
            this.btnRemoveAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveAll.Enabled = false;
            this.btnRemoveAll.Location = new System.Drawing.Point(6, 213);
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.Size = new System.Drawing.Size(165, 23);
            this.btnRemoveAll.TabIndex = 6;
            this.btnRemoveAll.Text = "Remover Todas Assinaturas";
            this.btnRemoveAll.UseVisualStyleBackColor = true;
            this.btnRemoveAll.Click += new System.EventHandler(this.btnRemoveAll_Click);
            // 
            // gpbFiles
            // 
            this.gpbFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gpbFiles.Controls.Add(this.btnSelectAll);
            this.gpbFiles.Controls.Add(this.gpbDescription);
            this.gpbFiles.Controls.Add(this.lstDocuments);
            this.gpbFiles.Controls.Add(this.gpbSignatures);
            this.gpbFiles.Location = new System.Drawing.Point(12, 70);
            this.gpbFiles.Name = "gpbFiles";
            this.gpbFiles.Size = new System.Drawing.Size(648, 499);
            this.gpbFiles.TabIndex = 8;
            this.gpbFiles.TabStop = false;
            this.gpbFiles.Text = "Arquivos";
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectAll.Location = new System.Drawing.Point(471, 219);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(165, 23);
            this.btnSelectAll.TabIndex = 19;
            this.btnSelectAll.Text = "Selecionar Todos";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // gpbDescription
            // 
            this.gpbDescription.Controls.Add(this.label1);
            this.gpbDescription.Controls.Add(this.tbPath);
            this.gpbDescription.Controls.Add(this.tbName);
            this.gpbDescription.Controls.Add(this.lblSelected);
            this.gpbDescription.Controls.Add(this.lblDName);
            this.gpbDescription.Controls.Add(this.lblDPath);
            this.gpbDescription.Controls.Add(this.lblModifiedBy);
            this.gpbDescription.Controls.Add(this.lblModified);
            this.gpbDescription.Controls.Add(this.lblDModified);
            this.gpbDescription.Controls.Add(this.lblCreated);
            this.gpbDescription.Controls.Add(this.lblTitle);
            this.gpbDescription.Controls.Add(this.lblCreator);
            this.gpbDescription.Controls.Add(this.lblSubject);
            this.gpbDescription.Controls.Add(this.lblDescription);
            this.gpbDescription.Controls.Add(this.lblDCreator);
            this.gpbDescription.Controls.Add(this.lblDCreated);
            this.gpbDescription.Controls.Add(this.lblDModifiedBy);
            this.gpbDescription.Controls.Add(this.lblDTitle);
            this.gpbDescription.Controls.Add(this.lblDDescription);
            this.gpbDescription.Controls.Add(this.lblDSubject);
            this.gpbDescription.Location = new System.Drawing.Point(5, 248);
            this.gpbDescription.Name = "gpbDescription";
            this.gpbDescription.Size = new System.Drawing.Size(280, 242);
            this.gpbDescription.TabIndex = 17;
            this.gpbDescription.TabStop = false;
            this.gpbDescription.Text = "Descrição";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 213);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Documentos Selecionados:";
            // 
            // tbPath
            // 
            this.tbPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPath.BackColor = System.Drawing.SystemColors.Control;
            this.tbPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPath.Location = new System.Drawing.Point(117, 35);
            this.tbPath.Name = "tbPath";
            this.tbPath.ReadOnly = true;
            this.tbPath.Size = new System.Drawing.Size(157, 13);
            this.tbPath.TabIndex = 30;
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.BackColor = System.Drawing.SystemColors.Control;
            this.tbName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbName.Location = new System.Drawing.Point(117, 19);
            this.tbName.Name = "tbName";
            this.tbName.ReadOnly = true;
            this.tbName.Size = new System.Drawing.Size(157, 13);
            this.tbName.TabIndex = 29;
            // 
            // lblSelected
            // 
            this.lblSelected.AutoSize = true;
            this.lblSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelected.Location = new System.Drawing.Point(152, 213);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Size = new System.Drawing.Size(14, 13);
            this.lblSelected.TabIndex = 28;
            this.lblSelected.Text = "0";
            // 
            // lblDName
            // 
            this.lblDName.AutoSize = true;
            this.lblDName.Location = new System.Drawing.Point(16, 19);
            this.lblDName.Name = "lblDName";
            this.lblDName.Size = new System.Drawing.Size(92, 13);
            this.lblDName.TabIndex = 25;
            this.lblDName.Text = "Nome do Arquivo:";
            // 
            // lblDPath
            // 
            this.lblDPath.AutoSize = true;
            this.lblDPath.Location = new System.Drawing.Point(57, 35);
            this.lblDPath.Name = "lblDPath";
            this.lblDPath.Size = new System.Drawing.Size(51, 13);
            this.lblDPath.TabIndex = 24;
            this.lblDPath.Text = "Caminho:";
            // 
            // lblModifiedBy
            // 
            this.lblModifiedBy.AutoSize = true;
            this.lblModifiedBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModifiedBy.Location = new System.Drawing.Point(114, 68);
            this.lblModifiedBy.Name = "lblModifiedBy";
            this.lblModifiedBy.Size = new System.Drawing.Size(0, 13);
            this.lblModifiedBy.TabIndex = 11;
            // 
            // lblModified
            // 
            this.lblModified.AutoSize = true;
            this.lblModified.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModified.Location = new System.Drawing.Point(114, 148);
            this.lblModified.Name = "lblModified";
            this.lblModified.Size = new System.Drawing.Size(0, 13);
            this.lblModified.TabIndex = 16;
            // 
            // lblDModified
            // 
            this.lblDModified.AutoSize = true;
            this.lblDModified.Location = new System.Drawing.Point(29, 148);
            this.lblDModified.Name = "lblDModified";
            this.lblDModified.Size = new System.Drawing.Size(79, 13);
            this.lblDModified.TabIndex = 23;
            this.lblDModified.Text = "Modificado em:";
            // 
            // lblCreated
            // 
            this.lblCreated.AutoSize = true;
            this.lblCreated.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreated.Location = new System.Drawing.Point(114, 132);
            this.lblCreated.Name = "lblCreated";
            this.lblCreated.Size = new System.Drawing.Size(0, 13);
            this.lblCreated.TabIndex = 15;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(114, 84);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(0, 13);
            this.lblTitle.TabIndex = 12;
            // 
            // lblCreator
            // 
            this.lblCreator.AutoSize = true;
            this.lblCreator.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreator.Location = new System.Drawing.Point(114, 52);
            this.lblCreator.Name = "lblCreator";
            this.lblCreator.Size = new System.Drawing.Size(0, 13);
            this.lblCreator.TabIndex = 10;
            // 
            // lblSubject
            // 
            this.lblSubject.AutoSize = true;
            this.lblSubject.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubject.Location = new System.Drawing.Point(114, 116);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(0, 13);
            this.lblSubject.TabIndex = 14;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(114, 100);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(0, 13);
            this.lblDescription.TabIndex = 13;
            // 
            // lblDCreator
            // 
            this.lblDCreator.AutoSize = true;
            this.lblDCreator.Location = new System.Drawing.Point(65, 52);
            this.lblDCreator.Name = "lblDCreator";
            this.lblDCreator.Size = new System.Drawing.Size(43, 13);
            this.lblDCreator.TabIndex = 17;
            this.lblDCreator.Text = "Criador:";
            // 
            // lblDCreated
            // 
            this.lblDCreated.AutoSize = true;
            this.lblDCreated.Location = new System.Drawing.Point(51, 132);
            this.lblDCreated.Name = "lblDCreated";
            this.lblDCreated.Size = new System.Drawing.Size(57, 13);
            this.lblDCreated.TabIndex = 22;
            this.lblDCreated.Text = "Criado em:";
            // 
            // lblDModifiedBy
            // 
            this.lblDModifiedBy.AutoSize = true;
            this.lblDModifiedBy.Location = new System.Drawing.Point(9, 68);
            this.lblDModifiedBy.Name = "lblDModifiedBy";
            this.lblDModifiedBy.Size = new System.Drawing.Size(99, 13);
            this.lblDModifiedBy.TabIndex = 18;
            this.lblDModifiedBy.Text = "Ultima modificação:";
            // 
            // lblDTitle
            // 
            this.lblDTitle.AutoSize = true;
            this.lblDTitle.Location = new System.Drawing.Point(72, 84);
            this.lblDTitle.Name = "lblDTitle";
            this.lblDTitle.Size = new System.Drawing.Size(36, 13);
            this.lblDTitle.TabIndex = 19;
            this.lblDTitle.Text = "Titulo:";
            // 
            // lblDDescription
            // 
            this.lblDDescription.AutoSize = true;
            this.lblDDescription.Location = new System.Drawing.Point(50, 100);
            this.lblDDescription.Name = "lblDDescription";
            this.lblDDescription.Size = new System.Drawing.Size(58, 13);
            this.lblDDescription.TabIndex = 20;
            this.lblDDescription.Text = "Descrição:";
            // 
            // lblDSubject
            // 
            this.lblDSubject.AutoSize = true;
            this.lblDSubject.Location = new System.Drawing.Point(60, 116);
            this.lblDSubject.Name = "lblDSubject";
            this.lblDSubject.Size = new System.Drawing.Size(48, 13);
            this.lblDSubject.TabIndex = 21;
            this.lblDSubject.Text = "Assunto:";
            // 
            // lstDocuments
            // 
            this.lstDocuments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstDocuments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.flName,
            this.flType});
            this.lstDocuments.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDocuments.FullRowSelect = true;
            this.lstDocuments.HideSelection = false;
            this.lstDocuments.LargeImageList = this.ilistFiles;
            this.lstDocuments.Location = new System.Drawing.Point(6, 19);
            this.lstDocuments.Name = "lstDocuments";
            this.lstDocuments.Size = new System.Drawing.Size(630, 194);
            this.lstDocuments.SmallImageList = this.ilistFiles;
            this.lstDocuments.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstDocuments.TabIndex = 10;
            this.lstDocuments.TileSize = new System.Drawing.Size(300, 56);
            this.lstDocuments.UseCompatibleStateImageBehavior = false;
            this.lstDocuments.View = System.Windows.Forms.View.Tile;
            this.lstDocuments.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lstFiles_MouseUp);
            this.lstDocuments.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstFiles_ItemSelectionChanged);
            this.lstDocuments.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstFiles_KeyUp);
            // 
            // flName
            // 
            this.flName.Text = "Name";
            this.flName.Width = 150;
            // 
            // flType
            // 
            this.flType.Text = "Type";
            this.flType.Width = 100;
            // 
            // ilistFiles
            // 
            this.ilistFiles.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilistFiles.ImageStream")));
            this.ilistFiles.TransparentColor = System.Drawing.Color.Transparent;
            this.ilistFiles.Images.SetKeyName(0, "docx.gif");
            this.ilistFiles.Images.SetKeyName(1, "docx.gif");
            this.ilistFiles.Images.SetKeyName(2, "pptx.gif");
            this.ilistFiles.Images.SetKeyName(3, "pptx.gif");
            this.ilistFiles.Images.SetKeyName(4, "xlsx.gif");
            this.ilistFiles.Images.SetKeyName(5, "xlsx.gif");
            this.ilistFiles.Images.SetKeyName(6, "xps.gif");
            // 
            // gpbSignatures
            // 
            this.gpbSignatures.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gpbSignatures.Controls.Add(this.lstSigners);
            this.gpbSignatures.Controls.Add(this.btnRemoveAll);
            this.gpbSignatures.Controls.Add(this.btnRemove);
            this.gpbSignatures.Enabled = false;
            this.gpbSignatures.Location = new System.Drawing.Point(291, 248);
            this.gpbSignatures.Name = "gpbSignatures";
            this.gpbSignatures.Size = new System.Drawing.Size(351, 242);
            this.gpbSignatures.TabIndex = 9;
            this.gpbSignatures.TabStop = false;
            this.gpbSignatures.Text = "Assinaturas Digitais";
            // 
            // lstSigners
            // 
            this.lstSigners.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstSigners.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.sgName,
            this.sgIssuer,
            this.sgDate});
            this.lstSigners.FullRowSelect = true;
            this.lstSigners.LargeImageList = this.ilistValidate;
            this.lstSigners.Location = new System.Drawing.Point(6, 19);
            this.lstSigners.Name = "lstSigners";
            this.lstSigners.Size = new System.Drawing.Size(339, 188);
            this.lstSigners.SmallImageList = this.ilistValidate;
            this.lstSigners.TabIndex = 9;
            this.lstSigners.UseCompatibleStateImageBehavior = false;
            this.lstSigners.View = System.Windows.Forms.View.Details;
            this.lstSigners.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lstSigners_MouseUp);
            this.lstSigners.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstSigners_ItemSelectionChanged);
            // 
            // sgName
            // 
            this.sgName.Text = "Nome";
            this.sgName.Width = 95;
            // 
            // sgIssuer
            // 
            this.sgIssuer.Text = "Certificadora";
            this.sgIssuer.Width = 85;
            // 
            // sgDate
            // 
            this.sgDate.Text = "Data";
            this.sgDate.Width = 145;
            // 
            // ilistValidate
            // 
            this.ilistValidate.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilistValidate.ImageStream")));
            this.ilistValidate.TransparentColor = System.Drawing.Color.Transparent;
            this.ilistValidate.Images.SetKeyName(0, "signinvalid.gif");
            this.ilistValidate.Images.SetKeyName(1, "signalert.gif");
            this.ilistValidate.Images.SetKeyName(2, "signvalid.gif");
            // 
            // formHeaderImage
            // 
            this.formHeaderImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.formHeaderImage.Image = global::AssinadorDigital.Properties.Resources.remover_1920;
            this.formHeaderImage.InitialImage = global::AssinadorDigital.Properties.Resources.remover_1920;
            this.formHeaderImage.Location = new System.Drawing.Point(-1240, 0);
            this.formHeaderImage.Name = "formHeaderImage";
            this.formHeaderImage.Size = new System.Drawing.Size(1920, 65);
            this.formHeaderImage.TabIndex = 9;
            this.formHeaderImage.TabStop = false;
            this.formHeaderImage.WaitOnLoad = true;
            // 
            // ctxArquivo
            // 
            this.ctxArquivo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirArquivoToolStripMenuItem,
            this.abrirLocalDoArquivoToolStripMenuItem});
            this.ctxArquivo.Name = "ctxArquivo";
            this.ctxArquivo.Size = new System.Drawing.Size(194, 48);
            // 
            // abrirArquivoToolStripMenuItem
            // 
            this.abrirArquivoToolStripMenuItem.Name = "abrirArquivoToolStripMenuItem";
            this.abrirArquivoToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.abrirArquivoToolStripMenuItem.Text = "&Abrir Arquivo";
            this.abrirArquivoToolStripMenuItem.Click += new System.EventHandler(this.abrirArquivoToolStripMenuItem_Click);
            // 
            // abrirLocalDoArquivoToolStripMenuItem
            // 
            this.abrirLocalDoArquivoToolStripMenuItem.Name = "abrirLocalDoArquivoToolStripMenuItem";
            this.abrirLocalDoArquivoToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.abrirLocalDoArquivoToolStripMenuItem.Text = "Abrir &Local do Arquivo";
            this.abrirLocalDoArquivoToolStripMenuItem.Click += new System.EventHandler(this.abrirLocalDoArquivoToolStripMenuItem_Click);
            // 
            // ctxAssinatura
            // 
            this.ctxAssinatura.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.visualizarCertificadoDigitalToolStripMenuItem,
            this.visualizarXMLDaAssinaturaToolStripMenuItem});
            this.ctxAssinatura.Name = "ctxAssinatura";
            this.ctxAssinatura.Size = new System.Drawing.Size(225, 48);
            // 
            // visualizarCertificadoDigitalToolStripMenuItem
            // 
            this.visualizarCertificadoDigitalToolStripMenuItem.Name = "visualizarCertificadoDigitalToolStripMenuItem";
            this.visualizarCertificadoDigitalToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.visualizarCertificadoDigitalToolStripMenuItem.Text = "Visualizar Certificado Digital";
            this.visualizarCertificadoDigitalToolStripMenuItem.Click += new System.EventHandler(this.visualizarCertificadoDigitalToolStripMenuItem_Click);
            // 
            // visualizarXMLDaAssinaturaToolStripMenuItem
            // 
            this.visualizarXMLDaAssinaturaToolStripMenuItem.Name = "visualizarXMLDaAssinaturaToolStripMenuItem";
            this.visualizarXMLDaAssinaturaToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.visualizarXMLDaAssinaturaToolStripMenuItem.Text = "Visualizar XML da &Assinatura";
            this.visualizarXMLDaAssinaturaToolStripMenuItem.Click += new System.EventHandler(this.visualizarXMLDaAssinaturaToolStripMenuItem_Click);
            // 
            // frmSelectDigitalSignatureToRemove
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 589);
            this.Controls.Add(this.formHeaderImage);
            this.Controls.Add(this.gpbFiles);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(680, 617);
            this.Name = "frmSelectDigitalSignatureToRemove";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Remover Assinaturas - Assinador Digital";
            this.Load += new System.EventHandler(this.frmRemoveDigitalSignature_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmRemoveDigitalSignature_FormClosed);
            this.gpbFiles.ResumeLayout(false);
            this.gpbDescription.ResumeLayout(false);
            this.gpbDescription.PerformLayout();
            this.gpbSignatures.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.formHeaderImage)).EndInit();
            this.ctxArquivo.ResumeLayout(false);
            this.ctxAssinatura.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnRemoveAll;
        private System.Windows.Forms.GroupBox gpbFiles;
        private System.Windows.Forms.GroupBox gpbSignatures;
        private System.Windows.Forms.ListView lstDocuments;
        private System.Windows.Forms.Label lblModifiedBy;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCreator;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.Label lblCreated;
        private System.Windows.Forms.Label lblModified;
        private System.Windows.Forms.GroupBox gpbDescription;
        private System.Windows.Forms.ImageList ilistFiles;
        private System.Windows.Forms.ColumnHeader flName;
        private System.Windows.Forms.ColumnHeader flType;
        private System.Windows.Forms.ListView lstSigners;
        private System.Windows.Forms.ColumnHeader sgName;
        private System.Windows.Forms.ColumnHeader sgIssuer;
        private System.Windows.Forms.ImageList ilistValidate;
        private System.Windows.Forms.Label lblDCreated;
        private System.Windows.Forms.Label lblDSubject;
        private System.Windows.Forms.Label lblDDescription;
        private System.Windows.Forms.Label lblDCreator;
        private System.Windows.Forms.Label lblDModifiedBy;
        private System.Windows.Forms.Label lblDTitle;
        private System.Windows.Forms.Label lblDModified;
        private System.Windows.Forms.PictureBox formHeaderImage;
        private System.Windows.Forms.ColumnHeader sgDate;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Label lblSelected;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lblDPath;
        private System.Windows.Forms.Label lblDName;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView treeFolders;
        private System.Windows.Forms.ContextMenuStrip ctxArquivo;
        private System.Windows.Forms.ToolStripMenuItem abrirArquivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirLocalDoArquivoToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip ctxAssinatura;
        private System.Windows.Forms.ToolStripMenuItem visualizarCertificadoDigitalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem visualizarXMLDaAssinaturaToolStripMenuItem;
        public System.ComponentModel.IContainer components;
    }
}


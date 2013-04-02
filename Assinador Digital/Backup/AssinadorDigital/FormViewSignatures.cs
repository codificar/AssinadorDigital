using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Packaging;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Xml;
using FileUtils;
using Microsoft.Office.DocumentFormat.OpenXml.Packaging;
using Microsoft.Win32;
using OPC;

namespace AssinadorDigital
{
    public partial class frmViewDigitalSignature : Form
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public frmViewDigitalSignature(string[] args)
        {
            InitializeComponent();
            documents = args;
            CertificateUtils.VerifyConsultCRL();
        }
        #endregion

        #region private Properties
        RegistryKey assinadorRegistry = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\LTIA\Assinador Digital", true);
        /// <summary>
        /// Object of DigitalSignature
        /// </summary>
        private DigitalSignature digitalSignature;
        private List<FileHistory> selectedDocuments = new List<FileHistory>();
        /// <summary>
        /// String[] of listed documents
        /// </summary>
        private string[] documents;
        private ArrayList signers = new ArrayList();
        private ListViewItem focusedSigner;
        /// <summary>
        /// ArrayList of selected documents in the list
        /// </summary>
        private ArrayList selectedSigners = new ArrayList();
        private ArrayList invalidSignatures = new ArrayList();
        private ArrayList documentProperties = new ArrayList();
        #endregion

        #region Private Methods

        private void listFiles(string[] filenames)
        {
            string[] filetype = new string[2];

            int length = filenames.Length;
            for (int i = 0; i < length; i++)
            {
                if (Path.HasExtension(filenames[i]))
                {
                    string fileextension = Path.GetExtension(filenames[i]);
                    bool documentFound = false;
                    foreach (ListViewItem documentAlreadyInList in lstDocuments.Items)
                    {
                        if (documentAlreadyInList.SubItems[2].Text == filenames[i])
                        {
                            documentFound = true;
                            break;
                        }
                    }
                    if (!documentFound)
                    {
                        if (fileextension == ".docx")
                        {
                            filetype[0] = "0";
                            filetype[1] = "Microsoft Office Word Document";
                        }
                        else if (fileextension == ".docm")
                        {
                            filetype[0] = "1";
                            filetype[1] = "Microsoft Office Word Macro-Enabled Document";
                        }
                        else if (fileextension == ".pptx")
                        {
                            filetype[0] = "2";
                            filetype[1] = "Microsoft Office PowerPoint Presentation";
                        }
                        else if (fileextension == ".pptm")
                        {
                            filetype[0] = "3";
                            filetype[1] = "Microsoft Office PowerPoint Macro-Enabled Presentation";
                        }
                        else if (fileextension == ".xlsx")
                        {
                            filetype[0] = "4";
                            filetype[1] = "Microsoft Office Excel Worksheet";
                        }
                        else if (fileextension == ".xlsm")
                        {
                            filetype[0] = "5";
                            filetype[1] = "Microsoft Office Excel Macro-Enabled Worksheet";
                        }
                        else if (fileextension == ".xps")
                        {
                            filetype[0] = "6";
                            filetype[1] = "XPS Document";
                        }
                        else
                        {
                            filetype[0] = "-1";
                            filetype[1] = "Unknow";
                        }

                        if (filetype[0] != "-1")
                        {
                            ListViewItem listItem = new ListViewItem();         //INDEX
                            listItem.Text = Path.GetFileName(filenames[i]);     //0 filename
                            listItem.ImageIndex = Convert.ToInt32(filetype[0]);
                            listItem.SubItems.Add(filetype[1]);                 //1 filetype
                            listItem.SubItems.Add(filenames[i]);                //2 filepath
                            listItem.SubItems.Add(filenames[i]);                //3 originalFilePath

                            lstDocuments.Items.Add(listItem);
                        }
                    }
                }
            }
            selectedDocuments.Clear();
            int count = lstDocuments.Items.Count;

            for (int i = 0; i < count; i++)
            {
                lstDocuments.Items[i].Selected = true;
                FileHistory fh = new FileHistory(lstDocuments.SelectedItems[i].SubItems[3].Text, lstDocuments.SelectedItems[i].SubItems[2].Text);
                selectedDocuments.Add(fh);
                lblSelected.Text = count.ToString();
            }
            if (lstDocuments.Items.Count > 0)
            {
                lstDocuments.Items[0].Focused = true;
            }
            loadSigners();
        }

        private void loadFileDescription()
        {
            if (selectedDocuments.Count > 0)
            {
                loadDigitalSignature(lstDocuments.FocusedItem.SubItems[2].Text);
                DocumentCoreProperties coreProps = new DocumentCoreProperties(digitalSignature.package, digitalSignature.xpsDocument, digitalSignature.DocumentType);
                documentProperties = coreProps.DocumentProperties;

                for (int i = 0; i < documentProperties.Count; i++)
                {
                    if (documentProperties[i].ToString().Length >= 30)
                        documentProperties[i] = documentProperties[i].ToString().Substring(0, 25) + "...";
                }

                string path = digitalSignature.signers.Path;
                tbName.Text = Path.GetFileName(path);
                if (Path.GetDirectoryName(path).EndsWith("\\"))
                    tbPath.Text = Path.GetDirectoryName(path);
                else
                    tbPath.Text = Path.GetDirectoryName(path) + "\\";
                lblCreator.Text = documentProperties[0].ToString();
                lblModifiedBy.Text = documentProperties[1].ToString();
                lblTitle.Text = documentProperties[2].ToString();
                lblDescription.Text = documentProperties[3].ToString();
                lblSubject.Text = documentProperties[4].ToString();
                lblCreated.Text = documentProperties[5].ToString();
                lblModified.Text = documentProperties[6].ToString();
            }
            else
            {
                tbName.Text = "";
                tbPath.Text = "";
                lblCreator.Text = "";
                lblModifiedBy.Text = "";
                lblTitle.Text = "";
                lblDescription.Text = "";
                lblSubject.Text = "";
                lblCreated.Text = "";
                lblModified.Text = "";
            }

        }

        private void loadDigitalSignature(string filepath)
        {
            string fileextension = Path.GetExtension(filepath);
            try
            {
                if (digitalSignature.DocumentType.Equals(Types.XpsDocument))
                {
                    digitalSignature.xpsDocument.Close();
                }
                else
                {
                    digitalSignature.package.Close();
                }
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show("Memória insuficiênte para executar a aplicação", "Erro",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ObjectDisposedException)
            {
                System.Windows.Forms.MessageBox.Show("O Documento foi fechado ou descartado, portanto não é possível realizar operações com o mesmo. ", "Erro",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch { }
            finally
            {
                    if ((fileextension == ".docx") || (fileextension == ".docm"))
                        digitalSignature = new DigitalSignature(filepath, Types.WordProcessingML);
                    else if ((fileextension == ".pptx") || (fileextension == ".pptm"))
                        digitalSignature = new DigitalSignature(filepath, Types.PresentationML);
                    else if ((fileextension == ".xlsx") || (fileextension == ".xlsm"))
                        digitalSignature = new DigitalSignature(filepath, Types.SpreadSheetML);
                    else if (fileextension == ".xps")
                        digitalSignature = new DigitalSignature(filepath, Types.XpsDocument);
            }
        }

        public bool loadSigners()
        {
            bool checkCRL = Convert.ToBoolean(assinadorRegistry.GetValue("ConsultCRL"));

            if (documents == null)
                return false;

            this.Cursor = Cursors.WaitCursor;

            lstSigners.Items.Clear();
            lstSigners.Groups.Clear();

            if (lstDocuments.SelectedItems.Count > 0)
            {
                List<string> problematicFoundDocuments = new List<string>();
                Signers commonSigners = new Signers();

                List<X509Certificate2> nonconformitySigners = new List<X509Certificate2>();
                List<X509Certificate2> conformitySigners = new List<X509Certificate2>();
                Hashtable certificatesList = new Hashtable();
                foreach (FileHistory filepath in selectedDocuments)
                {
                    try
                    {
                        loadDigitalSignature(filepath.NewPath);

                        if (digitalSignature.error)
                        {
                            if (digitalSignature.DocumentType.Equals(Types.XpsDocument))
                            {
                                digitalSignature.xpsDocument.Close();
                            }
                            else
                            {
                                digitalSignature.package.Close();
                            }
                            this.Cursor = Cursors.Arrow;
                            return false;
                        }

                        invalidSignatures.Clear();
                        if (!digitalSignature.Validate())
                            invalidSignatures = digitalSignature.InvalidDigitalSignatureHolderNames;

                        ListViewGroup sigaturesGroup = new ListViewGroup(Path.GetFileName(filepath.NewPath));
                        lstSigners.Groups.Add(sigaturesGroup);

                        foreach (Signer signer in digitalSignature.signers)
                        {
                            string[] signature = new string[5];
                            signature[0] = signer.name;
                            signature[1] = signer.issuer;
                            signature[2] = signer.uri;
                            signature[3] = signer.date;
                            signature[4] = signer.serialNumber;

                            X509Certificate2 signatureCertificate = signer.signerCertificate;
                            if ((!nonconformitySigners.Contains(signatureCertificate)) && (!conformitySigners.Contains(signatureCertificate)))
                            {
                                if (!CertificateUtils.ValidateCertificate(signatureCertificate, checkCRL, false) ?? true)
                                    nonconformitySigners.Add(signatureCertificate);
                                else
                                    conformitySigners.Add(signatureCertificate);
                                certificatesList.Add(signatureCertificate, CertificateUtils.buildStatus);
                            }

                            X509ChainStatus chainStatus = new X509ChainStatus();
                            chainStatus = (X509ChainStatus)certificatesList[signatureCertificate];

                            ChainDocumentStatus chainDocumentStatus = new ChainDocumentStatus();
                            int signatureIcon;
                            if (!(invalidSignatures.Contains(signature[0]) && invalidSignatures.Contains(signature[1]) &&
                                invalidSignatures.Contains(signature[2]) && invalidSignatures.Contains(signature[3])))
                            {
                                chainDocumentStatus = new ChainDocumentStatus(chainStatus, null);
                                if (nonconformitySigners.Contains(signatureCertificate))
                                    signatureIcon = 1;
                                else
                                    signatureIcon = 2;
                            }
                            else
                            {
                                signatureIcon = 0;
                                chainDocumentStatus = new ChainDocumentStatus(chainStatus, ChainDocumentStatus.ChainDocumentStatusFlags.CorruptedDocument);
                            }

                            ListViewItem newSignerItem = new ListViewItem();    //INDEX
                            newSignerItem.Text = signature[0].ToString();       //0 signer.name
                            newSignerItem.ImageIndex = signatureIcon;
                            newSignerItem.Group = sigaturesGroup;
                            newSignerItem.SubItems.Add(signature[1]);           //1 signer.issuer
                            newSignerItem.SubItems.Add(signature[3]);           //2 signer.date
                            newSignerItem.SubItems.Add(filepath.NewPath);       //3 signer.path
                            newSignerItem.SubItems.Add(signature[4]);           //4 signer.serialNumber
                            newSignerItem.SubItems.Add(signature[2]);           //5 signer.URI
                            newSignerItem.SubItems.Add(filepath.OriginalPath);  //6 signer.originalPath

                            ListViewItem.ListViewSubItem chainSt = new ListViewItem.ListViewSubItem();
                            chainSt.Text = "";
                            chainSt.Tag = (object)chainDocumentStatus;
                            newSignerItem.SubItems.Add(chainSt);                //7 Tag chainStatus

                            newSignerItem.Tag = (object)signatureCertificate;   //Tag signer.signerCertificate

                            lstSigners.Items.Add(newSignerItem);

                            if (lstSigners.Groups.Count == 1)
                            {
                                Signer sgn = new Signer();
                                sgn.name = signature[0].ToString();
                                sgn.issuer = signature[1].ToString();
                                sgn.serialNumber = signature[4].ToString();
                                sgn.signerCertificate = signatureCertificate;
                                if (!commonSigners.Contains(sgn))
                                    commonSigners.Add(sgn);
                            }

                        }
                        Signers commonRecentlyFoundSigners = new Signers();
                        foreach (ListViewItem lst in sigaturesGroup.Items)
                        {
                            foreach (Signer sgn in commonSigners)
                            {
                                if (lst.SubItems[0].Text == sgn.name &&
                                    lst.SubItems[1].Text == sgn.issuer &&
                                    lst.SubItems[4].Text == sgn.serialNumber &&
                                    !commonRecentlyFoundSigners.Contains(sgn))
                                {
                                    commonRecentlyFoundSigners.Add(sgn);
                                }
                            }
                        }
                        commonSigners = commonRecentlyFoundSigners;
                    }
                    #region catch
                    catch (IOException)
                    {
                        if (MessageBox.Show("Erro ao abrir o documento " + System.IO.Path.GetFileName(filepath.NewPath) + ".\nCertifique-se de que o documento não foi movido ou está em uso por outra aplicação.\n\nDeseja retirá-lo da lista?", System.IO.Path.GetFileName(filepath.NewPath),
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            foreach (ListViewItem item in lstDocuments.Items)
                            {
                                if (item.SubItems[2].Text == filepath.NewPath)
                                {
                                    problematicFoundDocuments.Add(item.SubItems[2].Text);
                                    lstDocuments.Items.Remove(item);
                                }
                            }
                        }
                        else
                        {
                            foreach (ListViewItem item in lstDocuments.Items)
                            {
                                if (item.SubItems[2].Text == filepath.NewPath)
                                {
                                    problematicFoundDocuments.Add(item.SubItems[2].Text);
                                    item.Selected = false;
                                    if (lstDocuments.SelectedItems.Count > 0)
                                    {
                                        lstDocuments.FocusedItem = lstDocuments.SelectedItems[0];
                                    }
                                }
                            }
                        }
                    }
                    catch (FileFormatException)
                    {
                        if (MessageBox.Show("Erro ao abrir o documento " + System.IO.Path.GetFileName(filepath.NewPath) + ".\nSeu conteúdo está corrompido, talvez seja um arquivo temporário.\n\nDeseja retirá-lo da lista?", System.IO.Path.GetFileName(filepath.NewPath),
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            foreach (ListViewItem item in lstDocuments.Items)
                            {
                                if (item.SubItems[2].Text == filepath.NewPath)
                                {
                                    problematicFoundDocuments.Add(item.SubItems[2].Text);
                                    lstDocuments.Items.Remove(item);
                                }
                            }
                        }
                        else
                        {
                            foreach (ListViewItem item in lstDocuments.Items)
                            {
                                if (item.SubItems[2].Text == filepath.NewPath)
                                {
                                    problematicFoundDocuments.Add(item.SubItems[2].Text);
                                    item.Selected = false;
                                    if (lstDocuments.SelectedItems.Count > 0)
                                    {
                                        lstDocuments.FocusedItem = lstDocuments.SelectedItems[0];
                                    }
                                }
                            }
                        }
                    }
                    catch (ArgumentNullException)
                    {
                        if (MessageBox.Show("O Documento " + System.IO.Path.GetFileName(filepath.NewPath) + "não está disponível para abertura.\n\nDeseja retirá-lo da lista?", System.IO.Path.GetFileName(filepath.NewPath),
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            foreach (ListViewItem item in lstDocuments.Items)
                            {
                                if (item.SubItems[2].Text == filepath.NewPath)
                                {
                                    problematicFoundDocuments.Add(item.SubItems[2].Text);
                                    lstDocuments.Items.Remove(item);
                                }
                            }
                        }
                        else
                        {
                            foreach (ListViewItem item in lstDocuments.Items)
                            {
                                if (item.SubItems[2].Text == filepath.NewPath)
                                {
                                    problematicFoundDocuments.Add(item.SubItems[2].Text);
                                    item.Selected = false;
                                    if (lstDocuments.SelectedItems.Count > 0)
                                    {
                                        lstDocuments.FocusedItem = lstDocuments.SelectedItems[0];
                                    }
                                }
                            }
                        }
                    }
                    catch (OpenXmlPackageException)
                    {
                        if (MessageBox.Show("O Documento " + System.IO.Path.GetFileName(filepath.NewPath) + " não é um pacote Open XML válido.\n\nDeseja retirá-lo da lista?", System.IO.Path.GetFileName(filepath.NewPath),
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            foreach (ListViewItem item in lstDocuments.Items)
                            {
                                if (item.SubItems[2].Text == filepath.NewPath)
                                {
                                    problematicFoundDocuments.Add(item.SubItems[2].Text);
                                    lstDocuments.Items.Remove(item);
                                }
                            }
                        }
                        else
                        {
                            foreach (ListViewItem item in lstDocuments.Items)
                            {
                                if (item.SubItems[2].Text == filepath.NewPath)
                                {
                                    problematicFoundDocuments.Add(item.SubItems[2].Text);
                                    item.Selected = false;
                                    if (lstDocuments.SelectedItems.Count > 0)
                                    {
                                        lstDocuments.FocusedItem = lstDocuments.SelectedItems[0];
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception err)
                    {

                        if (MessageBox.Show("Houve um problema na listagem do seguinte documento:\n " + System.IO.Path.GetFileName(filepath.NewPath) + "\n" + err.Message.Substring(0, err.Message.IndexOf(".") + 1) + "\n\nDeseja excluí-lo da lista?", System.IO.Path.GetFileName(filepath.NewPath),
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            foreach (ListViewItem item in lstDocuments.Items)
                            {
                                if (item.SubItems[2].Text == filepath.NewPath)
                                {
                                    problematicFoundDocuments.Add(item.SubItems[2].Text);
                                    lstDocuments.Items.Remove(item);
                                }
                            }
                        }
                        else
                        {
                            foreach (ListViewItem item in lstDocuments.Items)
                            {
                                if (item.SubItems[2].Text == filepath.NewPath)
                                {
                                    problematicFoundDocuments.Add(item.SubItems[2].Text);
                                    item.Selected = false;
                                    if (lstDocuments.SelectedItems.Count > 0)
                                    {
                                        lstDocuments.FocusedItem = lstDocuments.SelectedItems[0];
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                }

                if (lstDocuments.SelectedItems.Count > 1)
                {
                    ListViewGroup commonSigaturesGroup = new ListViewGroup("commonSignatures", "Assinaturas em Comum");
                    lstSigners.Groups.Insert(0, commonSigaturesGroup);

                    foreach (Signer sig in commonSigners)
                    {
                        ListViewItem newCommonSignerItem = new ListViewItem();
                        newCommonSignerItem.Text = sig.name;                    //0 signer.name
                        newCommonSignerItem.Group = commonSigaturesGroup;
                        newCommonSignerItem.SubItems.Add(sig.issuer);           //1 signer.issuer
                        newCommonSignerItem.SubItems.Add("");                   //2 signer.date
                        newCommonSignerItem.SubItems.Add("");                   //3 signer.path
                        newCommonSignerItem.SubItems.Add(sig.serialNumber);     //4 signer.serialNumber
                        newCommonSignerItem.SubItems.Add("");                   //5 signer.URI
                        newCommonSignerItem.SubItems.Add("");                   //6 signer.originalPath
                        newCommonSignerItem.Tag = (object)sig.signerCertificate;//Tag signer.signerCertificate

                        lstSigners.Items.Add(newCommonSignerItem);
                    }
                }
                selectedDocuments.Clear();
                foreach (ListViewItem lvi in lstDocuments.SelectedItems)
                {
                    FileHistory fh = new FileHistory(lvi.SubItems[3].Text, lvi.SubItems[2].Text);
                    selectedDocuments.Add(fh);
                }
            }
            if (lstDocuments.SelectedItems.Count > 0)
            {
                loadFileDescription();
                if (digitalSignature.DocumentType.Equals(Types.XpsDocument))
                {
                    digitalSignature.xpsDocument.Close();
                }
                else
                {
                    digitalSignature.package.Close();
                }
            }
            else if (lstDocuments.Items.Count == 0)
            {
                MessageBox.Show("Os arquivos selecionados não são pacotes Open XML válidos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            this.Cursor = Cursors.Arrow;
            return true;
        }

        #endregion                                 
        
        #region Events

        private void frmViewDigitalSignature_Load(object sender, EventArgs e)
        {
            //TODO: Provável causa do problema listado
            listFiles(documents);
        }

        private void frmViewDigitalSignature_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        #region ListView Documents

        private void lstFiles_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            bool status = true;
            if (lstDocuments.SelectedItems.Count < 1)
            {
                lstSigners.Items.Clear();
                lstSigners.Groups.Clear();
                status = false;
            }
            gpbSignatures.Enabled = status;
        }

        private void lstFiles_MouseUp(object sender, MouseEventArgs e)
        {
            selectedDocuments.Clear();
            int count = lstDocuments.SelectedItems.Count;
            if (count > 0)
                gpbSignatures.Enabled = true;
            int i = 0;
            for (i = 0; i < count; i++)
            {
                FileHistory fh = new FileHistory(lstDocuments.SelectedItems[i].SubItems[3].Text, lstDocuments.SelectedItems[i].SubItems[2].Text);
                selectedDocuments.Add(fh);
            }
            
            lblSelected.Text = i.ToString();
            loadSigners();

            if ((e.Button == MouseButtons.Right) && (selectedDocuments.Count == 1))
            {
                ctxArquivo.Show(lstDocuments, e.Location);
            }
        }

        private void lstFiles_KeyDown(object sender, KeyEventArgs e)
        {
            selectedDocuments.Clear();
            int count = lstDocuments.SelectedItems.Count;
            if (count > 0)
                gpbSignatures.Enabled = true;
            for (int i = 0; i < count; i++)
            {
                FileHistory fh = new FileHistory(lstDocuments.SelectedItems[i].SubItems[3].Text, lstDocuments.SelectedItems[i].SubItems[2].Text);
                selectedDocuments.Add(fh);
            }

            lblSelected.Text = count.ToString();
            loadSigners();
        }

        private void abrirArquivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstDocuments.SelectedItems.Count > 0)
            {
                string filePath = lstDocuments.SelectedItems[0].SubItems[2].Text;
                Process.Start(filePath);
            }
        }

        private void abrirLocalDoArquivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstDocuments.SelectedItems.Count > 0)
            {
                string argument = @"/select, " + lstDocuments.SelectedItems[0].SubItems[2].Text;
                Process.Start("explorer.exe", argument);
            }
        }

        #endregion
        #region ListView Signers

        private void lstSigners_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            for (int i = 0; i < lstSigners.Items.Count; i++)
            {
                if (lstSigners.Items[i].Selected)
                    signers.Add(lstSigners.Items[i]);
            }
        }

        private void lstSigners_MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Right) &&
                (lstSigners.SelectedItems.Count == 1) &&
                (lstSigners.SelectedItems[0].Group.Name != "commonSignatures"))
            {
                ctxAssinatura.Show(lstSigners, e.Location);
            }
        }

        private void visualizarXMLDaAssinaturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((lstSigners.SelectedItems.Count > 0) && (lstSigners.SelectedItems[0].Group.Header != "commonSignatures"))
            {
                if (lstSigners.SelectedItems[0].SubItems[5] != null)
                {
                    string filePath = lstSigners.SelectedItems[0].SubItems[3].Text;
                    string uri = lstSigners.SelectedItems[0].SubItems[5].Text;
                    string fileExtension = Path.GetExtension(filePath);
                    string tempFilePath;
                    XmlDocument xDoc = new XmlDocument();
                    
                    if ((fileExtension == ".docx") || (fileExtension == ".docm"))
                    {
                        using (WordprocessingDocument wdoc = WordprocessingDocument.Open(filePath, false))
                        {
                            foreach (XmlSignaturePart xmlSignature in wdoc.DigitalSignatureOriginPart.XmlSignatureParts)
                            {
                                if (xmlSignature.Uri.OriginalString == uri)
                                {
                                    tempFilePath = Path.GetTempFileName() + ".xml";
                                    xDoc.Load(xmlSignature.GetStream());

                                    TextWriter tw = new StreamWriter(tempFilePath, true, System.Text.Encoding.UTF8);
                                    tw.Write(xDoc.InnerXml);
                                    tw.Flush();
                                    tw.Close();
                                    
                                    Process.Start(tempFilePath);    
                                }
                            }
                        }
                    }
                    else if ((fileExtension == ".pptx") || (fileExtension == ".pptm"))
                    {
                        using (PresentationDocument pptdoc = PresentationDocument.Open(filePath, false))
                        {
                            foreach (XmlSignaturePart xmlSignature in pptdoc.DigitalSignatureOriginPart.XmlSignatureParts)
                            {
                                if (xmlSignature.Uri.OriginalString == uri)
                                {
                                    tempFilePath = Path.GetTempFileName() + ".xml";
                                    xDoc.Load(xmlSignature.GetStream());

                                    TextWriter tw = new StreamWriter(tempFilePath, true, System.Text.Encoding.UTF8);
                                    tw.Write(xDoc.InnerXml);
                                    tw.Flush();
                                    tw.Close();

                                    Process.Start(tempFilePath);
                                }
                            }
                        }
                    }
                    else if ((fileExtension == ".xlsx") || (fileExtension == ".xlsm"))
                    {
                        using (SpreadsheetDocument xlsdoc = SpreadsheetDocument.Open(filePath, false))
                        {
                            foreach (XmlSignaturePart xmlSignature in xlsdoc.DigitalSignatureOriginPart.XmlSignatureParts)
                            {
                                if (xmlSignature.Uri.OriginalString == uri)
                                {
                                    tempFilePath = Path.GetTempFileName() + ".xml";
                                    xDoc.Load(xmlSignature.GetStream());

                                    TextWriter tw = new StreamWriter(tempFilePath, true, System.Text.Encoding.UTF8);
                                    tw.Write(xDoc.InnerXml);
                                    tw.Flush();
                                    tw.Close();

                                    Process.Start(tempFilePath);
                                }
                            }
                        }
                    }
                    else if (fileExtension == ".xps")
                    {
                        using (Package package = Package.Open(filePath, FileMode.Open, FileAccess.ReadWrite))
                        {
                            PackageDigitalSignatureManager _signatures = null;
                            _signatures = new PackageDigitalSignatureManager(package);

                            foreach (PackageDigitalSignature sig in _signatures.Signatures)
                            {
                                if (sig.SignaturePart.Uri.OriginalString == uri)
                                {
                                    tempFilePath = Path.GetTempFileName() + ".xml";

                                    xDoc.Load(sig.SignaturePart.GetStream());

                                    TextWriter tw = new StreamWriter(tempFilePath, true, System.Text.Encoding.UTF8);
                                    tw.Write(xDoc.InnerXml);
                                    tw.Flush();
                                    tw.Close();

                                    Process.Start(tempFilePath);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void visualizarCertificadoDigitalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstSigners.SelectedItems.Count > 0)
            {
                FormSignatureDetails FormSignatureDetails = new FormSignatureDetails((X509Certificate2)lstSigners.SelectedItems[0].Tag, (ChainDocumentStatus)lstSigners.SelectedItems[0].SubItems[7].Tag);
                FormSignatureDetails.Owner = (frmViewDigitalSignature)this;
                FormSignatureDetails.ShowDialog();
            }
        }

        #endregion
        #region Other Controls

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            selectedDocuments.Clear();
            int count = lstDocuments.Items.Count;

            for (int i = 0; i < count; i++)
            {
                lstDocuments.Items[i].Selected = true;
                FileHistory fh = new FileHistory(lstDocuments.SelectedItems[i].SubItems[3].Text, lstDocuments.SelectedItems[i].SubItems[2].Text);
                selectedDocuments.Add(fh);
            }
            if (lstDocuments.Items.Count > 0)
            {
                lstDocuments.Items[0].Focused = true;
            }
            lblSelected.Text = count.ToString();

            loadSigners();
        }

        #endregion

        #endregion
    }
}

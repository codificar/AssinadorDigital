using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using FileUtils;
using Microsoft.Win32;
using OPC;

namespace AssinadorDigital
{
    public partial class frmAddDigitalSignature : Form
    {
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="explorerSelectedItens"></param>
        /// <param name="showCheckBoxViewDocuments"></param>
        public frmAddDigitalSignature(string[] explorerSelectedItens, bool showCheckBoxViewDocuments)
        {
            InitializeComponent();
            documentsToSign = explorerSelectedItens;

            chkViewDocuments.Checked = showCheckBoxViewDocuments;
            chkViewDocuments.Visible = showCheckBoxViewDocuments;
            chkIncludeSubfolders.Visible = showCheckBoxViewDocuments;

            LastBackedUpFolder = Registry.CurrentUser.OpenSubKey(@"Software\LTIA\Assinador Digital", true);
            txtPath.Text = (LastBackedUpFolder.GetValue("LastBackUpFolder")??"").ToString();

            CertificateUtils.VerifyConsultCRL();
        }

        public frmAddDigitalSignature(List<FileHistory> selectedItens, bool showCheckBoxViewDocuments)
        {
            InitializeComponent();
            compatibleDocumentsList = selectedItens;

            chkViewDocuments.Checked = showCheckBoxViewDocuments;
            chkViewDocuments.Visible = showCheckBoxViewDocuments;
            chkIncludeSubfolders.Visible = showCheckBoxViewDocuments;

            LastBackedUpFolder = Registry.CurrentUser.OpenSubKey(@"Software\LTIA\Assinador Digital", true);
            txtPath.Text = (LastBackedUpFolder.GetValue("LastBackUpFolder")??"").ToString();

            CertificateUtils.VerifyConsultCRL();
        }

        #endregion

        #region Private Properties

        private DigitalSignature digitalSignature;
        private List<string> compatibleDocuments = new List<string>();
        private List<FileHistory> compatibleDocumentsList = new List<FileHistory>();
        private string[] documentsToSign;
        private List<FileStatus> documentsSignStatus = new List<FileStatus>();
        private RegistryKey LastBackedUpFolder = null;
        #endregion

        #region Private Methods

        private string[] signDocuments()
        {
            String officeDocument = Properties.Resources.OfficeObject;

            X509Certificate2 certificate = CertificateUtils.GetCertificate();

            if (certificate != null)
            {
                List<string> signedDocuments = new List<string>();
                if (chkCopyDocuments.Checked)
                {
                    documentsSignStatus = FileOperations.Copy(compatibleDocumentsList, txtPath.Text, chkOverwrite.Checked);
                    foreach (FileStatus documentToSign in documentsSignStatus)
                    {
                        if (documentToSign.Status == Status.Success)
                        {
                            try
                            {
                                loadDigitalSignature(documentToSign.Path);
                                bool signatureExists = digitalSignature.DocumentHasSignature(certificate);
                                if (signatureExists)
                                {
                                    if (!chkNotSignIfAlreadyExists.Checked)
                                    {
                                        digitalSignature.SetOfficeDocument(officeDocument);
                                        digitalSignature.SignDocument(certificate);
                                    }
                                    else
                                    {
                                        documentToSign.Status = Status.SignatureAlreadyExists;
                                    }
                                }
                                else
                                {
                                    digitalSignature.SetOfficeDocument(officeDocument);
                                    digitalSignature.SignDocument(certificate);
                                }   
                                signedDocuments.Add(documentToSign.Path);
                                if (digitalSignature.DocumentType.Equals(Types.XpsDocument))
                                {
                                    digitalSignature.xpsDocument.Close();
                                }
                                else
                                {
                                    digitalSignature.package.Close();
                                }
                            }
                            catch (NullReferenceException)
                            {
                                documentToSign.Status = Status.GenericError;
                            }
                            catch (FileFormatException)
                            {
                                documentToSign.Status = Status.CorruptedContent;
                            }
                            catch (IOException e)
                            {
                                documentToSign.Status = Status.InUseByAnotherProcess;
                            }
                            catch (Exception e)
                            {
                                documentToSign.Status = Status.GenericError;
                            }
                        }
                    }
                }
                else
                {
                    foreach (string documentToSign in compatibleDocuments)
                    {
                        Status st = new Status();
                        try
                        {
                            loadDigitalSignature(documentToSign);
                            bool signatureExists = digitalSignature.DocumentHasSignature(certificate);
                            if (signatureExists)
                            {
                                if (!chkNotSignIfAlreadyExists.Checked)
                                {
                                    digitalSignature.SetOfficeDocument(officeDocument);
                                    digitalSignature.SignDocument(certificate);
                                    st = Status.ModifiedButNotBackedUp;
                                }
                                else
                                {
                                    st = Status.SignatureAlreadyExistsNotBackedUp;
                                }
                            }
                            else
                            {
                                digitalSignature.SetOfficeDocument(officeDocument);
                                digitalSignature.SignDocument(certificate);
                                st = Status.ModifiedButNotBackedUp;
                            }

                            signedDocuments.Add(documentToSign);

                            if (digitalSignature.DocumentType.Equals(Types.XpsDocument))
                            {
                                digitalSignature.xpsDocument.Close();
                            }
                            else
                            {
                                digitalSignature.package.Close();
                            }
                        }
                        catch
                        {
                            st = Status.NotSigned;
                        }

                        FileStatus documentSignStatus = new FileStatus(documentToSign, st);
                        documentsSignStatus.Add(documentSignStatus);                        
                    }
                }
                return signedDocuments.ToArray();
            }
            return null;
        }

        private void loadDigitalSignature(string filepath)
        {
            string fileextension = Path.GetExtension(filepath);
            try
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
            catch (IOException e)
            {
                throw new IOException(e.Message, e.InnerException);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message,e.InnerException);
            }
        }
        
        #endregion

        #region Events

        private void chkCopyDocuments_CheckedChanged(object sender, EventArgs e)
        {
            gpbCopyDoduments.Enabled = chkCopyDocuments.Checked;
        }

        private void btnChangeFolder_Click(object sender, EventArgs e)
        {
            if (fbdSelectNewPath.ShowDialog() == DialogResult.OK)
                txtPath.Text = fbdSelectNewPath.SelectedPath;
        }

        private void chkOverwrite_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkOverwrite.Checked)
            {
                if (MessageBox.Show("Você optou por não sobrescrever as cópias de segurança caso elas já existam.\nDeseja ainda assim prosseguir em assinar todos os documentos originais?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    chkOverwrite.Checked = true;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FormCollection openedForms = Application.OpenForms;

            int visibleForms = 0;
            foreach (Form form in openedForms)
            {
                if (form.Visible)
                    visibleForms++;

            }
            if (visibleForms < 1)
                Application.Exit();

            this.Close();
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            if (chkCopyDocuments.Checked)
                LastBackedUpFolder.SetValue("LastBackUpFolder", txtPath.Text, RegistryValueKind.String);
            compatibleDocuments.Clear();
            if (compatibleDocumentsList.Count < 1)
            {
                compatibleDocuments = FileOperations.ListAllowedFilesAndSubfolders(documentsToSign, true, chkIncludeSubfolders.Checked);
                foreach (string str in compatibleDocuments)
                {
                    FileHistory fh = new FileHistory(str, str);
                    compatibleDocumentsList.Add(fh);
                }
            }
            else
                compatibleDocuments = FileOperations.ListAllowedFilesAndSubfolders(compatibleDocumentsList, true, chkIncludeSubfolders.Checked);

            if (compatibleDocuments.Count > 0)
            {
                string[] signedDocuments = signDocuments();

                List<string> docs = new List<string>();
                List<FileHistory> docsHist = new List<FileHistory>();
                foreach (FileStatus fs in documentsSignStatus)
                {
                    docs.Add(fs.Path);

                    FileHistory fh = new FileHistory(fs.OldPath, fs.Path);
                    docsHist.Add(fh);
                }
                
                if (signedDocuments != null)
                {
                    if (!chkViewDocuments.Visible)
                    {
                        ((frmManageDigitalSignature)this.Owner).listFiles(docsHist);
                    }
                    if (chkViewDocuments.Checked)
                    {
                        frmViewDigitalSignature FormViewDigitalSignature = new frmViewDigitalSignature(docs.ToArray());
                        FormViewDigitalSignature.Show();
                    }

                    this.Visible = false;

                    frmReport FormReport = new frmReport(documentsSignStatus, "sign");
                    FormReport.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Os arquivos selecionados não são pacotes válidos.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        #endregion
    }
}

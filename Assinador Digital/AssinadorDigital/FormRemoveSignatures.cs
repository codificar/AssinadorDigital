using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using FileUtils;
using Microsoft.Win32;
using OPC;

namespace AssinadorDigital
{
    public partial class frmRemoveDigitalSignatures : Form
    {
        #region Constructor

        /// <summary>
        /// RemoveAllSignatures from the selected documents
        /// </summary>
        /// <param name="documents"></param>
        public frmRemoveDigitalSignatures(List<string> documents)
        {
            InitializeComponent();

            removeSignaturesActionType = removeSignaturesType.removeAllSignatures;
            selectedDocumentsToRemoveSignature = documents;

            LastBackedUpFolder = Registry.CurrentUser.OpenSubKey(@"Software\LTIA\Assinador Digital", true);
            txtPath.Text = (LastBackedUpFolder.GetValue("LastBackUpFolder")??"").ToString();
        }
        /// <summary>
        /// Remove the list os signers from the documents
        /// </summary>
        /// <param name="documents"></param>
        /// <param name="selectedSignatures"></param>
        public frmRemoveDigitalSignatures(List<string> documents, List<Signers> selectedSignatures)
        {
            InitializeComponent();

            removeSignaturesActionType = removeSignaturesType.removeSelectedSignatures;
            selectedDocumentsToRemoveSignature = documents;
            selectedSignaturesInDocuments = selectedSignatures;

            LastBackedUpFolder = Registry.CurrentUser.OpenSubKey(@"Software\LTIA\Assinador Digital", true);
            txtPath.Text = (LastBackedUpFolder.GetValue("LastBackUpFolder")??"").ToString();
        }

        public frmRemoveDigitalSignatures(List<FileHistory> documents)
        {
            InitializeComponent();

            removeSignaturesActionType = removeSignaturesType.removeAllSignatures;
            selectedDocumentsToRemoveDigitalSignature = documents;

            LastBackedUpFolder = Registry.CurrentUser.OpenSubKey(@"Software\LTIA\Assinador Digital", true);
            txtPath.Text = (LastBackedUpFolder.GetValue("LastBackUpFolder")??"").ToString();
        }

        public frmRemoveDigitalSignatures(List<FileHistory> documents, List<Signers> selectedSignatures)
        {
            InitializeComponent();

            removeSignaturesActionType = removeSignaturesType.removeSelectedSignatures;
            selectedDocumentsToRemoveDigitalSignature = documents;
            selectedSignaturesInDocuments = selectedSignatures;

            LastBackedUpFolder = Registry.CurrentUser.OpenSubKey(@"Software\LTIA\Assinador Digital", true);
            txtPath.Text = (LastBackedUpFolder.GetValue("LastBackUpFolder")??"").ToString();
        }
        #endregion

        #region Private Properties

        private DigitalSignature digitalSignature;
        private List<string> selectedDocumentsToRemoveSignature = new List<string>();
        private List<FileHistory> selectedDocumentsToRemoveDigitalSignature = new List<FileHistory>();
        private removeSignaturesType removeSignaturesActionType = new removeSignaturesType();      
        private List<FileStatus> documentsRemoveSignStatus = new List<FileStatus>();
        private List<Signers> selectedSignaturesInDocuments;
        private RegistryKey LastBackedUpFolder = null;

        #endregion

        #region Public Properties

        public enum removeSignaturesType
        {
            removeAllSignatures,
            removeSelectedSignatures
        }

        #endregion

        #region Private Methods

        private void loadDigitalSignature(string filepath)
        {
            string fileextension = Path.GetExtension(filepath);
            try
            {
                if (digitalSignature.DocumentType.Equals(Types.XpsDocument))
                {
                    digitalSignature.xpsDocument.Close();
                }
                else if (digitalSignature.DocumentType.Equals(Types.PdfDocument))
                {
                    // nothing to-do here
                }
                else
                {
                    digitalSignature.package.Close();
                }
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show(Constants.InsufficientMemory, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ObjectDisposedException)
            {
                MessageBox.Show(Constants.DisposedDocument, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                else if (fileextension == ".pdf")
                    digitalSignature = new DigitalSignature(filepath, Types.PdfDocument);

            }
        }

        private void viewReport(List<FileStatus> fileStatusList)
        {
            frmReport FormReport = new frmReport(fileStatusList, "remove");
            FormReport.ShowDialog();
        }

        private void removeSignatures()
        {
            List<string> documentsReadyToRemoveSignature = new List<string>();
            List<FileHistory> documentsToInteract = new List<FileHistory>();

            switch (removeSignaturesActionType)
            {
                case removeSignaturesType.removeAllSignatures:
                    documentsToInteract = selectedDocumentsToRemoveDigitalSignature;
                    break;
                case removeSignaturesType.removeSelectedSignatures:
                    if (selectedSignaturesInDocuments != null)
                    {
                        bool commonSignatures = false;
                        List<string> docs = new List<string>();
                        foreach (Signers signers in selectedSignaturesInDocuments)
                        {
                            if (signers.Path != "commonSignatures")
                            {
                                docs.Add(signers.Path);
                            }
                            else
                            {
                                docs.Clear();
                                commonSignatures = true;
                                break;
                            }
                        }
                        if (!commonSignatures)
                        {
                            foreach (FileHistory fh in selectedDocumentsToRemoveDigitalSignature)
                            {
                                if (docs.Contains(fh.OriginalPath))
                                {
                                    documentsToInteract.Add(fh);
                                }
                            }
                        }
                        else
                        {
                            documentsToInteract = selectedDocumentsToRemoveDigitalSignature;
                        }
                    }
                    break;
            }
            if (documentsToInteract.Count > 0)
            {
                if (chkCopyDocuments.Checked)
                {
                    documentsRemoveSignStatus = FileOperations.Copy(documentsToInteract, txtPath.Text, chkOverwrite.Checked);
                }
                else
                {
                    foreach (FileHistory documentToRemoveSignature in documentsToInteract)
                    {
                        FileStatus documentStatus = new FileStatus(documentToRemoveSignature.OriginalPath, documentToRemoveSignature.NewPath, FileUtils.Status.ModifiedButNotBackedUp);
                        documentsRemoveSignStatus.Add(documentStatus);
                    }
                }
            }

            switch (removeSignaturesActionType)
            {
                case removeSignaturesType.removeAllSignatures:
                    foreach (FileStatus documentToRemoveSignature in documentsRemoveSignStatus)
                    {
                        if ((documentToRemoveSignature.Status == Status.Success) || (documentToRemoveSignature.Status == Status.ModifiedButNotBackedUp))
                        {
                            try
                            {
                                loadDigitalSignature(documentToRemoveSignature.Path);
                                digitalSignature._RemoveAllSignatures();
                            }
                            catch (NullReferenceException)
                            {
                                documentToRemoveSignature.Status = Status.GenericError;
                            }
                            catch (FileFormatException)
                            {
                                documentToRemoveSignature.Status = Status.CorruptedContent;
                            }
                            catch (IOException)
                            {
                                documentToRemoveSignature.Status = Status.InUseByAnotherProcess;
                            }
                            catch (Exception)
                            {
                                documentToRemoveSignature.Status = Status.GenericError;
                            }

                            if (digitalSignature.DocumentType.Equals(Types.XpsDocument))
                            {
                                digitalSignature.xpsDocument.Close();
                            }
                            else if (digitalSignature.DocumentType.Equals(Types.PdfDocument))
                            {
                                // nothing to-do here
                            }
                            else
                            {
                                digitalSignature.package.Close();
                            }
                        }
                    }
                    break;
                case removeSignaturesType.removeSelectedSignatures:
                    List<FileStatus> removeSignatureStatusList = new List<FileStatus>();
                    FileStatus selectedFileStatus = null;
                    foreach (FileStatus documentToRemoveSignature in documentsRemoveSignStatus)
                    {
                        if (documentToRemoveSignature.Status == Status.Success
                                || documentToRemoveSignature.Status == Status.ModifiedButNotBackedUp)
                        {
                            foreach (Signers document in selectedSignaturesInDocuments)
                            {
                                if ((documentToRemoveSignature.OldPath == document.Path) || (document.Path == "commonSignatures"))
                                {
                                    try
                                    {
                                        string documentPath;
                                        string documentOldPath = "";
                                        if (document.Path != "commonSignatures")
                                        {
                                            documentOldPath = documentToRemoveSignature.OldPath;
                                            documentPath = documentToRemoveSignature.Path;
                                            foreach (Signer signer in document)
                                            {
                                                loadDigitalSignature(documentPath);
                                                Uri signUri = new Uri(signer.uri, UriKind.Relative);
                                                digitalSignature.RemoveUniqueSignatureFromFile(signUri, signer.serialNumber);
                                            }
                                        }
                                        else
                                        {
                                            documentOldPath = documentToRemoveSignature.OldPath;
                                            documentPath = documentToRemoveSignature.Path;
                                            foreach (Signer signer in document)
                                            {
                                                loadDigitalSignature(documentPath);
                                                string serialNumber = signer.serialNumber;
                                                digitalSignature.RemoveSignaturesFromFilesBySigner(serialNumber);
                                            }
                                        }
                                        if (chkCopyDocuments.Checked)
                                        {
                                            selectedFileStatus = new FileStatus(documentOldPath, documentPath, Status.Success);
                                        }
                                        else
                                        {
                                            selectedFileStatus = new FileStatus(documentOldPath, documentPath, Status.ModifiedButNotBackedUp);
                                        }
                                    }
                                    catch (NullReferenceException)
                                    {
                                        selectedFileStatus = new FileStatus(document.Path, Status.GenericError);
                                    }
                                    catch (FileFormatException)
                                    {
                                        selectedFileStatus = new FileStatus(document.Path, Status.CorruptedContent);
                                    }
                                    catch (IOException)
                                    {
                                        selectedFileStatus = new FileStatus(document.Path, Status.InUseByAnotherProcess);
                                    }
                                    catch (Exception)
                                    {
                                        selectedFileStatus = new FileStatus(document.Path, Status.GenericError);
                                    }

                                    if (!removeSignatureStatusList.Contains(selectedFileStatus))
                                    {
                                        removeSignatureStatusList.Add(selectedFileStatus);
                                    }
                                    //break;
                                }
                            }
                        }
                    }

                    foreach (FileStatus documentReadyStatus in documentsRemoveSignStatus)
                    {
                        if (removeSignatureStatusList.Contains(documentReadyStatus))
                            documentsRemoveSignStatus.Remove(documentReadyStatus);
                    }
                    break;
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
                if (MessageBox.Show(Constants.DontOverride, "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    chkOverwrite.Checked = true;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (chkCopyDocuments.Checked)
                LastBackedUpFolder.SetValue("LastBackUpFolder", txtPath.Text, RegistryValueKind.String);

            removeSignatures();
            viewReport(documentsRemoveSignStatus);

            List<FileHistory> files = new List<FileHistory>();
            foreach (FileStatus fs in documentsRemoveSignStatus)
            {
                FileHistory file = new FileHistory(fs.OldPath, fs.Path);
                files.Add(file);
            }
            string formOwner = this.Owner.ToString();
            int start = formOwner.IndexOf('.');
            int end = formOwner.IndexOf(',');
            formOwner = formOwner.Substring(start + 1, end - start - 1);
            if (formOwner == "frmSelectDigitalSignatureToRemove")
                ((frmSelectDigitalSignatureToRemove)this.Owner).listFiles(files);
            if (formOwner == "frmManageDigitalSignature")
                ((frmManageDigitalSignature)this.Owner).listFiles(files);
            this.Close();
        }

        #endregion
    }
}

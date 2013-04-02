using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FileUtils;

namespace AssinadorDigital
{
    public partial class frmReport : Form
    {
        List<FileStatus> documentsStatus = new List<FileStatus>();
        string actionPerformed;

        public frmReport(List<FileStatus> report, string action)
        {
            documentsStatus = report;
            actionPerformed = action;
            InitializeComponent();
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            switch (actionPerformed)
            {
                case "sign":
                    foreach (FileStatus fileStatus in documentsStatus)
                    {
                        ListViewItem lviDocumentStatus = new ListViewItem(fileStatus.Path);
                        lviDocumentStatus.SubItems.Add(fileStatus.OldPath);
                        switch (fileStatus.Status)
                        {
                            case Status.Success:
                                lviDocumentStatus.SubItems.Add("Documento copiado e assinado com sucesso.");
                                break;
                            case Status.SignatureAlreadyExists:
                                lviDocumentStatus.SubItems.Add("Documento copiado mas não assinado, assinatura já existente.");
                                break;
                            case Status.SignatureAlreadyExistsNotBackedUp:
                                lviDocumentStatus.SubItems.Add("Assinatura já existente, o documento não foi assinado.");
                                break;
                            case Status.Unmodified:
                                lviDocumentStatus.SubItems.Add("Documento já existente na pasta de destino. A assinatura não foi inserida.");
                                break;
                            case Status.ModifiedButNotBackedUp:
                                lviDocumentStatus.SubItems.Add("Documento assinado com sucesso.");
                                break;
                            case Status.UnauthorizedAccess:
                                lviDocumentStatus.SubItems.Add("Não foi possível copiar o documento, acesso não autorizado à pasta de destino. A assinatura não foi inserida.");
                                break;
                            case Status.PathTooLong:
                                lviDocumentStatus.SubItems.Add("Não foi possível copiar o documento, caminho do diretório muito longo. A assinatura não foi inserida.");
                                break;
                            case Status.InUseByAnotherProcess:
                                lviDocumentStatus.SubItems.Add("Não foi possível copiar o documento, verifique se ele não está em uso por outra aplicação. A assinatura não foi inserida.");
                                break;
                            case Status.CorruptedContent:
                                lviDocumentStatus.SubItems.Add("Não foi possível copiar o documento, o conteúdo do documento está corrompido (talvez seja um arquivo temporário). A assinatura não foi inserida.");
                                break;
                            default:
                                lviDocumentStatus.SubItems.Add("Não foi possível copiar o documento. A assinatura não foi inserida.");
                                break;
                        }
                        lstReport.Items.Add(lviDocumentStatus);
                    }
                    break;
                case "remove":
                    foreach (FileStatus fileStatus in documentsStatus)
                    {
                        ListViewItem lviDocumentStatus = new ListViewItem(fileStatus.Path);
                        lviDocumentStatus.SubItems.Add(fileStatus.OldPath);
                        switch (fileStatus.Status)
                        {
                            case Status.Success:
                                lviDocumentStatus.SubItems.Add("Documento copiado e assinatura removida com sucesso.");
                                break;
                            case Status.Unmodified:
                                lviDocumentStatus.SubItems.Add("Não foi possível copiar o documento, arquivo já existente. A assinatura não foi removida.");
                                break;
                            case Status.ModifiedButNotBackedUp:
                                lviDocumentStatus.SubItems.Add("Assinatura removida do documento com sucesso.");
                                break;
                            case Status.UnauthorizedAccess:
                                lviDocumentStatus.SubItems.Add("Não foi possível copiar o documento, acesso não autorizado à pasta de destino. A assinatura não foi removida.");
                                break;
                            case Status.PathTooLong:
                                lviDocumentStatus.SubItems.Add("Não foi possível copiar o documento, caminho do diretório muito longo. A assinatura não foi removida.");
                                break;
                            case Status.InUseByAnotherProcess:
                                lviDocumentStatus.SubItems.Add("Não foi possível copiar o documento, verifique se ele não está em uso por outra aplicação. A assinatura não foi removida.");
                                break;
                            case Status.CorruptedContent:
                                lviDocumentStatus.SubItems.Add("Não foi possível copiar o documento, o conteúdo do documento está corrompido (talvez seja um arquivo temporário). A assinatura não foi removida.");
                                break;
                            default:
                                lviDocumentStatus.SubItems.Add("Não foi possível copiar o documento. A assinatura não foi removida.");
                                break;
                        }
                        lstReport.Items.Add(lviDocumentStatus);
                    }
                    break;
            }
        }

        private void frmReport_FormClosed(object sender, FormClosedEventArgs e)
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
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

using System;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace AssinadorDigital
{
    public partial class FormSignatureDetails : Form
    {
        private X509Certificate2 signatureCertificate = new X509Certificate2();
        private ChainDocumentStatus chainStatus = new ChainDocumentStatus();

        public FormSignatureDetails(X509Certificate2 certificate, ChainDocumentStatus status)
        {
            signatureCertificate = certificate;
            chainStatus = status;

            InitializeComponent();
        }

        private void statusUpdate()
        {
            txtStatus.Text = chainStatus.StatusInformation;

            //txtStatus.Text = chainStatus.StatusText + "\r\n" +
            //   chainStatus.StatusInformation;

            switch (chainStatus.Status)
            {
                case ChainDocumentStatus.ChainDocumentStatusFlags.CorruptedDocument:
                    pctValidate.Image = ilistValidate.Images[0];
                    break;
                case ChainDocumentStatus.ChainDocumentStatusFlags.CtlNotSignatureValid:
                case ChainDocumentStatus.ChainDocumentStatusFlags.CtlNotTimeValid:
                case ChainDocumentStatus.ChainDocumentStatusFlags.CtlNotValidForUsage:
                case ChainDocumentStatus.ChainDocumentStatusFlags.Cyclic:
                case ChainDocumentStatus.ChainDocumentStatusFlags.HasExcludedNameConstraint:
                case ChainDocumentStatus.ChainDocumentStatusFlags.HasNotDefinedNameConstraint:
                case ChainDocumentStatus.ChainDocumentStatusFlags.HasNotPermittedNameConstraint:
                case ChainDocumentStatus.ChainDocumentStatusFlags.HasNotSupportedNameConstraint:
                case ChainDocumentStatus.ChainDocumentStatusFlags.InvalidBasicConstraints:
                case ChainDocumentStatus.ChainDocumentStatusFlags.InvalidExtension:
                case ChainDocumentStatus.ChainDocumentStatusFlags.InvalidNameConstraints:
                case ChainDocumentStatus.ChainDocumentStatusFlags.InvalidPolicyConstraints:
                case ChainDocumentStatus.ChainDocumentStatusFlags.NoIssuanceChainPolicy:
                case ChainDocumentStatus.ChainDocumentStatusFlags.NotSignatureValid:
                case ChainDocumentStatus.ChainDocumentStatusFlags.NotTimeNested:
                case ChainDocumentStatus.ChainDocumentStatusFlags.NotTimeValid:
                case ChainDocumentStatus.ChainDocumentStatusFlags.NotValidForUsage:
                case ChainDocumentStatus.ChainDocumentStatusFlags.OfflineRevocation:
                case ChainDocumentStatus.ChainDocumentStatusFlags.PartialChain:
                case ChainDocumentStatus.ChainDocumentStatusFlags.RevocationStatusUnknown:
                case ChainDocumentStatus.ChainDocumentStatusFlags.Revoked:
                case ChainDocumentStatus.ChainDocumentStatusFlags.UntrustedRoot:
                    pctValidate.Image = ilistValidate.Images[1];
                    break;
                case ChainDocumentStatus.ChainDocumentStatusFlags.NoError:
                default:
                    pctValidate.Image = ilistValidate.Images[2];
                    break;
            }
        }

        private void certificateUpdate()
        {
            ListViewGroup v1Group = new ListViewGroup("v1Group", "Campos");
            lstDetails.Groups.Insert(0, v1Group);

            ListViewItem itemSerialNumber = new ListViewItem("Número de série");
            itemSerialNumber.SubItems.Add(signatureCertificate.SerialNumber);
            itemSerialNumber.Group = v1Group;
            lstDetails.Items.Add(itemSerialNumber);

            ListViewItem itemIssuerName = new ListViewItem("Emissor");
            itemIssuerName.SubItems.Add(signatureCertificate.IssuerName.Name.Replace("CN=", "").Replace("OU=", "").Replace("DC=", "").Replace("O=", "").Replace("C=", ""));
            itemIssuerName.Group = v1Group;
            lstDetails.Items.Add(itemIssuerName);

            ListViewItem itemNotBefore = new ListViewItem("Válido a partir de");
            itemNotBefore.SubItems.Add(signatureCertificate.NotBefore.ToString());
            itemNotBefore.Group = v1Group;
            lstDetails.Items.Add(itemNotBefore);

            ListViewItem itemNotAfter = new ListViewItem("Válido até");
            itemNotAfter.SubItems.Add(signatureCertificate.NotAfter.ToString());
            itemNotAfter.Group = v1Group;
            lstDetails.Items.Add(itemNotAfter);

            ListViewItem itemSubject = new ListViewItem("Requerente");
            itemSubject.SubItems.Add(signatureCertificate.Subject.Replace("CN=", "").Replace("OU=", "").Replace("DC=", "").Replace("O=", "").Replace("C=", ""));
            itemSubject.Group = v1Group;
            lstDetails.Items.Add(itemSubject);

            ListViewItem itemFriendlyName = new ListViewItem("Nome amigável");
            itemFriendlyName.SubItems.Add(signatureCertificate.FriendlyName);
            itemFriendlyName.Group = v1Group;
            lstDetails.Items.Add(itemFriendlyName);

            ListViewGroup extensionsGroup = new ListViewGroup("extensions", "Extensões");
            lstDetails.Groups.Insert(1, extensionsGroup);
            foreach (X509Extension ext in signatureCertificate.Extensions)
            {
                ListViewItem item = new ListViewItem(ext.Oid.FriendlyName);
                item.SubItems.Add(ext.Format(true));
                item.Group = extensionsGroup;

                lstDetails.Items.Add(item);
            }
        }

        private void FormSignatureDetails_Load(object sender, EventArgs e)
        {
            certificateUpdate();
            statusUpdate();
            lstDetails.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            X509Certificate2UI.DisplayCertificate((X509Certificate2)signatureCertificate);
        }
    }
}

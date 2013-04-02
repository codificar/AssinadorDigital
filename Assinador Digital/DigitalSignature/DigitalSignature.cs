using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.IO.Packaging;
using System.Xml;
using System.Security;
using Microsoft.Office.DocumentFormat.OpenXml.Packaging;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Windows.Xps.Packaging;

using System.Windows.Forms;
using System.Drawing;

namespace OPC
{
    public class DigitalSignature:Helper
    {
        #region Private Constants, Fields  and Properties

        private const string wordmlNamespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main";
        private const string OfficeObjectID = "idOfficeObject";
        private const string SignatureID = "idPackageSignature";

        #endregion

        #region Public Fields

        public static String officeDocument;
        public ArrayList InvalidDigitalSignatureHolderNames = new ArrayList();

        public string filePath;

        #endregion

        #region Constructor

        /// <summary>
        /// Instanciates a wrapper class with helper methods for Digital Signatures Management on Open XML Packages
        /// </summary>
        /// <param name="filePath">A string containing the path to the Open Xml Package</param>
        /// <param name="type">Enum OpenXML.Type: indicates the type of the target document</param>
        public DigitalSignature(string filePath, Types type)
            : base(filePath, type)
        {
            this.filePath = filePath;
        }   
        #endregion

        #region Destructor

        /// <summary>
        /// Destructor that close the package
        /// </summary>
        ~DigitalSignature()
        {
            try
            {
                ClosePackage();
            }
            catch
            {                

            }           
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Method that makes two lists, parts to want Sign and indicate relationships to sign
        /// </summary>
        /// <param name="relationship">PackageRelationship</param>
        /// <param name="partsToSign"> Package Parts that you want to sign</param>
        /// <param name="relationshipsToSign">PacakgeRelationshipSelector objects which indicate relationships to sign</param>
        private void AddSignableItems(
            PackageRelationship relationship,
            List<Uri> partsToSign,
            List<PackageRelationshipSelector> relationshipsToSign)
        {
            PackageRelationshipSelector selector =
                new PackageRelationshipSelector(
                    relationship.SourceUri,
                    PackageRelationshipSelectorType.Id,
                    relationship.Id);
            relationshipsToSign.Add(selector);
            if (relationship.TargetMode == TargetMode.Internal)
            {
                PackagePart part = relationship.Package.GetPart(
                    PackUriHelper.ResolvePartUri(
                        relationship.SourceUri, relationship.TargetUri));
                if (partsToSign.Contains(part.Uri) == false)
                {
                    partsToSign.Add(part.Uri);
                    foreach (PackageRelationship childRelationship in
                        part.GetRelationships())
                    {
                        AddSignableItems(childRelationship,
                            partsToSign, relationshipsToSign);
                    }
                }
            }
        }

        /// <summary>
        /// Method that return the Object for fixing validation problem
        /// </summary>
        /// <param name="SignatureID">ID of Signature</param>
        /// <param name="ManifestHashAlgorithm">Hash Algorithm</param>
        /// <returns>DataObject</returns>
        private System.Security.Cryptography.Xml.DataObject CreateOfficeObject(string SignatureID, string ManifestHashAlgorithm)
        {
            XmlDocument document = new XmlDocument();            
            document.LoadXml(String.Format(officeDocument, SignatureID, ManifestHashAlgorithm));
            System.Security.Cryptography.Xml.DataObject officeObject = new System.Security.Cryptography.Xml.DataObject();
            // do not change the order of the following two lines
            officeObject.LoadXml(document.DocumentElement); // resets ID
            officeObject.Id = OfficeObjectID; // required ID, do not change
            return officeObject;
        }

        private void dsm_InvalidSignatureEvent(object sender, SignatureVerificationEventArgs e)
        {            
            InvalidDigitalSignatureHolderNames.Add(e.Signature.Signer.Subject.Replace("CN=", ""));
            InvalidDigitalSignatureHolderNames.Add(e.Signature.SignaturePart.Uri.ToString());
            InvalidDigitalSignatureHolderNames.Add(e.Signature.SigningTime.ToString());
            InvalidDigitalSignatureHolderNames.Add(e.Signature.Signer.Issuer.Replace("CN=", ""));
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Validates the Package Digital Signatures
        /// </summary>
        /// <returns>
        /// True if the Package is Valid;
        /// False is the Package is Invalid.
        /// </returns>
        public bool Validate()
        {
            if (DocumentType.Equals(Types.XpsDocument))
            {           
                int validate = 0;
                foreach (XpsDigitalSignature digitalSignature in
                  xpsDocument.Signatures)
                { 
                // Verify the signature object, if present.

                    if (digitalSignature.Verify() ==
                        VerifyResult.Success)
                    {                        
                        //Signature is valid
                    }
                    else
                    {   
                        //Signature is not valid
                        InvalidDigitalSignatureHolderNames.Add(digitalSignature.SignerCertificate.Subject.Replace("CN=", ""));
                        InvalidDigitalSignatureHolderNames.Add(digitalSignature.SignedDocumentSequence.Uri.ToString());
                        InvalidDigitalSignatureHolderNames.Add(digitalSignature.SigningTime.ToString());
                        InvalidDigitalSignatureHolderNames.Add(digitalSignature.SignerCertificate.Issuer.Replace("CN=", ""));
                        validate = 1;
                    }
                }
                if (validate == 1)
                    return false;
                else                
                    return true;
            }
            else if (DocumentType.Equals(Types.PdfDocument))
            {
                if (this.pdfSignatureList != null)
                {
                    foreach (CertificadoDigital.Signature sig in this.pdfSignatureList)
                        if (!sig.Valid) return false;
                    return true;
                }
                else
                {
                    return false;
                }
                
                
            }
            else
            {
                if (package == null)
                    throw new ArgumentNullException("ValidateSignatures(package)");

                PackageDigitalSignatureManager dsm =
                    new PackageDigitalSignatureManager(package);

                dsm.InvalidSignatureEvent += new InvalidSignatureEventHandler(dsm_InvalidSignatureEvent);

                //Checking for Signatures
                if (!dsm.IsSigned)
                    return false;

                VerifyResult result = dsm.VerifySignatures(false);
                if (result != VerifyResult.Success)
                    return false;

                return true;
            }
        }
        /// <summary>
        /// Method that get the XML necessary to fix the validation problem
        /// </summary>
        /// <param name="setOfficeDocument">XML that was stored in Resouces</param>
        public void SetOfficeDocument(String setedDocument)
        {
            officeDocument = setedDocument;
        }

        public bool DocumentHasSignature(X509Certificate2 certificate)
        {
            string signerSerialNumber = certificate.GetSerialNumberString();
            string serialNumber = "";

            if (DocumentType.Equals(Types.XpsDocument))
            {
                //Search the serial in a Xps signatures.
                foreach (XpsDigitalSignature signature in xpsDocument.Signatures)
                {
                    serialNumber = signature.SignerCertificate.GetSerialNumberString();
                    if (serialNumber == signerSerialNumber)
                        return true;
                }
            }
            else if (DocumentType.Equals(Types.PdfDocument))
            {
                if (this.pdfSignatureList == null)
                    return false;
                else
                {
                    foreach (CertificadoDigital.Signature sig in this.pdfSignatureList)
                    {
                        if (sig.X509Certificate.GetSerialNumberString() == signerSerialNumber)
                            return true;
                    }
                }
            }
            else
            {
                PackageDigitalSignatureManager _assinaturas = null;
                _assinaturas = new PackageDigitalSignatureManager(base.package);
                _assinaturas.CertificateOption = CertificateEmbeddingOption.InSignaturePart;

                foreach (PackageDigitalSignature signature in _assinaturas.Signatures)
                {
                    serialNumber = signature.Signer.GetSerialNumberString();
                    if (serialNumber == signerSerialNumber)
                        return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Method that Sign the document, he call the others methods and uses the lists and OfficeObject to Sign
        /// </summary>
        /// <param name="certificate">the digital certificate that will sign</param>
        public void SignDocument(X509Certificate2 certificate)
        {
            if (certificate != null)
            {
                if (DocumentType.Equals(Types.XpsDocument))
                {
                    xpsDocument.SignDigitally(
                    certificate, true, XpsDigSigPartAlteringRestrictions.None);
                }
                else if (DocumentType.Equals(Types.PdfDocument))
                {
                    CertificadoDigital.Sign.signFile(this.filePath, certificate);
                }
                else
                {
                    List<Uri> partsToSign = new List<Uri>();
                    List<PackageRelationshipSelector> relationshipsToSign =
                        new List<PackageRelationshipSelector>();
                    List<Uri> finishedItems = new List<Uri>();
                    foreach (PackageRelationship relationship in
                        package.GetRelationshipsByType(RT_OfficeDocument))
                    {
                        AddSignableItems(relationship,
                            partsToSign, relationshipsToSign);
                    }
                    PackageDigitalSignatureManager mgr = new PackageDigitalSignatureManager(package);
                    mgr.CertificateOption = CertificateEmbeddingOption.InSignaturePart;

                    string signatureID = SignatureID;
                    string manifestHashAlgorithm = ManifestHashAlgorithm;
                    System.Security.Cryptography.Xml.DataObject officeObject = CreateOfficeObject(signatureID, manifestHashAlgorithm);
                    Reference officeObjectReference = new Reference("#" + OfficeObjectID);
                    mgr.Sign(partsToSign, certificate,
                    relationshipsToSign, signatureID,
                    new System.Security.Cryptography.Xml.DataObject[] { officeObject },
                    new Reference[] { officeObjectReference });
                }
            }
        }
        /// <summary>
        /// Method that remove a signer of the file
        /// </summary>
        /// <param name="signer">Name of the signer</param>
        public void RemoveUniqueSignatureFromFile(Uri siguri, string serialNumber = null)  
        {

            if (DocumentType.Equals(Types.XpsDocument))
            {
                //To remove the signature we opened XPS like System.IO.Package,
                //Because the URI is necessary to remove the correct signature
                xpsDocument.Close();
                package = Package.Open(signers.Path, FileMode.Open, FileAccess.ReadWrite);

                PackageDigitalSignatureManager _signatures = null;
                _signatures = new PackageDigitalSignatureManager(package);
                _signatures.CertificateOption = CertificateEmbeddingOption.InSignaturePart;

                PackageDigitalSignatureManager _assinaturas = null;
                _assinaturas = new PackageDigitalSignatureManager(base.package);
                _assinaturas.CertificateOption = CertificateEmbeddingOption.InSignaturePart;
                _assinaturas.RemoveSignature(siguri);
                package.Flush();
                package.Close();
                
            }
            else if (DocumentType.Equals(Types.PdfDocument))
            {
                CertificadoDigital.Remove.removeSignature(pdfDocumentPath, serialNumber);
            }
            else
            {
                PackageDigitalSignatureManager _assinaturas = null;
                _assinaturas = new PackageDigitalSignatureManager(base.package);
                _assinaturas.CertificateOption = CertificateEmbeddingOption.InSignaturePart;
                _assinaturas.RemoveSignature(siguri);
                package.Flush();
                package.Close();
            }
        }

        public void RemoveSignaturesFromFilesBySigner(string signerSerialNumber)
        {
            string signuri = "";
            string serialNumber = "";

            if (DocumentType.Equals(Types.XpsDocument))
            {
                //Search the serial in a Xps signatures.
                foreach (XpsDigitalSignature signature in xpsDocument.Signatures)
                {
                    serialNumber = signature.SignerCertificate.GetSerialNumberString();
                    if (serialNumber == signerSerialNumber)
                    {
                        xpsDocument.RemoveSignature(signature);
                    }                   
                }
                serialNumber = null;
                xpsDocument.Close();
            }
            else if (DocumentType.Equals(Types.PdfDocument))
            {
                CertificadoDigital.Remove.removeSignature(pdfDocumentPath, serialNumber);
            }
            else
            {

                PackageDigitalSignatureManager _assinaturas = null;
                _assinaturas = new PackageDigitalSignatureManager(base.package);
                _assinaturas.CertificateOption = CertificateEmbeddingOption.InSignaturePart;
                int signaturesCount = _assinaturas.Signatures.Count;
                for (int index = 0; index < _assinaturas.Signatures.Count; index++)
                {
                    PackageDigitalSignature signature = _assinaturas.Signatures[index];
                    serialNumber = signature.Signer.GetSerialNumberString();
                    if (serialNumber == signerSerialNumber)
                    {
                        signuri = signature.SignaturePart.Uri.ToString();
                        Uri uri = new Uri(signuri, UriKind.Relative);
                        _assinaturas.RemoveSignature(uri);
                        index--;
                    }
                }
                package.Flush();
                package.Close();
            }
        }

        /// <summary>
        /// Method that remove all signers of the file
        /// </summary>
        public void _RemoveAllSignatures()
        {
            if (DocumentType.Equals(Types.XpsDocument))
            {
                foreach (XpsDigitalSignature signature in xpsDocument.Signatures)
                {
                    xpsDocument.RemoveSignature(signature);
                }
            }
            else if (DocumentType.Equals(Types.PdfDocument))
            {
                CertificadoDigital.Remove.removeSignature(pdfDocumentPath);
            }
            else
            {
                PackageDigitalSignatureManager _assinaturas = null;
                _assinaturas = new PackageDigitalSignatureManager(base.package);
                _assinaturas.CertificateOption = CertificateEmbeddingOption.InSignaturePart;
                _assinaturas.RemoveAllSignatures();
                package.Flush();
                package.Close();
            }
        }
        #endregion

        #region InputDialog

        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        #endregion
    }
}

using System;
using System.Text;
using System.Collections;
using System.IO;
using System.IO.Packaging;
using Microsoft.Office.DocumentFormat.OpenXml.Packaging;
using System.Xml;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Xps.Packaging;

namespace OPC
{
    public class Helper:Signers
    {   
        #region Private Constants, Fields and Properties

        protected NameTable nt = new NameTable();
        protected XmlNamespaceManager nsManager;             

        #endregion

        #region Public Constants and Fields

        public const string RT_OfficeDocument = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument";
        public const string ManifestHashAlgorithm = "http://www.w3.org/2000/09/xmldsig#sha1";
        public const string relationshipNamespace = "http://schemas.openxmlformats.org/officeDocument/2006/relationships";
        public const string documentRelationshipType = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument";
        public const string signatureNamespace = "http://www.w3.org/2000/09/xmldsig#";
        public Signers signers = new Signers();
        public DigitalSignatureOriginPart digSigOrigin;
        public Package package = null;
        public XpsDocument xpsDocument = null;
        public Types DocumentType;      
        public bool error = false;

        #endregion

        #region Construtor

        public Helper(string filePath, Types type)
        {
            nsManager = new XmlNamespaceManager(nt);
            //Adicionar o Namespace da documentRelationshipType
            nsManager.AddNamespace("r", relationshipNamespace);
            nsManager.AddNamespace("ns1", signatureNamespace);
            DocumentType = type;
            signers.Path = filePath;

            if (DocumentType.Equals(Types.XpsDocument))
            {
                if (xpsDocument == null)
                {
                    //Open a XPSDocument
                    xpsDocument = new XpsDocument(filePath, FileAccess.ReadWrite);
                    SetDigitalSignatureOriginPartbyDocumentType();
                }
            }
            else
            {
                if (package == null)
                {   //Open a OpenXML
                    try
                    {
                        package = Package.Open(signers.Path, FileMode.Open, FileAccess.ReadWrite);
                        SetDigitalSignatureOriginPartbyDocumentType();
                    }
                    catch (NullReferenceException e)
                    {
                        throw new NullReferenceException(e.Message,e.InnerException);
                    }
                    catch (FileFormatException  e)
                    {
                        throw new FileFormatException(e.Message,e.InnerException);
                    }
                }
            }
        }

        #endregion
        
        #region Private Methods

        private void SetDigitalSignatureOriginPartbyDocumentType()
        {
                if (DocumentType == Types.WordProcessingML)
                    digSigOrigin = WordprocessingDocument.Open(package).DigitalSignatureOriginPart;

                if (DocumentType == Types.SpreadSheetML)

                    digSigOrigin = SpreadsheetDocument.Open(package).DigitalSignatureOriginPart;

                if (DocumentType == Types.PresentationML)
                    digSigOrigin = PresentationDocument.Open(package).DigitalSignatureOriginPart;

                signers = GetDigitalSigners();
        }   
        #endregion

        #region Public Methods

        public List<XmlSignaturePart> GetDigitalSignatures()
        {
            List<XmlSignaturePart> xmlSignaturePartList = new List<XmlSignaturePart>();
            if (digSigOrigin != null)
            {
                foreach (XmlSignaturePart xmlSignaturePart in digSigOrigin.XmlSignatureParts)
                {
                    xmlSignaturePartList.Add(xmlSignaturePart);
                }
            }
            return xmlSignaturePartList;
        }

        public List<X509Certificate2> GetDigitalSignatureCertificates()
        {
            List<X509Certificate2> certificateList;
            certificateList = new List<X509Certificate2>();
            List<XmlSignaturePart> xmlSignaturePartList = this.GetDigitalSignatures();
            foreach (XmlSignaturePart xmlSigntature in xmlSignaturePartList)
            {
                XmlDocument xDoc = new XmlDocument(nt);
                xDoc.Load(xmlSigntature.GetStream());
                XmlNode xNode = xDoc.SelectSingleNode("//ns1:X509Certificate", nsManager);
                byte[] certificate = Convert.FromBase64String(xNode.InnerText);
                certificateList.Add(new X509Certificate2(certificate));
            }
            return certificateList;
        }

        /// <summary>
        /// Return a List of Signer in the package
        /// </summary>
        /// <returns>
        /// Package Path (string), Signer Name (string), Signer URI (string), Signer Issuer (string)
        /// </returns>
        public Signers GetDigitalSigners()
        {
            Signers sigs = new Signers();
            sigs.Path = signers.Path;

            List<X509Certificate2> certificateList = new List<X509Certificate2>();
            List<XmlSignaturePart> xmlSignaturePartList = this.GetDigitalSignatures();

            if (DocumentType.Equals(Types.XpsDocument))
            {
                //To collect the information of the signature we used XPS like a System.IO.Packaging
             
                xpsDocument.Close();
                package = Package.Open(signers.Path, FileMode.Open, FileAccess.Read);
                
                PackageDigitalSignatureManager _signatures = null;
                _signatures = new PackageDigitalSignatureManager(package);
                _signatures.CertificateOption = CertificateEmbeddingOption.InSignaturePart;

                // Add the signers in the list
                foreach (PackageDigitalSignature signature in _signatures.Signatures)
                {
                    string name = signature.Signer.Subject.Replace("CN=", "");
                    string uri = signature.SignaturePart.Uri.ToString();
                    string date = signature.SigningTime.ToString();
                    string issuer = signature.Signer.Issuer.Replace("CN=", "");
                    string serial = signature.Signer.GetSerialNumberString();
                    X509Certificate2 signatureCertificate = (X509Certificate2)signature.Signer;

                    sigs.Add(name, uri, issuer, date, serial, signatureCertificate);
                }
                package.Close();            
                xpsDocument = new XpsDocument(signers.Path, FileAccess.ReadWrite);
                return sigs;
            }
            else
            {
                PackageDigitalSignatureManager _signatures = null;
                _signatures = new PackageDigitalSignatureManager(package);
                _signatures.CertificateOption = CertificateEmbeddingOption.InSignaturePart;

                // Add the signers in the list
                foreach (PackageDigitalSignature signature in _signatures.Signatures)
                {
                    string name = signature.Signer.Subject.Replace("CN=", "");
                    string uri = signature.SignaturePart.Uri.ToString();
                    string date = signature.SigningTime.ToString();
                    string issuer = signature.Signer.Issuer.Replace("CN=", "");
                    string serial = signature.Signer.GetSerialNumberString();
                    X509Certificate2 signatureCertificate = (X509Certificate2)signature.Signer;

                    sigs.Add(name, uri, issuer, date, serial, signatureCertificate);
                }
                return sigs;
            }
        }

        /// <summary>
        /// Close the package file
        /// </summary>
        public void ClosePackage()
        {
            if(xpsDocument==null)
            {
                xpsDocument.Close();
                xpsDocument = null;
            }
            else{
                package.Flush();
                package.Close();
            }
        }        
        #endregion
    } 
}

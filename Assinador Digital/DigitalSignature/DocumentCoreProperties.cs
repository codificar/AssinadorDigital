using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.IO.Packaging;
using Microsoft.Office.DocumentFormat.OpenXml.Packaging;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Xps.Packaging;

namespace OPC
{

   public class DocumentCoreProperties
    {
        #region Public Properties

        private ArrayList _DocumentProperties = new ArrayList();

        public ArrayList DocumentProperties
        {
            get { return _DocumentProperties; }
            set {  }
        }
           
        #endregion       

        #region Public Methods
        /// <summary>
        /// Get the File Properties (Metadata)
        /// Creator, Last Modifier, Title, Description, Subject, Created Date, Modified Date
        /// </summary>
        /// <returns>Returns a ArrayList containing the file properties</returns>
        public DocumentCoreProperties(Package package,XpsDocument xpsDocument, Types type, string pdfFile, CertificadoDigital.SignatureList pdfSigList)
        {

            if (type.Equals(Types.XpsDocument))
            {
                if (xpsDocument.CoreDocumentProperties.Creator != null)
                {
                    DocumentProperties.Add(xpsDocument.CoreDocumentProperties.Creator.ToString());
                }
                else
                    DocumentProperties.Add("");
                if (xpsDocument.CoreDocumentProperties.LastModifiedBy != null)
                {
                    DocumentProperties.Add(xpsDocument.CoreDocumentProperties.LastModifiedBy.ToString());
                }
                else
                    DocumentProperties.Add("");
                if (xpsDocument.CoreDocumentProperties.Title != null)
                {
                    DocumentProperties.Add(xpsDocument.CoreDocumentProperties.Title.ToString());
                }
                else
                    DocumentProperties.Add("");
                if (xpsDocument.CoreDocumentProperties.Description != null)
                {
                    DocumentProperties.Add(xpsDocument.CoreDocumentProperties.Description.ToString());
                }
                else
                    DocumentProperties.Add("");
                if (xpsDocument.CoreDocumentProperties.Subject!=null)
                {
                    DocumentProperties.Add(xpsDocument.CoreDocumentProperties.Subject.ToString());
                }
                else
                    DocumentProperties.Add("");
                if (xpsDocument.CoreDocumentProperties.Created != null)
                {
                    DocumentProperties.Add(xpsDocument.CoreDocumentProperties.Created.ToString());
                }
                else
                    DocumentProperties.Add("");
                if (xpsDocument.CoreDocumentProperties.Modified != null)
                {
                    DocumentProperties.Add(xpsDocument.CoreDocumentProperties.Modified.ToString());
                }
                else
                    DocumentProperties.Add("");

            }
            else if (type.Equals(Types.PdfDocument))
            {

                /*****                 
                 *   Creator
                 *   LastModifiedBy
                 *   Title
                 *   Description
                 *   Subject
                 *   Created
                 *   Modified
                 *****/

                if (pdfSigList == null)
                {
                    try
                    {
                        pdfSigList = CertificadoDigital.Validate.validateFile(pdfFile);
                    }
                    catch (CertificadoDigital.NoSignatureFoundException nsfe) { }

                }

                if (pdfSigList != null && pdfSigList.Count > 0 & pdfSigList[0] != null)
                {
                    CertificadoDigital.FileProperties[] prop = 
                    {   
                        CertificadoDigital.FileProperties.Author,      
                        CertificadoDigital.FileProperties.LastModifiedBy,  
                        CertificadoDigital.FileProperties.Title,
                        CertificadoDigital.FileProperties.Description,
                        CertificadoDigital.FileProperties.Subject,
                        CertificadoDigital.FileProperties.CreationDate,
                        CertificadoDigital.FileProperties.ModDate
                    };

                    for (int i = 0; i < prop.Length; i++)
                    {
                        if (prop[i] == CertificadoDigital.FileProperties.CreationDate || prop[i] == CertificadoDigital.FileProperties.ModDate)
                        {
                            DocumentProperties.Add(
                                pdfSigList[0].FileProperties.ContainsKey(prop[i]) ?
                                DateTime.ParseExact(pdfSigList[0].FileProperties[prop[i]].Substring(2, 8), "yyyyMMdd", null).ToString("dd-MMM-yy") :
                                string.Empty);
                        }
                        else
                        {
                            DocumentProperties.Add(
                                pdfSigList[0].FileProperties.ContainsKey(prop[i]) ? pdfSigList[0].FileProperties[prop[i]] : string.Empty);
                        }

                    }
                }
                else
                {
                    for (int i = 0; i < 7; i++)
                        DocumentProperties.Add("");
                }
                
            }
            else
            {
                const String coreRelType = @"http://schemas.openxmlformats.org/package/2006/relationships/metadata/core-properties";
                PackagePart corePart = null;
                Uri documentUri = null;

                foreach (PackageRelationship relationship in package.GetRelationshipsByType(coreRelType))
                {
                    documentUri = PackUriHelper.ResolvePartUri(new Uri("/", UriKind.Relative), relationship.TargetUri);
                    corePart = package.GetPart(documentUri);
                    break; //There is only one part
                }

                if (corePart != null)
                {
                    NameTable nt = new NameTable();
                    XmlNamespaceManager nsmgr = new XmlNamespaceManager(nt);
                    nsmgr.AddNamespace("dc", "http://purl.org/dc/elements/1.1/");
                    nsmgr.AddNamespace("cp", "http://schemas.openxmlformats.org/package/2006/metadata/core-properties");
                    nsmgr.AddNamespace("dcterms", "http://purl.org/dc/terms/");

                    XmlDocument doc = new XmlDocument(nt);
                    doc.Load(corePart.GetStream());

                    XmlNode nodeCreator = doc.DocumentElement.SelectSingleNode("//dc:creator", nsmgr);
                    if (nodeCreator != null)
                        DocumentProperties.Add(nodeCreator.InnerText);
                    else
                        DocumentProperties.Add("");

                    XmlNode nodeModifier = doc.DocumentElement.SelectSingleNode("//cp:lastModifiedBy", nsmgr);
                    if (nodeModifier != null)
                        DocumentProperties.Add(nodeModifier.InnerText);
                    else
                        DocumentProperties.Add("");

                    XmlNode nodeTitle = doc.DocumentElement.SelectSingleNode("//dc:title", nsmgr);
                    if (nodeTitle != null)
                        DocumentProperties.Add(nodeTitle.InnerText);
                    else
                        DocumentProperties.Add("");

                    XmlNode nodeDescription = doc.DocumentElement.SelectSingleNode("//dc:description", nsmgr);
                    if (nodeDescription != null)
                        DocumentProperties.Add(nodeDescription.InnerText);
                    else
                        DocumentProperties.Add("");

                    XmlNode nodeSubject = doc.DocumentElement.SelectSingleNode("//dc:subject", nsmgr);
                    if (nodeSubject != null)
                        DocumentProperties.Add(nodeSubject.InnerText);
                    else
                        DocumentProperties.Add("");

                    XmlNode nodeCreatedDate = doc.DocumentElement.SelectSingleNode("//dcterms:created", nsmgr);
                    if (nodeCreatedDate != null)
                        DocumentProperties.Add(DateTime.Parse(nodeCreatedDate.InnerText).ToShortDateString());
                    else
                        DocumentProperties.Add("");

                    XmlNode nodeModifiedDate = doc.DocumentElement.SelectSingleNode("//dcterms:modified", nsmgr);
                    if (nodeModifiedDate != null)
                        DocumentProperties.Add(DateTime.Parse(nodeModifiedDate.InnerText).ToShortDateString());
                    else
                        DocumentProperties.Add("");
                }
            }
        }
        #endregion       
        
    }
}

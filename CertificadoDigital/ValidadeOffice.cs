using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.IO.Packaging;

// system's certificates
using System.Security.Cryptography.X509Certificates;

// office packages
using Microsoft.Office.DocumentFormat.OpenXml.Packaging;

// xps
using System.Windows.Xps.Packaging;

namespace CertificadoDigital
{
    class ValidadeOffice
        : Validate
    {

        /// <summary>
        /// Valida arquivos Office geral
        /// </summary>
        /// <param name="filePath">Caminho do arquivo a ser validado</param>
        /// <param name="format">Formato do documento a ser validado</param>
        /// <param name="onlyRoot">
        /// Determina se irá considerar apenas certificados de Autoridades Certificadoras.
        /// True: Considera apenas certificados de Autoridades Certificadoras.
        /// False: Considera todos os certificados instalados na máquina.
        /// Default: True.
        /// </param>
        /// <returns>Lista de assinaturas, válidas ou não</returns>        
        internal static SignatureList validate(string filePath, FileFormat format, bool onlyRoot = true)
        {
            try
            {

                // list to return
                SignatureList list = new SignatureList();

                // open the file
                Package package;
                try
                {
                    package = Package.Open(filePath, FileMode.Open, FileAccess.Read);
                }
                catch (NullReferenceException e)
                {
                    throw new NullReferenceException(e.Message, e.InnerException);
                }
                 
                // get the signatures
                PackageDigitalSignatureManager _dsm = new PackageDigitalSignatureManager(package);
                _dsm.CertificateOption = CertificateEmbeddingOption.InSignaturePart;

                // if no signatures found
                if (_dsm.Signatures == null || _dsm.Signatures.Count == 0)
                    throw new NoSignatureFoundException();

                // verify all the signatures
                foreach (PackageDigitalSignature signature in _dsm.Signatures)
                {

                    list.Add
                        (
                        new Signature
                            (
                                filePath, // file path
                                format, // file format
                                getObjective(signature.Signature.GetXml().OuterXml), // objective                           
                                getSubject(signature.Signer.Subject), // subject
                                signature.SigningTime, // date time                                                      
                                validateCertificate((X509Certificate2)signature.Signer, true), // validate the certificate
                                getCertificates((X509Certificate2)signature.Signer, true) // certificate
                            )
                            {
                                SX509Certificate = (X509Certificate2)signature.Signer,
                                SFileProperties = getFileProperties(package.PackageProperties)
                            }
                        );

                }

                // close the file            
                package.Close();

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Valida arquivos XPS
        /// </summary>
        /// <param name="filePath">Caminho do arquivo a ser validado</param>        
        /// <param name="onlyRoot">
        /// Determina se irá considerar apenas certificados de Autoridades Certificadoras.
        /// True: Considera apenas certificados de Autoridades Certificadoras.
        /// False: Considera todos os certificados instalados na máquina.
        /// Default: True.
        /// </param>
        /// <returns>Lista de assinaturas, válidas ou não</returns>        
        internal static SignatureList validateXps(string filePath, bool onlyRoot = true)
        {

            SignatureList list = new SignatureList();

            // open the file
            XpsDocument xpsDocument = new XpsDocument(filePath, FileAccess.Read);

            if (xpsDocument.Signatures == null || xpsDocument.Signatures.Count == 0)
                throw new NoSignatureFoundException();

            foreach (XpsDigitalSignature digitalSignature in xpsDocument.Signatures)
            {
                list.Add
                (
                    new Signature
                    (
                        filePath,
                        FileFormat.XpsDocument,
                        null,
                        getSubject(digitalSignature.SignerCertificate.Subject),
                        digitalSignature.SigningTime,
                        digitalSignature.VerifyCertificate() == X509ChainStatusFlags.NoError,
                        getCertificates((X509Certificate2)digitalSignature.SignerCertificate, true)
                    )
                    {
                        SX509Certificate = (X509Certificate2)digitalSignature.SignerCertificate,
                        SFileProperties = getFileProperties(xpsDocument.CoreDocumentProperties)
                    }
                );
            }

            xpsDocument.Close();
            
            return list;

        }

        /// <summary>
        /// Faz o parser do Subject no objeto lido para o formato corrente
        /// </summary>
        /// <param name="strSubject">string do Objeto lido</param>
        /// <returns>Retorna um Subject</returns>
        private static Subject getSubject(string strSubject)
        {
            
            /****
             * codificar string subject sample
             * CN=CODIFICAR SISTEMAS TECNOLOGICOS LTDA ME:05957264000151, 
             * OU=Autenticado por PRODEMGE, 
             * OU=RFB e-CNPJ A3, 
             * OU=Secretaria da Receita Federal do Brasil - RFB, 
             * L=Belo Horizonte, 
             * S=MG, 
             * O=ICP-Brasil, 
             * C=BR
             ****/
            
            Subject sub = new Subject();
            
            // list of possible values
            /*List<Tuple<SubjectType, string>> list = new List<Tuple<SubjectType, string>>
            {
                new Tuple<SubjectType, string>(SubjectType.Country, "C"),
                new Tuple<SubjectType, string>(SubjectType.Organization, "O"),
                new Tuple<SubjectType, string>(SubjectType.State, "S"),
                new Tuple<SubjectType, string>(SubjectType.Locality, "L"),
                new Tuple<SubjectType, string>(SubjectType.OrganizationalUnit, "OU"),
                new Tuple<SubjectType, string>(SubjectType.CommonName, "CN"),
            };

            // adjust the string to find all
            strSubject = ", " + strSubject;

            // find all the items on the string
            List<int> posList = new List<int>();

            foreach(Tuple<SubjectType, string> item in list)
            {
                int i = -1;
                
                do
                {
                    i = strSubject.IndexOf(", " + item.Item2 + "=", i + 1);
                    if (i != -1) posList.Add(i);
                } while (i != -1);

            }*/

            // list of possible values
            Dictionary<SubjectType, string> list = new Dictionary<SubjectType, string>();
            list.Add(SubjectType.Country, "C");
            list.Add(SubjectType.Organization, "O");
            list.Add(SubjectType.State, "S");
            list.Add(SubjectType.Locality, "L");
            list.Add(SubjectType.OrganizationalUnit, "OU");
            list.Add(SubjectType.CommonName, "CN");

            // adjust the string to find all
            strSubject = ", " + strSubject;

            // find all the items on the string
            List<int> posList = new List<int>();

            foreach(KeyValuePair<SubjectType, string> item in list)
            {
                int i = -1;
                
                do
                {
                    i = strSubject.IndexOf(", " + item.Value + "=", i + 1);
                    if (i != -1) posList.Add(i);
                } while (i != -1);

            }

            // sort the list
            posList.Sort();

            // "split" the string and get the values by type
            for (int i = 0; i < posList.Count; i++)
            {

                // calculate the begin and the end of substring
                int init = strSubject.IndexOf("=", posList[i]) + 1;
                int end = i < posList.Count - 1 ? posList[i + 1] : strSubject.Length;

                // get the value
                string aux = strSubject.Substring(init, end - init);

                // get the type of subject
                string type = strSubject.Substring(posList[i] + 2, init - posList[i] - 3);

                SubjectType t =
                    (
                        from st in list
                        where st.Value == type
                        select st.Key
                    ).First();

                sub.Add(new SubjectDetail(t, aux));

            }

            return sub;

        }

        /// <summary>
        /// Obtém o Objetivo da Assinatura a partir do xml
        /// </summary>
        /// <param name="xml">XML da assinatura</param>
        /// <returns>string para o Objetivo</returns>
        private static string getObjective(string xml)
        {
            string _objective = string.Empty;

            string begin = "<SignatureComments>";
            string end = "</SignatureComments>";

            int b = xml.IndexOf(begin);
            int e = xml.IndexOf(end);

            if (b != -1 && e != -1)
                _objective = xml.Substring(b + begin.Length, e - b - begin.Length);

            return (_objective.Trim() != null && _objective.Trim() != string.Empty) ? _objective : null;
        }

        /// <summary>
        /// Obtém a cadeia de certificados da assinatura
        /// </summary>
        /// <param name="certificate"></param>
        /// <param name="checkCRL"></param>
        /// <returns></returns>
        private static CertificateList getCertificates(X509Certificate2 certificate, bool checkCRL)
        {

            CertificateList list = new CertificateList();

            X509Chain chain = getCertificateChain(certificate, checkCRL);

            chain.Build(certificate);

            foreach(X509ChainElement cert in chain.ChainElements)
            {
                list.Add(
                    new Certificate(
                            getSubject(cert.Certificate.Subject), 
                            cert.Certificate.NotBefore, 
                            cert.Certificate.NotAfter, 
                            cert.Certificate.SerialNumber.ToString(), 
                            cert.Certificate.SignatureAlgorithm.FriendlyName, 
                            cert.Certificate.Version)
                    );
            }

            return list;

        }

        /// <summary>
        /// Obtém as propriedades de um arquivo office comum
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        private static Dictionary<FileProperties, string> getFileProperties(PackageProperties properties)
        {
        
            Dictionary<FileProperties, string> ret = new Dictionary<FileProperties, string>();

            if (properties.Creator != null && properties.Creator != string.Empty)
                ret.Add(FileProperties.Author, properties.Creator);

            if (properties.LastModifiedBy != null && properties.LastModifiedBy != string.Empty)
                ret.Add(FileProperties.LastModifiedBy, properties.LastModifiedBy);

            if (properties.Title != null && properties.Title != string.Empty)
                ret.Add(FileProperties.Title, properties.Title);

            if (properties.Description != null && properties.Description != string.Empty)
                ret.Add(FileProperties.Description, properties.Description);

            if (properties.Created.HasValue)
                ret.Add(FileProperties.CreationDate, properties.Created.Value.ToString(Constants.DateTimeFormat));

            if(properties.Modified.HasValue)
                ret.Add(FileProperties.ModDate, properties.Modified.Value.ToString(Constants.DateTimeFormat));

            return ret;
        }
    }
}

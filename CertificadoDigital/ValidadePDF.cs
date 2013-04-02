using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// system's certificates
using System.Security.Cryptography.X509Certificates;

// itextsharp
using BCX = Org.BouncyCastle.X509;
using BCT = Org.BouncyCastle.Tsp;
using BCO = Org.BouncyCastle.Ocsp;
using BCS = Org.BouncyCastle.X509.Store;
using BCA = Org.BouncyCastle.Asn1;
using iTextSharp.text;
using TS = iTextSharp.text.pdf;
using TSS = iTextSharp.text.pdf.security;

namespace CertificadoDigital
{

    /// <summary>
    /// Validação de Arquivos PDF
    /// </summary>
    class ValidadePDF
        : Validate
    {

        /// <summary>
        /// Valida arquivos PDF
        /// </summary>
        /// <param name="filePath">Caminho do arquivo a ser validado</param>
        /// <param name="onlyRoot">
        /// Determina se irá considerar apenas certificados de Autoridades Certificadoras.
        /// True: Considera apenas certificados de Autoridades Certificadoras.
        /// False: Considera todos os certificados instalados na máquina.
        /// Default: True.
        /// </param>
        /// <returns>Lista de assinaturas, válidas ou não</returns>
        internal static SignatureList validate(string filePath, bool onlyRoot = true)
        {
            try
            {

                #region [obsolete code]
                /*
                 * 
                // list of valid certificates
                List<BCX.X509Certificate> kall = new List<BCX.X509Certificate>();

                // get the root certificates
                getSystemCertificates(StoreName.Root, StoreLocation.CurrentUser, ref kall);

                // if not only root, get others certificates 
                if (!onlyRoot)
                {
                    getSystemCertificates(StoreName.AddressBook, StoreLocation.CurrentUser, ref kall);
                    getSystemCertificates(StoreName.AuthRoot, StoreLocation.CurrentUser, ref kall);
                    getSystemCertificates(StoreName.CertificateAuthority, StoreLocation.CurrentUser, ref kall);
                    getSystemCertificates(StoreName.My, StoreLocation.CurrentUser, ref kall);
                    getSystemCertificates(StoreName.TrustedPeople, StoreLocation.CurrentUser, ref kall);
                    getSystemCertificates(StoreName.TrustedPublisher, StoreLocation.CurrentUser, ref kall);
                }
                 * */
                #endregion

                // open the pdf file
                TS.PdfReader reader = new TS.PdfReader(filePath);

                // get the fields inside the file
                TS.AcroFields af = reader.AcroFields;

                // get the signatures
                List<string> names = af.GetSignatureNames();

                // if don't found signature
                if (names == null || names.Count == 0)
                    throw new NoSignatureFoundException();

                // signatures to return
                SignatureList signatures = new SignatureList();

                // for each signature in pdf file
                foreach (string name in names)
                {

                    // verify the signature
                    TSS.PdfPKCS7 pk = af.VerifySignature(name);

                    // get the datetime of signature
                    DateTime cal = pk.SignDate;
                    cal = (pk.TimeStampToken != null ? pk.TimeStampDate : cal);
                     
                    // create the signature
                    Signature sig = new Signature
                        (
                            filePath, // file path
                            FileFormat.PDFDocument, // pdf format
                            pk.Reason, // objective                           
                            getSubject(pk.SigningCertificate.SubjectDN), // subject
                            cal, // date time                            
                            //verifySignature(pk.SignCertificateChain, kall, cal, pk.SigningCertificate), // signature validate, obsolete
                            verifySignature(pk.SigningCertificate),
                            getSignatureCertificates(pk.SignCertificateChain) // get the certificates
                        );

                    // set the x509certificates
                    sig.SX509Certificate = convertCertificate(pk.SigningCertificate);

                    // set the issuer
                    sig.SIssuer = pk.SigningCertificate.IssuerDN.ToString();

                    // set the file properties
                    foreach (KeyValuePair<string, string> prop in reader.Info)
                    {
                        FileProperties? fp = null;
                        try
                        {
                            fp = (FileProperties)Enum.Parse(typeof(FileProperties), prop.Key, true);
                        }
                        catch{ }

                        if (fp.HasValue)
                            sig.addProperties(fp.Value, prop.Value);
                    }

                    // add signature to the list
                    signatures.Add(sig);

                }

                return signatures;

            }
            catch(Exception ex)
            {
                throw ex;                
            }
        }

        /// <summary>
        /// Formata o assunto no padrão codificar
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        private static Subject getSubject(BCA.X509.X509Name subject)
        {

            /**                 
            * Codificar Signature Sample
            * C=BR,
            * O=ICP-Brasil,
            * ST=MG,
            * L=Belo Horizonte,
            * OU=Secretaria da Receita Federal do Brasil - RFB,
            * OU=RFB e-CNPJ A3,
            * OU=Autenticado por PRODEMGE,
            * CN=CODIFICAR SISTEMAS TECNOLOGICOS LTDA ME:05957264000151
            **/

            /*List<Tuple<SubjectType, BCA.DerObjectIdentifier>> listSubject = new List<Tuple<SubjectType, BCA.DerObjectIdentifier>>
            {
                new Tuple<SubjectType, BCA.DerObjectIdentifier>(SubjectType.Country, iTextSharp.text.pdf.security.CertificateInfo.X509Name.C),
                new Tuple<SubjectType, BCA.DerObjectIdentifier>(SubjectType.Organization, iTextSharp.text.pdf.security.CertificateInfo.X509Name.O),
                new Tuple<SubjectType, BCA.DerObjectIdentifier>(SubjectType.State, iTextSharp.text.pdf.security.CertificateInfo.X509Name.ST),
                new Tuple<SubjectType, BCA.DerObjectIdentifier>(SubjectType.Locality, iTextSharp.text.pdf.security.CertificateInfo.X509Name.L),
                new Tuple<SubjectType, BCA.DerObjectIdentifier>(SubjectType.OrganizationalUnit, iTextSharp.text.pdf.security.CertificateInfo.X509Name.OU),
                new Tuple<SubjectType, BCA.DerObjectIdentifier>(SubjectType.CommonName, iTextSharp.text.pdf.security.CertificateInfo.X509Name.CN),
            };

            Subject sub = new Subject();

            foreach (Tuple<SubjectType, BCA.DerObjectIdentifier> item in listSubject)            
                foreach (string obj in subject.GetValueList(item.Item2))                
                    sub.Add(new SubjectDetail(item.Item1, obj));

            return sub;*/

            Dictionary<SubjectType, BCA.DerObjectIdentifier> listSubject = new Dictionary<SubjectType, BCA.DerObjectIdentifier>();

            listSubject.Add(SubjectType.Country, iTextSharp.text.pdf.security.CertificateInfo.X509Name.C);
            listSubject.Add(SubjectType.Organization, iTextSharp.text.pdf.security.CertificateInfo.X509Name.O);
            listSubject.Add(SubjectType.State, iTextSharp.text.pdf.security.CertificateInfo.X509Name.ST);
            listSubject.Add(SubjectType.Locality, iTextSharp.text.pdf.security.CertificateInfo.X509Name.L);
            listSubject.Add(SubjectType.OrganizationalUnit, iTextSharp.text.pdf.security.CertificateInfo.X509Name.OU);
            listSubject.Add(SubjectType.CommonName, iTextSharp.text.pdf.security.CertificateInfo.X509Name.CN);

            Subject sub = new Subject();

            foreach (KeyValuePair<SubjectType, BCA.DerObjectIdentifier> pair in listSubject)            
                foreach (string obj in subject.GetValueList(pair.Value))                
                    sub.Add(new SubjectDetail(pair.Key, obj));

            return sub;
        }
        
        /// <summary>
        /// Obtém os certificados no Sistema Operacional
        /// </summary>
        /// <param name="sn">"Tipo" de certificados</param>
        /// <param name="sl">"Escopo" dos certificados: usuário, máquina ...</param>
        /// <param name="kall">Variável de referência com a lista dos certificados</param>
        private static void getSystemCertificates(StoreName sn, StoreLocation sl, ref List<BCX.X509Certificate> kall)
        {

            // "dealing" with the system certificates
            BCX.X509CertificateParser parser = new BCX.X509CertificateParser();

            // get the certificates store
            X509Store st = new X509Store(sn, sl);
            st.Open(OpenFlags.ReadOnly);

            // get the certificates and close the store
            X509Certificate2Collection col = st.Certificates;
            st.Close();

            // add the certificates to the list
            foreach (X509Certificate2 cert in col)
            {
                BCX.X509Certificate c2 = parser.ReadCertificate(cert.GetRawCertData());
                kall.Add(c2);
            }

        }

        /// <summary>
        /// Verifica a validade da assinatura (obsoleta)
        /// </summary>
        /// <param name="pkc">Cadeia de assinaturas</param>
        /// <param name="kall">Lista de certificados</param>
        /// <param name="cal">Data/Hora da assinatura</param>
        /// <returns></returns>
        private static bool verifySignature(BCX.X509Certificate[] pkc, List<BCX.X509Certificate> kall, DateTime cal, BCX.X509Certificate signer)
        {
            Object[] fails = iTextSharp.text.pdf.security.CertificateVerification.VerifyCertificates(pkc, kall, null, cal);
            return (fails == null);
        }

        /// <summary>
        /// Verifica a validade da assinatura
        /// </summary>
        /// <param name="signer">Certificado da assinatura</param>
        /// <returns></returns>
        private static bool verifySignature(BCX.X509Certificate signer)
        {   
            return validateCertificate(convertCertificate(signer), true);
        }

        /// <summary>
        /// Obtém a cadeia de certificados de uma assinatura;
        /// </summary>
        /// <param name="chain"></param>
        /// <returns></returns>
        private static CertificateList getSignatureCertificates(BCX.X509Certificate[] chain)
        {

            CertificateList list = new CertificateList();

            for (int i = 0; i < chain.Length; i++)
            {
                list.Add(
                    new Certificate
                    (   
                        getSubject(chain[i].SubjectDN),
                        chain[i].NotBefore.ToLocalTime(),
                        chain[i].NotAfter.ToLocalTime(),
                        chain[i].SerialNumber.ToString(16).ToUpper(),
                        chain[i].SigAlgName,
                        chain[i].Version
                    )
                );
            }

            return list;

        }

        /// <summary>
        /// Converte um certificado Bouncy.Castle em System.Security
        /// </summary>
        /// <param name="sig"></param>
        /// <returns></returns>
        private static System.Security.Cryptography.X509Certificates.X509Certificate2 convertCertificate(BCX.X509Certificate sig)
        {
            X509Certificate2 certificate = new X509Certificate2();
            certificate.Import(sig.GetEncoded());

            return certificate;
        }
    }
}

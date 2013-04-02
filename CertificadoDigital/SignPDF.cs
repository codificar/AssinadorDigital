using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

// system's certificates
using System.Security.Cryptography.X509Certificates;

// itextsharp
using BCX = Org.BouncyCastle.X509;
using BCT = Org.BouncyCastle.Tsp;
using BCO = Org.BouncyCastle.Ocsp;
using BCS = Org.BouncyCastle.X509.Store;
using BCA = Org.BouncyCastle.Asn1;
using BCSe = Org.BouncyCastle.Security;
using iTextSharp.text;
using TS = iTextSharp.text.pdf;
using TSS = iTextSharp.text.pdf.security;

namespace CertificadoDigital
{
    /// <summary>
    /// Assinatura de arquivos PDF
    /// </summary>
    class SignPDF
        : Sign
    {
        /// <summary>
        /// Assina o arquivo PDF
        /// </summary>
        /// <param name="filePath">Caminho do arquivo</param>
        /// <param name="certificate">Certificado</param>        
        /// <param name="reason">Motivo da assinatura</param>
        internal static void sign(string filePath, X509Certificate2 certificate, string reason = null)
        {
            try
            {
                // make the certificate chain
                IList <BCX.X509Certificate> chain = getCertChain(certificate);

                // open the original file
                TS.PdfReader reader = new TS.PdfReader(filePath);

                string newFilePath = filePath.Substring(0, filePath.Length - 4) + "_signed.pdf";
                
                // create a new file
                FileStream fout = new FileStream(newFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);

                // create the "stamp" on the file
                TS.PdfStamper stamper = TS.PdfStamper.CreateSignature(reader, fout, '\0', null, true);
                TS.PdfSignatureAppearance appearance = stamper.SignatureAppearance;
                appearance.Reason = reason;
                appearance.Location = getLocation(certificate.Subject);

                int i = 1;
                int xdiff = 0;

                while (true)
                {
                    string fieldName = "Assinatura" + i.ToString(); ;
                    try
                    {
                        appearance.SetVisibleSignature(new iTextSharp.text.Rectangle(20 + xdiff, 10, 170 + xdiff, 60), 1, fieldName);

                        TSS.X509Certificate2Signature es = new TSS.X509Certificate2Signature(certificate, "SHA-1");
                        TSS.MakeSignature.SignDetached(appearance, es, chain, null, null, null, 0, TSS.CryptoStandard.CMS);
                        break;
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message != "The field " + fieldName + " already exists.")
                            throw ex;
                        else
                        {
                            i++;
                            xdiff += 180;
                        }
                    }
                }

                // close the files
                reader.Close();                
                fout.Close();

                // delete the tmp file e move the new to the right name
                System.IO.File.Delete(filePath);
                System.IO.File.Move(newFilePath, filePath);
                

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtém a cadeia de certificados
        /// </summary>
        /// <param name="cert">Certificado</param>
        /// <returns></returns>
        private static IList<BCX.X509Certificate> getCertChain(X509Certificate2 cert)
        {

            IList<BCX.X509Certificate> chain = new List<BCX.X509Certificate>();
            X509Chain x509chain = new X509Chain();
            x509chain.Build(cert);

            foreach (X509ChainElement el in x509chain.ChainElements)
                chain.Add(BCSe.DotNetUtilities.FromX509Certificate(el.Certificate));

            return chain;
        }

        /// <summary>
        /// Monta a localização a partir do certificado
        /// </summary>
        /// <param name="subject">Assunto</param>
        /// <returns></returns>
        private static string getLocation(string subject)
        {

            subject += ",";

            string location = string.Empty;
            string state = string.Empty;
            string country = string.Empty;

            // location
            int l1 = subject.IndexOf("L=");
            int l2 = subject.IndexOf(",", l1 + 1);
            if (l1 != -1 && l2 != -1)
                location = subject.Substring(l1 + 2, l2 - l1 - 2);

            // state
            int s1 = subject.IndexOf("S=");
            int s2 = subject.IndexOf(",", s1 + 1);
            if (s1 != -1 && s2 != -1)
                state = subject.Substring(s1 + 2, s2 - s1 - 2);

            // country
            int c1 = subject.IndexOf("C=");
            int c2 = subject.IndexOf(",", c1 + 1);
            if (c1 != -1 && c2 != -1)
                country = subject.Substring(c1 + 2, c2 - c1 - 2);

            string ret =
                (location != string.Empty ? location + " - " : string.Empty) +
                (state != string.Empty ? state + " - " : string.Empty) +
                (country != string.Empty ? country + " - " : string.Empty);

            return ret.Substring(0, ret.Length - 3);

        }

    }
}

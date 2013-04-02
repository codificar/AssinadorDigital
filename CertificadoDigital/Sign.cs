using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

// system certificates
using System.Security.Cryptography.X509Certificates;

namespace CertificadoDigital
{
    /// <summary>
    /// Assina um documento
    /// </summary>
    public class Sign
    {

        /// <summary>
        /// Obtém o certificado e assina o documento
        /// </summary>
        /// <param name="filePath">Caminho do documento a ser assinado</param>        
        /// <param name="reason">Motivo da assinatu</param>
        public static void signFile(string filePath, string reason = null)
        {
            signFile(filePath, General.FileFormatFromString(Path.GetExtension(filePath)), reason);
        }

        /// <summary>
        /// Obtém o certificado e assina o documento
        /// </summary>
        /// <param name="filePath">Caminho do arquivo</param>
        /// <param name="format">Formato do arquivo</param>        
        /// <param name="reason">Motivo da assinatura</param>
        public static void signFile(string filePath, FileFormat format, string reason = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Assina um documento a partir do caminho e do certificado
        /// </summary>
        /// <param name="filePath">Caminho do arquivo</param>
        /// <param name="certificate">Certificado a ser utilizado.</param>        
        /// <param name="reason">Motivo da assinatura</param>
        public static void signFile(string filePath, X509Certificate2 certificate, string reason = null)
        {
            if (filePath == null || filePath.Trim().Length == 0)
                throw new ArgumentNullException(Constants.ErrInvalidPath);
            else if (certificate == null)
                throw new ArgumentNullException(Constants.ErrInvalidCertificate);
            else
                signFile(filePath, General.FileFormatFromString(Path.GetExtension(filePath)), certificate, reason);                
        }

        /// <summary>
        /// Assina um documento a partir do caminho e do certificado
        /// </summary>
        /// <param name="filePath">Caminho do documento a ser assinado</param>
        /// <param name="format">Formato do arquivo</param>
        /// <param name="certificate">Certificado a ser utilizado</param>        
        /// <param name="reason">Motivo da assinatura</param>
        public static void signFile(string filePath, FileFormat format, X509Certificate2 certificate, string reason = null)
        {

            if (filePath == null || filePath.Trim().Length == 0)
                throw new ArgumentNullException(Constants.ErrInvalidPath);
            else if (certificate == null)
                throw new ArgumentNullException(Constants.ErrInvalidCertificate);
            else
            {
                if (format == FileFormat.PDFDocument)
                {
                    SignPDF.sign(filePath, certificate, reason);
                }
                else
                {
                    throw new NotImplementedException("Only add PDF signatures are implemented in this library.");
                }
            }

        }

    }
}

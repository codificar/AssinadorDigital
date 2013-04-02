using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

// system's certificates
using System.Security.Cryptography.X509Certificates;

namespace CertificadoDigital
{

    /// <summary>
    /// Executa as validações em um documento
    /// </summary>
    public class Validate
    {
        
        /// <summary>
        /// Retorna as assinaturas de um documento
        /// </summary>
        /// <param name="filePath">Caminho do arquivo a ser verificado</param>
        /// <returns>Assinaturas do documento</returns>
        public static SignatureList validateFile(string filePath)
        {
            if (filePath == null || filePath.Trim().Length == 0)
                throw new ArgumentNullException(Constants.ErrInvalidPath);
            else
                return validateFile(filePath, General.FileFormatFromString(Path.GetExtension(filePath)));
        }

        /// <summary>
        /// Retorna as assinaturas de um documento
        /// </summary>
        /// <param name="filePath">Caminho do arquivo a ser verificado</param>
        /// <param name="fileName">Nome do arquivo a ser verificado</param>
        /// <returns>Assinaturas do documento</returns>
        public static SignatureList validateFile(string filePath, string fileName)
        {

            if (filePath == null || filePath.Trim().Length == 0)
                throw new ArgumentNullException(Constants.ErrInvalidPath);
            else
                return validateFile(filePath, General.FileFormatFromString(Path.GetExtension(fileName)));
        }

        /// <summary>
        /// Retorna as assinaturas de um documento
        /// </summary>
        /// <param name="filePath">Caminho do arquivo a ser verificado</param>
        /// <param name="format">Formato do arquivo a ser verificado</param>
        /// <returns>Assinaturas do documento</returns>
        public static SignatureList validateFile(string filePath, FileFormat format)
        {
            try
            {
                // if filePath not informed
                if (filePath == null || filePath.Trim().Length == 0)
                    throw new ArgumentNullException(Constants.ErrInvalidPath);
                else
                {
                    if (format == FileFormat.PDFDocument)
                        return ValidadePDF.validate(filePath);

                    else if (format == FileFormat.WordProcessingML)
                        return ValidadeOffice.validate(filePath, format);

                    else if (format == FileFormat.SpreadSheetML)
                        return ValidadeOffice.validate(filePath, format);

                    else if (format == FileFormat.PresentationML)
                        return ValidadeOffice.validate(filePath, format);

                    else if (format == FileFormat.XpsDocument)
                        return ValidadeOffice.validateXps(filePath);

                    else
                        throw new InvalidFileFormatException();
                }

            }
            catch (IOException ioex)
            {
                throw new IOException("Erro ao abrir o arquivo: " + ioex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Valida o certificado
        /// </summary>
        /// <param name="certificate">Certificado a ser validado</param>
        /// <param name="checkCRL">Verifica certificados on-line</param>
        /// <returns></returns>
        protected static bool validateCertificate(X509Certificate2 certificate, bool checkCRL)
        {
            return getCertificateChain(certificate, checkCRL).Build(certificate);
        }

        /// <summary>
        /// Obtém a cadeia de certificados a partir de um certificado
        /// </summary>
        /// <param name="certificate"></param>
        /// <param name="checkCRL"></param>
        /// <returns></returns>
        protected static X509Chain getCertificateChain(X509Certificate2 certificate, bool checkCRL)
        {
            X509Chain chain = new X509Chain();

            chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
            chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 1, 0);
            chain.ChainPolicy.VerificationFlags = X509VerificationFlags.NoFlag;
            chain.ChainPolicy.VerificationTime = DateTime.Now;

            if (checkCRL)
                chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
            else
            {
                chain.ChainPolicy.VerificationFlags = X509VerificationFlags.IgnoreEndRevocationUnknown;
                chain.ChainPolicy.RevocationMode = X509RevocationMode.Offline;
            }

            return chain;

        }

    }

}

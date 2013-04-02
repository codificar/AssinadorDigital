using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace CertificadoDigital
{
    /// <summary>
    /// Remove assinaturas de um documento
    /// </summary>
    public class Remove
    {

        /// <summary>
        /// Remove a assinatura
        /// </summary>
        /// <param name="filePath">caminho do arquivo</param>        
        public static void removeSignature(string filePath)
        {
            if (filePath == null || filePath.Trim().Length == 0)
                throw new ArgumentNullException(Constants.ErrInvalidPath);
            else
                removeSignature(filePath, General.FileFormatFromString(Path.GetExtension(filePath)));
        }

        /// <summary>
        /// Remove a assinatura
        /// </summary>
        /// <param name="filePath">caminho do arquivo</param>
        /// <param name="format">formato</param>
        public static void removeSignature(string filePath, FileFormat format)
        {
            if (filePath == null || filePath.Trim().Length == 0)
                throw new ArgumentNullException(Constants.ErrInvalidPath);
            else if (format != FileFormat.PDFDocument)
                throw new NotImplementedException("Only remove PDF signatures are implemented in this library.");
            else
                RemovePdf.remove(filePath);
        }

        /// <summary>
        /// Remove uma assinatura específica no documento
        /// </summary>
        /// <param name="filePath">Caminho do arquivo</param>
        /// <param name="serialNumber">Serial do assinador a ser removido</param>
        public static void removeSignature(string filePath, string serialNumber)
        {
            if (filePath == null || filePath.Trim().Length == 0 || serialNumber == null || serialNumber.Trim().Length == 0)
                throw new ArgumentNullException(Constants.ErrInvalidPath);
            else
                removeSignature(filePath, serialNumber, General.FileFormatFromString(Path.GetExtension(filePath)));
        }

        /// <summary>
        /// Remove uma assinatura específica no documento
        /// </summary>
        /// <param name="filePath">Caminho do arquivo</param>
        /// <param name="serialNumber">Serial do assinador a ser removido</param>
        /// <param name="format">Formato do arquivo</param>
        public static void removeSignature(string filePath, string serialNumber, FileFormat format)
        {
            if (filePath == null || filePath.Trim().Length == 0)
                throw new ArgumentNullException(Constants.ErrInvalidPath);
            else if (format != FileFormat.PDFDocument)
                throw new NotImplementedException("Only remove PDF signatures are implemented in this library.");
            else
                RemovePdf.remove(filePath, serialNumber);
        }

    }

}

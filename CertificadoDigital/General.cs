using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CertificadoDigital
{
    /// <summary>
    /// Classe com funções estáticas genéricas
    /// </summary>
    internal class General
    {
        
        /// <summary>
        /// Determina o FileFormat a partir de uma string
        /// </summary>
        /// <param name="fileExtension"></param>
        /// <returns></returns>
        internal static FileFormat FileFormatFromString(string fileExtension)
        {
            if (fileExtension == ".pdf" || fileExtension == "pdf")
                return FileFormat.PDFDocument;
            else if (fileExtension == ".docx" || fileExtension == ".docm" || fileExtension == "docx" || fileExtension == "docm")
                return FileFormat.WordProcessingML;
            else if (fileExtension == ".pptx" || fileExtension == ".pptm" || fileExtension == "pptx" || fileExtension == "pptm")
                return FileFormat.PresentationML;
            else if (fileExtension == ".xlsx" || fileExtension == ".xlsm" || fileExtension == "xlsx" || fileExtension == "xlsm")
                return FileFormat.SpreadSheetML;
            else if (fileExtension == ".xps" || fileExtension == "xps")
                return FileFormat.XpsDocument;
            else
                throw new InvalidFileFormatException();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;

using System.Collections;
using System.IO;

namespace CertificadoDigital
{
    /// <summary>
    /// Remove assinaturas de documentos PDF
    /// </summary>
    class RemovePdf
        : Remove
    {

        /// <summary>
        /// Remove a assinatura de um documento PDF
        /// </summary>
        internal static void remove(string filePath, string serialNumber = null)
        {

            try
            {
                // create a tmp file to remove
                string newFilePath = filePath.Substring(0, filePath.Length - 4) + "_tmp.pdf";
                System.IO.File.Copy(filePath, newFilePath);

                // open the file
                PdfReader reader = new PdfReader(newFilePath);

                // get the fields inside the file
                AcroFields af = reader.AcroFields;

                // get the list of signatures
                List<string> names = af.GetSignatureNames();

                if (names.Count == 0)
                {
                    reader.Close();
                    throw new NoSignatureFoundException();
                }

                // create the stream to file
                MemoryStream mStream = new MemoryStream();

                // open the file to edit
                PdfStamper stamper = new PdfStamper(reader, mStream);
                AcroFields af2 = stamper.AcroFields;

                // close the reader file
                reader.Close();

                if (serialNumber == null)
                {
                    // remove all signatures
                    for (int i = 0; i < names.Count; i++)
                    {
                        //af2.ClearSignatureField(names[i].ToString());
                        af2.RemoveField(names[i].ToString());
                    }
                }
                else
                {
                    // find and remove the selected signature
                    for (int i = 0; i < names.Count; i++)
                    {
                        PdfPKCS7 pk = af.VerifySignature(names[i]);

                        if (pk.SigningCertificate.SerialNumber.ToString(16).ToUpper() == serialNumber)
                        {
                            //af2.ClearSignatureField(names[i].ToString());
                            af2.RemoveField(names[i].ToString());
                        }
                    }
                }

                // clear the stream of obejct                                
                reader.RemoveUnusedObjects();
                
                // close the stamper file                
                stamper.Writer.CloseStream = false;
                stamper.Close();

                // save file
                File.WriteAllBytes(newFilePath, mStream.ToArray());

                // delete the tmp file e move the new to the right name
                System.IO.File.Delete(filePath);
                System.IO.File.Move(newFilePath, filePath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

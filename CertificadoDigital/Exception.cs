using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CertificadoDigital
{
    /// <summary>
    /// Exceção para assinatura não encontrada.
    /// </summary>
    public class NoSignatureFoundException
        : Exception
    {
        public NoSignatureFoundException()
            : base(Constants.ErrNoSignatureFound)
        {
            return;
        }
    }

    /// <summary>
    /// Exceção para formato inválido
    /// </summary>
    public class InvalidFileFormatException
        : Exception
    {
        public InvalidFileFormatException()
            : base(Constants.ErrInvalidFormat)
        {
            return;
        }
    }
}

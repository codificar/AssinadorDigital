using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace OPC
{
    public class Signer
    {
        #region Public Fields and Constants

        public string name;
        public string uri;
        public string issuer;
        public string serialNumber;
        public string date;
        public bool isValid;
        public X509Certificate2 signerCertificate;

        #endregion

        #region Constructor

        public Signer()
        {

        }
        #endregion
    }
}

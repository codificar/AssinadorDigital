using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace OPC
{
    public class Signers : CollectionBase, IEnumerable, IEnumerator 
    {
        #region Private Constants, Fields  and Properties

        private int index = -1;
        private string path;

        #endregion

        #region Public Constants and Fields

        /// <summary>
        /// Gets or Sets the Signer value in Signers[i]
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Signer this[int index]
        {
            get { return (Signer)this.List[index]; }
            set { this.List[index] = value; }
        }
        /// <summary>
        /// Gets or Sets the Path string of the document
        /// </summary>
        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        #endregion

        #region Constructor

        public Signers()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Add the signer parameters in the Signers list
        /// </summary>
        /// <param name="name"></param>
        /// <param name="uri"></param>
        /// <param name="issuer"></param>
        /// <param name="date"></param>
        /// <param name="serial"></param>
        public void Add(string name, string uri, string issuer, string date, string serial, X509Certificate2 signerCertificate)
        {
            MoveNext();
            Signer sig = new Signer();
            sig.name = name;
            sig.date = date;
            sig.uri = uri;
            sig.issuer = issuer;
            sig.serialNumber = serial;
            sig.signerCertificate = signerCertificate;
            this.List.Add(sig);
        }
        /// <summary>
        /// Add the signer parameter in the Signers list
        /// </summary>
        /// <param name="signer"></param>
        public void Add(Signer signer)
        {
            MoveNext();
            this.List.Add(signer);
        }
        /// <summary>
        /// Remove the signer parameter from the Signers list
        /// </summary>
        /// <param name="signer"></param>
        public void Remove(Signer signer)
        {
            this.List.Remove(signer);
        }
        /// <summary>
        /// Verify if the Signers list contains the signer parameter
        /// Compare the signature name + signature issuer + serial number
        /// </summary>
        /// <param name="signer"></param>
        /// <returns></returns>
        public bool Contains(Signer signer)
        {
            foreach (Signer sgn in this.InnerList)
            {
                if ((sgn.serialNumber == signer.serialNumber) &&
                    (sgn.issuer == signer.issuer) &&
                    (sgn.name == signer.name))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Compare the serial string parameter with each Signer.serialNumber in Signers list
        /// and returns true if the Signers list has the serial number
        /// </summary>
        /// <param name="serial">The serial number related with the digital signature</param>
        /// <returns></returns>
        public bool HasSerialNumber(string serial)
        {
            foreach (Signer sgn in this.InnerList)
            {
                if (sgn.serialNumber == serial)
                    return true;
            }
            return false;
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        #endregion

        #region IEnumerator Members

        public Object Current
        {
            get
            {
                return this.List[index];
            }
        }

        public bool MoveNext()
        {
            this.index++;
            return (this.index < this.List.Count);
        }

        public void Reset()
        {
            this.index = -1;
        }

        #endregion
    }
}

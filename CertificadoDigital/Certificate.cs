using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Linq;

namespace CertificadoDigital
{

    /// <summary>
    /// Representa o certificado em uma assinatura
    /// </summary>    
    public class Certificate
    {

        #region [Constructor]

        /// <summary>
        /// Construtor vazio
        /// </summary>
        internal Certificate()
        {
        }

        /// <summary>
        /// Construtor Interno
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="serial"></param>
        /// <param name="algorithm"></param>
        /// <param name="version"></param>
        internal Certificate
        (   
            Subject subject,
            DateTime startDate,
            DateTime endDate,
            string serial,
            string algorithm,
            int version
        )
        {

            if (subject == null)
                throw new ArgumentNullException();

            this._subject = subject;

            if (startDate != null)
                this._startDate = startDate;

            if (endDate != null)
                this._endDate = endDate;

            if (serial != null && serial != string.Empty)
            {
                if (serial.Length == 1) serial = "0" + serial;

                this._serial = serial;                
            }

            if(algorithm != null && algorithm != string.Empty)
                this._algorithm = this.convertAlgorithm(algorithm);

            if (version != null)
                this._version = version;
        }

        #endregion

        #region [Variables]

        private Subject _subject;
        private DateTime _startDate;
        private DateTime _endDate;
        private string _serial;
        private string _algorithm;
        private int _version;
        
        #endregion

        #region [Properties]

        /// <summary>
        /// Nome do Certificado
        /// </summary>
        public string Name
        {
            get
            {
                return this.Subject.CommonName;
            }
        }

        /// <summary>
        /// Assunto do Certificado
        /// </summary>
        internal Subject Subject
        {
            get
            {
                return this._subject;
            }
        }

        /// <summary>
        /// Organização responsável pelo Certificado
        /// </summary>
        public string Organization
        {
            get
            {
                return this.Subject.Organization;
            }
        }

        /// <summary>
        /// Relação das organizações que ratificam o certificado
        /// </summary>
        public List<string> OrganizationUnit
        {
            get
            {
                return this.Subject.OrganizationUnit;
            }
        }

        /// <summary>
        /// Data inicial de validade do certificado
        /// </summary>
        public DateTime StartDate
        {
            get
            {
                return this._startDate;
            }
        }

        /// <summary>
        /// Data final de validade do certificado
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                return this._endDate;
            }
        }

        /// <summary>
        /// Identificador Serial do Certificado
        /// </summary>
        public string Serial
        {
            get
            {
                return this._serial;
            }
        }

        /// <summary>
        /// Algoritmo usado no certificado
        /// </summary>
        public string Algorithm
        {
            get
            {
                return this._algorithm;
            }
        }

        /// <summary>
        /// Versão do Certificado
        /// </summary>
        public int Version
        {
            get
            {
                return this._version;
            }
        }

        #endregion

        #region [InternalProperties - Setters]

        internal Subject SSubject
        {
            set
            {
                this._subject = value;
            }
        }

        internal DateTime SStartDate
        {
            set
            {
                this._startDate = value;
            }
        }

        internal DateTime SEndDate
        {
            set
            {
                this._endDate = value;
            }
        }

        internal string SSerial
        {
            set
            {
                this._serial = value;
            }
        }

        internal string SAlgorithm
        {
            set
            {
                this._algorithm = this.convertAlgorithm(value);
            }
        }

        internal int SVersion
        {
            set
            {
                this._version = value;
            }
        }

        #endregion

        #region [Methods]

        /// <summary>
        /// Converte o Certificado em String
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string _s = string.Empty;

            _s +=
                "Name=" + this.Name +
                ";Organization=" + this.Organization;

            for (int i = 0; i < this.OrganizationUnit.Count; i++)
                _s += ";OrganizationUnit" + (i + 1) + "=" + this.OrganizationUnit[i];

            _s +=
                ";StartDate=" + this.StartDate.ToString(Constants.DateTimeFormat) +
                ";EndDate=" + this.EndDate.ToString(Constants.DateTimeFormat) +
                ";Serial=" + this.Serial +
                ";Algorithm=" + this.Algorithm +
                ";Version=" + this.Version;

            return _s;
                
        }

        /// <summary>        
        /// Converte o certificado em uma string de XML
        /// </summary>
        /// <returns></returns>
        public string toXml()
        {
            return this._toXml().ToString();
        }

        /// <summary>
        /// Converte o certificado em um XML
        /// </summary>
        /// <returns></returns>
        public XElement _toXml()
        {
            XElement _xml =
                new XElement("Certificate",
                    new XAttribute("Value", this.Name),
                    new XElement("Name", this.Name),
                    new XElement("Organization", this.Organization),
                    new XElement("OrganizationUnits",
                        (
                            from ou in this.OrganizationUnit
                            orderby ou
                            select new XElement("OrganizationUnit", ou.ToString())
                        )
                    ),
                    new XElement("StartDate", this.StartDate.ToString(Constants.DateTimeFormat)),
                    new XElement("EndDate", this.EndDate.ToString(Constants.DateTimeFormat)),
                    new XElement("Serial", this.Serial),
                    new XElement("Algorithm", this.Algorithm),
                    new XElement("Version", this.Version)
                );

            return _xml;
        }

        /// <summary>
        /// Converte um XElement em Certificate
        /// </summary>
        /// <returns></returns>
        public static Certificate fromXml(XElement c)
        {
            return new Certificate
            {
                SStartDate = DateTime.ParseExact(c.Element("StartDate").Value, Constants.DateTimeFormat, null),
                SEndDate = DateTime.ParseExact(c.Element("EndDate").Value, Constants.DateTimeFormat, null),
                SSerial = c.Element("Serial").Value,
                SAlgorithm = c.Element("Algorithm").Value,
                SVersion = Convert.ToInt32(c.Element("Version").Value),
                SSubject = (
                    new Subject
                    {
                        Organization = c.Element("Organization").Value,
                        CommonName = c.Element("Name").Value,
                        OrganizationUnit =
                        (
                            from ou in c.Element("OrganizationUnits").Elements("OrganizationUnit")
                            select ou.Value
                        ).ToList()
                    }
                )
            };
        }

        /// <summary>
        /// Converte o algoritmo para o padrão.
        /// </summary>
        /// <param name="key">Chave para o algoritmo.</param>
        /// <returns>Valor padrozinado.</returns>
        private string convertAlgorithm(string key)
        {

            Dictionary<string, string> algorithms = new Dictionary<string, string>();

            algorithms.Add("MD2withRSA",                "md2RSA");            
            algorithms.Add("MD4withRSA",                "md4RSA");            
            algorithms.Add("MD5withRSA",                "md5RSA");
            algorithms.Add("SHA-1withRSA",              "sha1RSA");            
            algorithms.Add("SHA-224withRSA",            "sha224RSA");            
            algorithms.Add("SHA-256withRSA",            "sha256RSA");           
            algorithms.Add("SHA-384withRSA",            "sha384RSA");           
            algorithms.Add("SHA-512withRSA",            "sha512RSA");            
            algorithms.Add("PSSwithRSA",                "pssRSA");           
            algorithms.Add("SHA-1withRSAandMGF1",       "sha1RSAMGF1");            
            algorithms.Add("SHA-224withRSAandMGF1",     "sha224RSAMGF1");            
            algorithms.Add("SHA-256withRSAandMGF1",     "sha256RSAMGF1");            
            algorithms.Add("SHA-384withRSAandMGF1",     "sha384RSAMGF1");      
            algorithms.Add("SHA-512withRSAandMGF1",     "sha512RSAMGF1");            
            algorithms.Add("RIPEMD128withRSA",          "ripemd128RSA");                        
            algorithms.Add("RIPEMD160withRSA",          "ripemd160RSA");                        
            algorithms.Add("RIPEMD256withRSA",          "ripemd256RSA");                        
            algorithms.Add("RSA",                       "RSA");            
            algorithms.Add("RAWRSASSA-PSS",             "rawRSASSA-PSS");            
            algorithms.Add("NONEwithDSA",               "noneDSA");            
            algorithms.Add("SHA-1withDSA",              "sha1DSA");                        
            algorithms.Add("SHA-224withDSA",            "sha224DSA");                        
            algorithms.Add("SHA-256withDSA",            "sha256DSA");                        
            algorithms.Add("SHA-384withDSA",            "sha384DSA");                        
            algorithms.Add("SHA-512withDSA",            "sha512DSA");                        
            algorithms.Add("NONEwithECDSA",             "noneECDSA");            
            algorithms.Add("SHA-1withECDSA",            "sha1ECDSA");                        
            algorithms.Add("SHA-224withECDSA",          "sha224ECDSA");                        
            algorithms.Add("SHA-256withECDSA",          "sha256ECDSA");                        
            algorithms.Add("SHA-384withECDSA",          "sha384ECDSA");                        
            algorithms.Add("SHA-512withECDSA",          "sha512ECDSA");                        
            algorithms.Add("RIPEMD160withECDSA",        "ripemd160ECDSA");                        
            algorithms.Add("GOST3410",                  "gost3410");                        
            algorithms.Add("ECGOST3410",                "ecgost3410");

            try
            {
                return algorithms[key];
            }
            catch (KeyNotFoundException)
            {
                return key;
            }
            
        }

        #endregion

    }

    /// <summary>
    /// Lista de Certificados
    /// </summary>    
    public class CertificateList : List<Certificate>
    {
    }
}

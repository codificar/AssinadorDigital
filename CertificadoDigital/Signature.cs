using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Linq;
using System.IO;
using System.Reflection;

namespace CertificadoDigital
{

    /// <summary>
    /// Representa uma assinatura digital em um documento
    /// </summary>            
    public class Signature
    {

        #region [Constructor]

        /// <summary>
        /// Construtor vazio
        /// </summary>
        internal Signature()
        {
        }
        
        /// <summary>
        /// Construtor completo da classe
        /// </summary>
        /// <param name="name"></param>
        internal Signature
        (
            string file,
            FileFormat format,
            string objective,            
            Subject subject,
            DateTime dateTime,            
            bool valid,
            CertificateList certificates
        )
        {

            if (subject == null)
                throw new ArgumentNullException();

            if (file != null && file != string.Empty)                
                this._file = System.IO.Path.GetFileName(file);
             
            this._format = format;
            
            if (objective != null && objective != string.Empty) 
                this._objective = objective;            
            
            if (subject != null)
                this._subject = subject;

            if (dateTime != null)            
                this._dateTime = (dateTime.Kind == DateTimeKind.Local ? dateTime : dateTime.ToLocalTime());
            
            this._valid = valid;

            if (certificates != null)
                this._certificates = certificates;

            this._fileProperties = new Dictionary<FileProperties, string>();
            
        }

        #endregion

        #region [Variables]

        private string _file;
        private Dictionary<FileProperties, string> _fileProperties;
        private FileFormat _format;

        private string _objective;        
        private Subject _subject;
        private DateTime _dateTime;        
        private bool _valid;
        private CertificateList _certificates;
        private System.Security.Cryptography.X509Certificates.X509Certificate2 _x509certificate;
        private string _issuer;
        
        #endregion

        #region [Properties]

        /// <summary>
        /// Arquivo verificado
        /// </summary>
        public string File
        {
            get
            {
                return this._file;
            }
        }

        /// <summary>
        /// Propriedades do arquivo
        /// </summary>
        public Dictionary<FileProperties, string> FileProperties
        {
            get
            {
                return this._fileProperties;
            }
        }

        /// <summary>
        /// Formato do Arquivo
        /// </summary>
        public FileFormat FileFormat
        {
            get
            {
                return this._format;
            }
        }

        /// <summary>
        /// Objetivo/Razão da Assinatura
        /// </summary>
        public string Objective
        {
            get{ return _objective; }
        }

        /// <summary>
        /// Informações da assinatura
        /// </summary>
        internal Subject Subject
        {
            get
            {
                return this._subject;
            }
        }

        /// <summary>
        /// Data e Hora da Assinatura
        /// </summary>
        public DateTime DateTime
        {
            get
            {
                return this._dateTime;
            }
        }

        /// <summary>
        /// Determina se a assinatura é válida
        /// </summary>
        public bool Valid
        {
            get
            {
                return this._valid;
            }
        }

        /// <summary>
        /// País de origem da assinatura
        /// </summary>
        public string Country
        {
            get
            {
                return this.Subject.Country;
            }
        }

        /// <summary>
        /// Estado de Origem da Assinatura
        /// </summary>
        public string State
        {
            get
            {
                return this.Subject.State;
            }
        }

        /// <summary>
        /// Localidade de origem da assinatura
        /// </summary>
        public string Locality
        {
            get
            {
                return this.Subject.Locality;
            }
        }

        /// <summary>
        /// Organização responsável pela Assinatura
        /// </summary>
        public string Organization
        {
            get
            {
                return this.Subject.Organization;
            }
        }

        /// <summary>
        /// Relação das organizações que ratificam a assinatura
        /// </summary>
        public List<string> OrganizationUnit
        {
            get
            {
                return this.Subject.OrganizationUnit;
            }
        }

        /// <summary>
        /// Assinante
        /// </summary>
        public string Signer
        {
            get
            {
                return this.Subject.CommonName;
            }

        }

        /// <summary>
        /// Identificação do assinante
        /// </summary>
        public string SignerIdentification
        {
            get
            {
                if (this.Subject == null)
                    return null;
                else
                {
                    string detail = this.Signer;
                        
                    if (detail.IndexOf("ME:") > 0) // MICROEMPRESA
                        return detail.Substring(detail.IndexOf("ME:") + 3);                            
                    else if (detail.IndexOf("LTDA:") > 0) // LTDA
                        return detail.Substring(detail.IndexOf("LTDA:") + 5);                    
                    else // DEFAULT
                        return getIdentifier(detail);
                    
                }
            }
        }

        /// <summary>
        /// Certificados contidos na assinatura
        /// </summary>
        public CertificateList Certificates
        {
            get
            {
                return this._certificates;
            }
        }

        /// <summary>
        /// Representa o Certificado no formato X509Certificate
        /// </summary>
        public System.Security.Cryptography.X509Certificates.X509Certificate2 X509Certificate
        {
            get
            {
                return this._x509certificate;
            }
        }

        #endregion

        #region [Methods]
        
        /// <summary>
        /// Converte a assinatura em uma string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            // order the lists            
            if (this.OrganizationUnit != null) this.OrganizationUnit.Sort((x, y) => (string.Compare(x, y)));
            if (this.Certificates != null) this.Certificates.Sort((x, y) => (string.Compare(x.Name, y.Name)));

            string _s = string.Empty;
            
            _s += 
                "Signer=" + this.Signer + "\n" +
                "File=" + this.File + "\n" + 
                "FileFormat=" + this.FileFormat + "\n" +
                "Objective=" + this.Objective + "\n" +
                "DateTime=" + this.DateTime.ToString(Constants.DateTimeFormat) + "\n" +
                "Valid=" + this.Valid.ToString() + "\n" + 
                "Country="  + this.Country + "\n" + 
                "State=" + this.State + "\n" + 
                "Locality=" + this.Locality + "\n" +
                "Organization=" + this.Organization;

            for(int i = 0; i < this.OrganizationUnit.Count; i++) 
                _s += "OrganizationUnit" + (i+1) + "=" + this.OrganizationUnit[i] + "\n"; 

            _s += "SignerIdentification=" + this.SignerIdentification + "\n";
  
            for(int i = 0; i < this.Certificates.Count; i++)
                _s += "Certificate" + (i+1) + "=" + this.Certificates[i].ToString() + "\n";

            return _s;
        }

        /// <summary>
        /// Converte o objeto numa string de XML
        /// </summary>
        /// <returns></returns>
        public string toXml()
        {
            return this._toXml().ToString();
        }

        /// <summary>
        /// Converte o objeto em XML
        /// </summary>
        /// <returns></returns>
        public XElement _toXml()
        {
            XElement _xml = new XElement("Signature",
                new XAttribute("Value", this.Signer),
                new XElement("Signer", this.Signer),
                new XElement("File", this.File),
                new XElement("FileFormat", this.FileFormat),
                new XElement("Objective", this.Objective),
                new XElement("DateTime", this.DateTime.ToString(Constants.DateTimeFormat)),
                new XElement("Valid", this.Valid),
                new XElement("Country", this.Country),
                new XElement("State", this.State),
                new XElement("Locality", this.Locality),
                new XElement("Organization", this.Organization),
                new XElement("OrganizationUnits",
                    (
                        from ou in this.OrganizationUnit
                        orderby ou
                        select new XElement("OrganizationUnit", ou.ToString())
                    )
                ),
                new XElement("SignerIdentification", this.SignerIdentification),
                new XElement("Certificates",
                    (
                        from c in this.Certificates
                        orderby c.Name
                        select c._toXml()
                    )         
                )               
            );

            return _xml;
        }

        /// <summary>
        /// Cria um Signature a partir do XML
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Signature fromXml(XElement s)
        {
            return new Signature
            {
                SFile = s.Element("File").Value,
                SFileFormat = (FileFormat)Enum.Parse(typeof(FileFormat), s.Element("FileFormat").Value),
                SObjective = s.Element("Objective").Value,
                SDateTime = DateTime.ParseExact(s.Element("DateTime").Value, Constants.DateTimeFormat, null),
                SValid = Convert.ToBoolean(s.Element("Valid").Value),
                SSubject = (
                    new Subject
                    {
                        Country = s.Element("Country").Value,
                        State = s.Element("State").Value,
                        Locality = s.Element("Locality").Value,
                        Organization = s.Element("Organization").Value,
                        CommonName = s.Element("Signer").Value,
                        OrganizationUnit =
                        (
                            from ou in s.Element("OrganizationUnits").Elements("OrganizationUnit")
                            select ou.Value
                        ).ToList()
                    }
                ),
                SCertificates = 
                (
                    from c in s.Element("Certificates").Elements("Certificate")
                    select Certificate.fromXml(c)
                ).ToList()
            };
        }

        /// <summary>
        /// Tenta obter o CNPJ a partir da assinatura
        /// </summary>
        /// <param name="signer"></param>
        /// <returns></returns>
        private string getIdentifier(string signer)
        {
            int count = 0;
            for (int i = 0; i < signer.Length; i++)
            {
                if (signer[i] >= '0' && signer[i] <= '9')
                    count++;
                else
                    count = 0;
                if (count == 14)
                    return signer.Substring(i - 13, 14);
            }
            return null;
        }

        /// <summary>
        /// Retorna o Subject no padrão Office
        /// </summary>
        /// <returns></returns>
        public string OfficeTemplateSubject()
        {
            return this.Subject.OfficeFormat();
        }

        /// <summary>
        /// Retorna o Emissor no formato Office
        /// </summary>
        /// <returns></returns>
        public string OfficeTemplateIssuer()
        {
            // CN=
            // OU=
            // O=
            // C=

            string[] sx = { "CN=", "OU=", "O=", "C=" };
            int[,] ix = new int[4,2];

            for (int i = 0; i < sx.Length; i++)
            {
                ix[i,0] = this._issuer.IndexOf(sx[i]);
                if(ix[i,0] != -1)
                    ix[i,1] = this._issuer.IndexOf(",", ix[i,0]);
            }

            string ret = string.Empty;

            for (int i = 0; i < sx.Length; i++)
            {
                if (ix[i, 0] != -1)
                    ret += this._issuer.Substring(ix[i, 0], (ix[i, 1] == -1 ? 
                        this._issuer.Length : ix[i, 1]) - ix[i, 0]) + (i != 3 ? ", " : "");
            }

            if (ret[ret.Length - 2] == ',')
                ret = ret.Substring(0, ret.Length - 2);
            
            return ret;

        }

        #endregion

        #region [InternalProperties - Setters]

        internal string SFile
        {
            set
            {
                this._file = value;
            }
        }

        internal FileFormat SFileFormat
        {
            set
            {
                this._format = value;
            }
        }

        internal string SObjective
        {
            set 
            { 
                if (value != null && value != string.Empty)
                    this._objective = value; 
            }
        }

        internal Subject SSubject
        {
            set
            {
                this._subject = value;
            }
        }

        internal DateTime SDateTime
        {
            set
            {   
                this._dateTime = value;
            }
        }

        internal bool SValid
        {
            set
            {
                this._valid = value;
            }
        }

        internal List<Certificate> SCertificates
        {
            set
            {
                this._certificates = new CertificateList();
                foreach (Certificate item in value)
                {
                    this._certificates.Add(item);
                }
            }
        }

        internal System.Security.Cryptography.X509Certificates.X509Certificate2 SX509Certificate
        {
            set
            {
                this._x509certificate = value;
            }
        }

        internal string SIssuer
        {
            set
            {
                this._issuer = value;
            }
        }

        internal void addProperties(FileProperties prop, string value)
        {
            this._fileProperties.Add(prop, value);
        }

        internal Dictionary<FileProperties, string> SFileProperties
        {
            set
            {
                this._fileProperties = value;
            }
        }

        #endregion

    }

    /// <summary>
    /// Lista de Assinaturas
    /// </summary>    
    public class SignatureList : List<Signature>
    {

        /// <summary>
        /// Converte a lista de assinaturas em XML
        /// </summary>
        /// <returns></returns>
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string toXml()
        {
            return this._toXml().ToString();
        }

        /// <summary>
        /// Converte a lista de assinaturas em XML
        /// </summary>
        /// <returns></returns>
        public XElement _toXml()
        {
            XElement _list = new XElement("Signatures",
                from s in this
                orderby s.Signer
                select s._toXml());

            return _list;
        }

        /// <summary>
        /// Cria um objeto do tipo SignatureList a partir de um XML
        /// </summary>
        /// <param name="xml">String com o código XML</param>
        /// <returns>Lista de assinaturas</returns>
        public static SignatureList fromXml(string xml)
        {
            TextReader tr = new StringReader(xml);
            XDocument doc = XDocument.Load(tr);

            List<Signature> list =
                (
                    from s in doc.Element("Signatures").Elements("Signature")
                    select Signature.fromXml(s)                    
                ).ToList();

            return fromList(list);

        }

        /// <summary>
        /// Faz a conversão entre List<Signture> e SignatureList
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        private static SignatureList fromList(List<Signature> from)
        {
            SignatureList ret = new SignatureList();
            
            foreach(Signature item in from)
            {
                ret.Add(item);
            }

            return ret;
        }

        /// <summary>
        /// Verifica se duas listas de assinaturas são iguais
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        /// <returns></returns>
        public static bool ListsAreEqual(SignatureList list1, SignatureList list2)
        {
            return list1.ToString() == list2.ToString();
        }

        /// <summary>
        /// Verifica se duas assinturas são iguais
        /// </summary>
        /// <param name="sig1"></param>
        /// <param name="sig2"></param>
        /// <returns></returns>
        public static bool SignaturesAreEqual(Signature sig1, Signature sig2)
        {
            return sig1.ToString() == sig2.ToString();
        }

        /// <summary>
        /// Converte a lista em uma String
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {

            // order the list                        
            this.Sort((x, y) => (string.Compare(x.Signer, y.Signer)));

            string _s = string.Empty;
            
            for(int i = 0; i < this.Count; i++)
                _s += "Signature" + (i + 1) + "=" + this[i].ToString();

            return _s;
        }

    }

}

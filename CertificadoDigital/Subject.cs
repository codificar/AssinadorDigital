using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CertificadoDigital
{
    
    /// <summary>
    /// Representa o detalhe de um assunto
    /// Com tipo e valor
    /// </summary>
    internal class SubjectDetail
    {
        private SubjectType _type;
        private string _value;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        public SubjectDetail(SubjectType type, string value)
        {
            this._type = type;
            this._value = value;
        }

        /// <summary>
        /// Tipo
        /// </summary>
        public SubjectType Type
        {
            get
            {
                return _type;
            }
        }

        /// <summary>
        /// Valor
        /// </summary>
        public string Value
        {
            get
            {
                return _value;
            }
        }

    }

    /// <summary>
    /// Lista de assuntos em uma assinatura ou certificado
    /// </summary>
    internal class Subject : List<SubjectDetail>
    {

        #region [Constructor]

        /// <summary>
        /// Construtor vazio
        /// </summary>
        internal Subject()
        {
        }

        #endregion

        #region [Properties]

        /// <summary>
        /// País de origem
        /// </summary>
        internal string Country{
            get
            {
                return this.get(SubjectType.Country);
            }
            set
            {
                this.set(SubjectType.Country, value);
            }
        }

        /// <summary>
        /// Estado de Origem
        /// </summary>
        internal string State
        {
            get
            {
                return this.get(SubjectType.State);                
            }
            set
            {
                this.set(SubjectType.State, value);
            }
        }

        /// <summary>
        /// Localidade de origem
        /// </summary>
        internal string Locality
        {
            get
            {
                return this.get(SubjectType.Locality);
            }
            set
            {
                this.set(SubjectType.Locality, value);
            }
        }

        /// <summary>
        /// Organização responsável pela assinatura/certificado
        /// </summary>
        internal string Organization
        {   
            get
            {
                return this.get(SubjectType.Organization);
            }
            set
            {
                this.set(SubjectType.Organization, value);
            }
        }

        /// <summary>
        /// Relação das organizações que ratificam a assinatura - Getter the List
        /// </summary>
        internal List<string> OrganizationUnit
        {
            get
            {   
                IEnumerable<string> list = (from subject in this
                                            where subject.Type == SubjectType.OrganizationalUnit
                                            select subject.Value);

                List<string> ret = new List<string>();
                
                foreach (string item in list)
                    ret.Add(item);

                return ret;
                
            }
            set
            {
                foreach(string item in value)
                {
                    this.set(SubjectType.OrganizationalUnit, item);
                }
            }
        }

        /// <summary>
        /// Valor CN no Subject
        /// </summary>
        internal string CommonName
        {
            get
            {
                return this.get(SubjectType.CommonName);
            }
            set
            {
                this.set(SubjectType.CommonName, value);
            }
        }
    
        #endregion

        #region [Methods]

        /// <summary>
        /// obtém subject pelo tipo
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string get(SubjectType type){
            try
            {
                return
                    (
                        from subject in this
                        where subject.Type == type
                        select subject.Value
                    ).First();
            }
            catch (InvalidOperationException ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Acrescenta um novo elemento
        /// </summary>
        /// <param name="type">Tipo</param>
        /// <param name="value">Valor</param>
        private void set(SubjectType type, string value)
        {
            if (value != null && value != string.Empty)
                this.Add(new SubjectDetail(type, value));
        }

        /// <summary>
        /// Converte tipo e valor em string todos os valores
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string strRet = string.Empty;

            foreach (SubjectDetail detail in this)
                strRet += detail.Type + "=" + detail.Value + "; ";

            return strRet;
        }

        /// <summary>
        /// Retorna o Subject com o padrão Office
        /// </summary>
        /// <returns></returns>
        internal string OfficeFormat()
        {
            string ret = string.Empty;

            ret += (this.CommonName != null && this.CommonName != string.Empty) ? "CN=" + this.CommonName : "";

            for (int i = this.OrganizationUnit.Count - 1; i > -1; i--)
                ret += (OrganizationUnit[i] != null && OrganizationUnit[i] != string.Empty) ? 
                    " ,OU=" + OrganizationUnit[i] : "";

            ret += this.Locality != null && this.Locality != string.Empty ? 
                " ,L=" + this.Locality : "";
            ret += this.State != null && this.State != string.Empty ? 
                " ,S=" + this.State : "";
            ret += this.Organization != null && this.Organization != string.Empty ? 
                " ,O=" + this.Organization : "";
            ret += this.Country != null && this.Country != string.Empty ? 
                " ,C=" + this.Country : "";
            
            return ret;

        }

        #endregion

    }
        
}

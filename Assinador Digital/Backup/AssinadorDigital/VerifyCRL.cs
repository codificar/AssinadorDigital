using System;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using Microsoft.Win32;

namespace AssinadorDigital
{
    public class ChainDocumentStatus
    {
        #region Private Fields Constants Variables

        private X509ChainStatus _X509ChainStatus = new X509ChainStatus();

        #endregion
        #region Public Fields Constants Variables

        public enum ChainDocumentStatusFlags
        {
            CtlNotSignatureValid,
            CtlNotTimeValid,
            CtlNotValidForUsage,
            Cyclic,
            HasExcludedNameConstraint,
            HasNotDefinedNameConstraint,
            HasNotPermittedNameConstraint,
            HasNotSupportedNameConstraint,
            InvalidBasicConstraints,
            InvalidExtension,
            InvalidNameConstraints,
            InvalidPolicyConstraints,
            NoError,
            NoIssuanceChainPolicy,
            NotSignatureValid,
            NotTimeNested,
            NotTimeValid,
            NotValidForUsage,
            OfflineRevocation,
            PartialChain,
            RevocationStatusUnknown,
            Revoked,
            UntrustedRoot,
            CorruptedDocument
        }

        public ChainDocumentStatusFlags Status = new ChainDocumentStatusFlags();

        public string StatusInformation
        {
            get
            {
                switch (Status)
                {
                    case ChainDocumentStatusFlags.CtlNotSignatureValid:
                    case ChainDocumentStatusFlags.CtlNotTimeValid:
                    case ChainDocumentStatusFlags.CtlNotValidForUsage:
                    case ChainDocumentStatusFlags.Cyclic:
                    case ChainDocumentStatusFlags.HasExcludedNameConstraint:
                    case ChainDocumentStatusFlags.HasNotDefinedNameConstraint:
                    case ChainDocumentStatusFlags.HasNotPermittedNameConstraint:
                    case ChainDocumentStatusFlags.HasNotSupportedNameConstraint:
                    case ChainDocumentStatusFlags.InvalidBasicConstraints:
                    case ChainDocumentStatusFlags.InvalidExtension:
                    case ChainDocumentStatusFlags.InvalidPolicyConstraints:
                    case ChainDocumentStatusFlags.NoIssuanceChainPolicy:
                    case ChainDocumentStatusFlags.NotSignatureValid:
                    case ChainDocumentStatusFlags.NotTimeNested:
                    case ChainDocumentStatusFlags.NotTimeValid:
                    case ChainDocumentStatusFlags.NotValidForUsage:
                    case ChainDocumentStatusFlags.OfflineRevocation:
                    case ChainDocumentStatusFlags.PartialChain:
                    case ChainDocumentStatusFlags.RevocationStatusUnknown:
                    case ChainDocumentStatusFlags.UntrustedRoot:
                        return _X509ChainStatus.StatusInformation;
                    case ChainDocumentStatusFlags.Revoked:
                        return "O Certificado Digital foi revogado pela Agência Certificadora.";
                    case ChainDocumentStatusFlags.NoError:
                        return "Esta assinatura e o conteúdo assinado não foram alterados desde a aplicação da assinatura.";
                    case ChainDocumentStatusFlags.CorruptedDocument:
                        return "O conteúdo assinado foi alterado após a aplicação da assinatura.";
                    default:
                        return "";
                }
            }
        }

        public string StatusText
        {
            get
            {
                switch (Status)
                {
                    case ChainDocumentStatusFlags.CtlNotSignatureValid:
                        return "Lista de Certificado de Confiança com assinatura inválida";
                    case ChainDocumentStatusFlags.CtlNotTimeValid:
                        return "Lista de Certificado de Confiança expirou";
                    case ChainDocumentStatusFlags.CtlNotValidForUsage:
                        return "Lista de Certificado de Confiança inválida para o uso";
                    case ChainDocumentStatusFlags.Cyclic:
                    case ChainDocumentStatusFlags.HasExcludedNameConstraint:
                        return "Cadeia do certificado apresentou problemas";
                    case ChainDocumentStatusFlags.HasNotDefinedNameConstraint:
                        return "";
                    case ChainDocumentStatusFlags.HasNotPermittedNameConstraint:
                        return "";
                    case ChainDocumentStatusFlags.HasNotSupportedNameConstraint:
                        return "";
                    case ChainDocumentStatusFlags.InvalidBasicConstraints:
                        return "";
                    case ChainDocumentStatusFlags.InvalidExtension:
                        return "Extensão Inválida";
                    case ChainDocumentStatusFlags.InvalidNameConstraints:
                        return "";
                    case ChainDocumentStatusFlags.InvalidPolicyConstraints:
                        return "";
                    case ChainDocumentStatusFlags.NoError:
                        return "Sem erros de não conformidade";
                    case ChainDocumentStatusFlags.NoIssuanceChainPolicy:
                        return "";
                    case ChainDocumentStatusFlags.NotSignatureValid:
                        return "Assinatura Inválida";
                    case ChainDocumentStatusFlags.NotTimeNested:
                        return "";
                    case ChainDocumentStatusFlags.NotTimeValid:
                        return "Data inválida";
                    case ChainDocumentStatusFlags.NotValidForUsage:
                        return "Inválido para o uso";
                    case ChainDocumentStatusFlags.OfflineRevocation:
                        return "";
                    case ChainDocumentStatusFlags.PartialChain:
                        return "Cadeia de certificado parcial";
                    case ChainDocumentStatusFlags.RevocationStatusUnknown:
                        return "Estado de revogação desconhecido";
                    case ChainDocumentStatusFlags.Revoked:
                        return "Certificado revogado";
                    case ChainDocumentStatusFlags.UntrustedRoot:
                        return "Certificado raiz não confiável";
                    case ChainDocumentStatusFlags.CorruptedDocument:
                        return "Documento corrompido";
                    default:
                        return "";
                }
            }
        }

        #endregion
        #region Constructor

        public ChainDocumentStatus()
        { }

        public ChainDocumentStatus(X509ChainStatus ChainStatus, ChainDocumentStatusFlags? CustomChainStatusFlags)
        {
            _X509ChainStatus = ChainStatus;
            SetStatusFlag(CustomChainStatusFlags);
        }

        #endregion
        #region Private Methods

        private void SetStatusFlag(ChainDocumentStatusFlags? CustomChainStatusFlags)
        {
            if (CustomChainStatusFlags.HasValue)
            {
                Status = CustomChainStatusFlags ?? ChainDocumentStatusFlags.NoError;
            }
            else
            {
                switch (_X509ChainStatus.Status)
                {
                    case X509ChainStatusFlags.CtlNotSignatureValid:
                        Status = ChainDocumentStatusFlags.CtlNotSignatureValid;
                        break;
                    case X509ChainStatusFlags.CtlNotTimeValid:
                        Status = ChainDocumentStatusFlags.CtlNotTimeValid;
                        break;
                    case X509ChainStatusFlags.CtlNotValidForUsage:
                        Status = ChainDocumentStatusFlags.CtlNotValidForUsage;
                        break;
                    case X509ChainStatusFlags.Cyclic:
                        Status = ChainDocumentStatusFlags.Cyclic;
                        break;
                    case X509ChainStatusFlags.HasExcludedNameConstraint:
                        Status = ChainDocumentStatusFlags.HasExcludedNameConstraint;
                        break;
                    case X509ChainStatusFlags.HasNotDefinedNameConstraint:
                        Status = ChainDocumentStatusFlags.HasNotDefinedNameConstraint;
                        break;
                    case X509ChainStatusFlags.HasNotPermittedNameConstraint:
                        Status = ChainDocumentStatusFlags.HasNotPermittedNameConstraint;
                        break;
                    case X509ChainStatusFlags.HasNotSupportedNameConstraint:
                        Status = ChainDocumentStatusFlags.HasNotSupportedNameConstraint;
                        break;
                    case X509ChainStatusFlags.InvalidBasicConstraints:
                        Status = ChainDocumentStatusFlags.InvalidBasicConstraints;
                        break;
                    case X509ChainStatusFlags.InvalidExtension:
                        Status = ChainDocumentStatusFlags.InvalidExtension;
                        break;
                    case X509ChainStatusFlags.InvalidNameConstraints:
                        Status = ChainDocumentStatusFlags.InvalidNameConstraints;
                        break;
                    case X509ChainStatusFlags.InvalidPolicyConstraints:
                        Status = ChainDocumentStatusFlags.InvalidPolicyConstraints;
                        break;
                    case X509ChainStatusFlags.NoError:
                        Status = ChainDocumentStatusFlags.NoError;
                        break;
                    case X509ChainStatusFlags.NoIssuanceChainPolicy:
                        Status = ChainDocumentStatusFlags.NoIssuanceChainPolicy;
                        break;
                    case X509ChainStatusFlags.NotSignatureValid:
                        Status = ChainDocumentStatusFlags.NotSignatureValid;
                        break;
                    case X509ChainStatusFlags.NotTimeNested:
                        Status = ChainDocumentStatusFlags.NotTimeNested;
                        break;
                    case X509ChainStatusFlags.NotTimeValid:
                        Status = ChainDocumentStatusFlags.NotTimeValid;
                        break;
                    case X509ChainStatusFlags.NotValidForUsage:
                        Status = ChainDocumentStatusFlags.NotValidForUsage;
                        break;
                    case X509ChainStatusFlags.OfflineRevocation:
                        Status = ChainDocumentStatusFlags.OfflineRevocation;
                        break;
                    case X509ChainStatusFlags.PartialChain:
                        Status = ChainDocumentStatusFlags.PartialChain;
                        break;
                    case X509ChainStatusFlags.RevocationStatusUnknown:
                        Status = ChainDocumentStatusFlags.RevocationStatusUnknown;
                        break;
                    case X509ChainStatusFlags.Revoked:
                        Status = ChainDocumentStatusFlags.Revoked;
                        break;
                    case X509ChainStatusFlags.UntrustedRoot:
                        Status = ChainDocumentStatusFlags.UntrustedRoot;
                        break;
                    default:
                        Status = ChainDocumentStatusFlags.NoError;
                        break;
                }
            }
        }

        #endregion
        #region Public Methods

        #endregion
    }

    public static class CertificateUtils
    {
        public static X509ChainStatus buildStatus;

        public static void VerifyConsultCRL()
        { 
            RegistryKey ConsultCRL = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\LTIA\Assinador Digital", true);
            if (!(Convert.ToBoolean(ConsultCRL.GetValue("ConsultCRL"))))
                if (MessageBox.Show("A Verificação de Certificados Revogados está desabilitada!\n" + 
                    "Deseja ativar agora?", "Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    ConsultCRL.SetValue("ConsultCRL", true);
                }
        }

        public static X509Certificate2 GetCertificate()
        {
            X509Store certStore = new X509Store(StoreLocation.CurrentUser);
            certStore.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection certs =
                X509Certificate2UI.SelectFromCollection(
                    certStore.Certificates,
                    "Selecionar um Certificado Digital.",
                    "Por favor selecione um certificado para assinatura.",
                    X509SelectionFlag.SingleSelection);

            if (certs.Count > 0)
            {
                RegistryKey ConsultCRL = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\LTIA\Assinador Digital", true);
                bool? valid = ValidateCertificate(certs[0], Convert.ToBoolean(ConsultCRL.GetValue("ConsultCRL")), true);
                ConsultCRL.Close();
                if (valid == null)
                {
                    return null;
                }
            }
            return certs.Count > 0 ? certs[0] : null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="certificate">X509Certificate2 to validate</param>
        /// <param name="checkCRL"></param>
        /// <param name="showAlert">Show a message box case the Certificate is not valid</param>
        /// <returns></returns>
        public static bool? ValidateCertificate(X509Certificate2 certificate, bool checkCRL, bool showAlert)
        {
            buildStatus = new X509ChainStatus();

            var chain = new X509Chain();
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

            bool valid = chain.Build(certificate);

            if (!valid)
            {
                buildStatus = chain.ChainStatus[0];
                if (showAlert)
                {
                    if (MessageBox.Show("O Certificado Digital selecionado apresentou problemas de não conformidade.\n" +
                        "O seguinte erro foi apresentado:\n\n" +
                        buildStatus.StatusInformation + "\n" +
                        "Deseja utilizá-lo mesmo assim?", "Problema de não conformidade - " + buildStatus.Status.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1) == DialogResult.No)
                    {
                        return null;
                    }
                }
            }
            return valid;
        }
    }
}

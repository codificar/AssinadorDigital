@echo off

REM Configura os OIDs em uso na ICP Brasil
REM Com base no Esquema de OID na ICPBrasil - ADE ICP04.01.A Versão 2.0

REM Atualizado em 27/01/2009
REM (c) Microsoft, 2009

REM Subject Alternative Name

oidinfo -r 2.16.76.1.3.1 "ICP-Brasil Pessoa Fisica"
oidinfo -r 2.16.76.1.3.2 "ICP-Brasil Nome"
oidinfo -r 2.16.76.1.3.3 "CNPJ"
oidinfo -r 2.16.76.1.3.4 "Dados"
oidinfo -r 2.16.76.1.3.5 "Titulo Eleitor"
oidinfo -r 2.16.76.1.3.6 "PF INSS"
oidinfo -r 2.16.76.1.3.7 "PJ INSS"
oidinfo -r 2.16.76.1.4.1.1 "SINCOR"
oidinfo -r 2.16.76.1.4.1.1 "Registro Corretor"

REM DPC

oidinfo -r 2.16.76.1.1.0 "ICP-Brasil AC Raiz"
oidinfo -r 2.16.76.1.1.1 "ICP-Brasil AC Presidencia"
oidinfo -r 2.16.76.1.1.2 "ICP-Brasil AC Serpro"
oidinfo -r 2.16.76.1.1.3 "ICP-Brasil AC SERASA ACP"
oidinfo -r 2.16.76.1.1.4 "ICP-Brasil AC SERASA AC"
oidinfo -r 2.16.76.1.1.5 "ICP-Brasil AC Certisign"
oidinfo -r 2.16.76.1.1.6 "ICP-Brasil AC Certisign SPB"
oidinfo -r 2.16.76.1.1.7 "ICP-Brasil AC SERASA"
oidinfo -r 2.16.76.1.1.8 "ICP-Brasil AC SRF"
oidinfo -r 2.16.76.1.1.9 "ICP-Brasil AC CAIXA"
oidinfo -r 2.16.76.1.1.10 "ICP-Brasil AC CAIXA IN"
oidinfo -r 2.16.76.1.1.11 "ICP-Brasil AC CAIXA PJ"
oidinfo -r 2.16.76.1.1.12 "ICP-Brasil AC CAIXA PF"
oidinfo -r 2.16.76.1.1.13 "ICP-Brasil AC SERPRO SRF"
oidinfo -r 2.16.76.1.1.14 "ICP-Brasil AC Certisign Multipla"
oidinfo -r 2.16.76.1.1.15 "ICP-Brasil AC Certisign SRF"
oidinfo -r 2.16.76.1.1.16 "ICP-Brasil AC SERASA SRF"
oidinfo -r 2.16.76.1.1.17 "ICP-Brasil AC Imprensa Oficial SP"
oidinfo -r 2.16.76.1.1.18 "ICP-Brasil AC PRODEMGE"
oidinfo -r 2.16.76.1.1.19 "ICP-Brasil AC JUS"
oidinfo -r 2.16.76.1.1.20 "ICP-Brasil AC SERPRO ACF"
oidinfo -r 2.16.76.1.1.21 "ICP-Brasil AC SINCOR"
oidinfo -r 2.16.76.1.1.22 "ICP-Brasil AC Imprensa Oficial SP SRF"
oidinfo -r 2.16.76.1.1.23 "ICP-Brasil AC FENACOR"
oidinfo -r 2.16.76.1.1.24 "ICP-Brasil AC SERPRO JUS"
oidinfo -r 2.16.76.1.1.25 "ICP-Brasil AC Caixa Justica"
oidinfo -r 2.16.76.1.1.26 "ICP-Brasil AC IMESP"
oidinfo -r 2.16.76.1.1.27 "ICP-Brasil AC PRODEMGE SRF"
oidinfo -r 2.16.76.1.1.28 "ICP-Brasil AC CertSign Justica"
oidinfo -r 2.16.76.1.1.29 "ICP-Brasil AC SERASA JUS"
oidinfo -r 2.16.76.1.1.30 "ICP-Brasil AC PETROBRAS"
oidinfo -r 2.16.76.1.1.31 "ICP-Brasil AC"
oidinfo -r 2.16.76.1.1.32 "ICP-Brasil AC SINCOR SRF"
oidinfo -r 2.16.76.1.1.33 "ICP-Brasil AC CeritSign FENACON SRF"
oidinfo -r 2.16.76.1.1.34 "ICP-Brasil AC Notorial SRF"
oidinfo -r 2.16.76.1.1.35 "ICP-Brasil AC SRF"

REM Certificados A1

oidinfo -r 2.16.76.1.2.1.1  "ICP-Brasil de Assinatura Digital A1 SERPRO SPB"
oidinfo -r 2.16.76.1.2.1.2  "ICP-Brasil de Assinatura Digital A1 SERASA" 
oidinfo -r 2.16.76.1.2.1.3  "ICP-Brasil de Assinatura Digital A1 Presidencia Republica PCA1"
oidinfo -r 2.16.76.1.2.1.4  "ICP-Brasil de Assinatura Digital A1 CertiSign Sistema de Pagamentos SPB"
oidinfo -r 2.16.76.1.2.1.5  "ICP-Brasil de Assinatura Digital A1 SERPRO" 
oidinfo -r 2.16.76.1.2.1.6  "ICP-Brasil de Assinatura Digital A1 SERASA CD"
oidinfo -r 2.16.76.1.2.1.7  "ICP-Brasil de Assinatura Digital A1 Caixa IN"
oidinfo -r 2.16.76.1.2.1.8  "ICP-Brasil de Assinatura Digital A1 Caixa PF"
oidinfo -r 2.16.76.1.2.1.9  "ICP-Brasil de Assinatura Digital A1 Caixa PJ"
oidinfo -r 2.16.76.1.2.1.10 "ICP-Brasil de Assinatura Digital A1 Serpro SRF"
oidinfo -r 2.16.76.1.2.1.11 "ICP-Brasil de Assinatura Digital A1 CertiSign"
oidinfo -r 2.16.76.1.2.1.12 "ICP-Brasil de Assinatura Digital A1 CertiSign SRF"
oidinfo -r 2.16.76.1.2.1.13 "ICP-Brasil de Assinatura Digital A1 SERASA SRF"
oidinfo -r 2.16.76.1.2.1.14 "ICP-Brasil de Assinatura Digital A1 Imprensa Oficial SP"
oidinfo -r 2.16.76.1.2.1.15 "ICP-Brasil de Assinatura Digital A1 PRODEMGE"
oidinfo -r 2.16.76.1.2.1.16 "ICP-Brasil de Assinatura Digital A1 SERPRO ACF"
oidinfo -r 2.16.76.1.2.1.17 "ICP-Brasil de Assinatura Digital A1 SERPRO ACF SPB"
oidinfo -r 2.16.76.1.2.1.18 "ICP-Brasil de Assinatura Digital A1 SINCOR"
oidinfo -r 2.16.76.1.2.1.19 "ICP-Brasil de Assinatura Digital A1 SINCOR Corretores"
oidinfo -r 2.16.76.1.2.1.20 "ICP-Brasil de Assinatura Digital A1 Imprensa Oficial SP SRF"
oidinfo -r 2.16.76.1.2.1.21 "ICP-Brasil de Assinatura Digital A1 SERPRO JUS" 
oidinfo -r 2.16.76.1.2.1.22 "ICP-Brasil de Assinatura Digital A1 Caixa Justica"
oidinfo -r 2.16.76.1.2.1.23 "ICP-Brasil de Assinatura Digital A1 PRODEMGE SRF"
oidinfo -r 2.16.76.1.2.1.24 "ICP-Brasil de Assinatura Digital A1 CertiSign Justica"
oidinfo -r 2.16.76.1.2.1.25 "ICP-Brasil de Assinatura Digital A1 SERASA JUS"
oidinfo -r 2.16.76.1.2.1.26 "ICP-Brasil de Assinatura Digital A1 PETROBRAS"
oidinfo -r 2.16.76.1.2.1.27 "ICP-Brasil de Assinatura Digital A1 SRF"
oidinfo -r 2.16.76.1.2.1.28 "ICP-Brasil de Assinatura Digital A1 SINCOR"
oidinfo -r 2.16.76.1.2.1.29 "ICP-Brasil de Assinatura Digital A1 Certisign FENACON SRF"
oidinfo -r 2.16.76.1.2.1.30 "ICP-Brasil de Assinatura Digital A1 Notarial SRF"

REM Certificados A2

oidinfo -r 2.16.76.1.2.2.1 "ICP-Brasil Assinatura Digital A2 SERASA CD"
oidinfo -r 2.16.76.1.2.2.2 "ICP-Brasil Assinatura Digital A2 SERASA SRF"
oidinfo -r 2.16.76.1.2.2.3 "ICP-Brasil de Assinatura Digital A2 Certisign"
oidinfo -r 2.16.76.1.2.2.4 "ICP-Brasil de Assinatura Digital A2 Imprensa Oficial SP"
oidinfo -r 2.16.76.1.2.2.5 "ICP-Brasil de Assinatura Digital A2 Caixa Justica"
oidinfo -r 2.16.76.1.2.2.6 "ICP-Brasil de Assinatura Digital A2 Certisign Justica"
oidinfo -r 2.16.76.1.2.2.7 "ICP-Brasil de Assinatura Digital A2 SERASA JUS"

REM Certificados A3

oidinfo -r 2.16.76.1.2.3.1 "ICP-Brasil Assinatura Digital A3 ACPR"
oidinfo -r 2.16.76.1.2.3.2 "ICP-Brasil Assinatura Digital A3 SERPRO"
oidinfo -r 2.16.76.1.2.3.3 "ICP-Brasil Assinatura Digital A3 SERASA CD"
oidinfo -r 2.16.76.1.2.3.4 "ICP-Brasil Assinatura Digital A3 SERPRO SRF"
oidinfo -r 2.16.76.1.2.3.5 "ICP-Brasil Assinatura Digital A3 Certisign"
oidinfo -r 2.16.76.1.2.3.6 "ICP-Brasil Assinatura Digital A3 Certisign SRF"
oidinfo -r 2.16.76.1.2.3.7 "ICP-Brasil Assinatura Digital A3 Caixa IN"
oidinfo -r 2.16.76.1.2.3.8 "ICP-Brasil Assinatura Digital A3 Caixa PF"
oidinfo -r 2.16.76.1.2.3.9 "ICP-Brasil Assinatura Digital A3 Caixa PJ"
oidinfo -r 2.16.76.1.2.3.10 "ICP-Brasil Assinatura Digital A3 SERASA SRF"
oidinfo -r 2.16.76.1.2.3.11 "ICP-Brasil Assinatura Digital A3 Imprensa Oficial SP"
oidinfo -r 2.16.76.1.2.3.12 "ICP-Brasil Assinatura Digital A3 PRODEMGE"
oidinfo -r 2.16.76.1.2.3.13 "ICP-Brasil Assinatura Digital A3 PC SERPRO"
oidinfo -r 2.16.76.1.2.3.14 "ICP-Brasil Assinatura Digital A3 SINCOR"
oidinfo -r 2.16.76.1.2.3.15 "ICP-Brasil Assinatura Digital A3 SINCOR Corretores"
oidinfo -r 2.16.76.1.2.3.16 "ICP-Brasil Assinatura Digital A3 Imprensa Oficial SP SRF"
oidinfo -r 2.16.76.1.2.3.17 "ICP-Brasil Assinatura Digital A3 FENACOR"
oidinfo -r 2.16.76.1.2.3.18 "ICP-Brasil Assinatura Digital A3 SERPRO JUS"
oidinfo -r 2.16.76.1.2.3.19 "ICP-Brasil Assinatura Digital A3 Caixa Justica"
oidinfo -r 2.16.76.1.2.3.20 "ICP-Brasil Assinatura Digital A3 PRODEMGE SRF"
oidinfo -r 2.16.76.1.2.3.21 "ICP-Brasil Assinatura Digital A3 Certisign Justica"
oidinfo -r 2.16.76.1.2.3.22 "ICP-Brasil Assinatura Digital A3 SERASA JUS"
oidinfo -r 2.16.76.1.2.3.23 "ICP-Brasil Assinatura Digital A3 Petrobras"
oidinfo -r 2.16.76.1.2.3.24 "ICP-Brasil Assinatura Digital A3"
oidinfo -r 2.16.76.1.2.3.25 "ICP-Brasil Assinatura Digital A3 SINCOR SRF"
oidinfo -r 2.16.76.1.2.3.26 "ICP-Brasil Assinatura Digital A3 FENACON SRF"
oidinfo -r 2.16.76.1.2.3.27 "ICP-Brasil Assinatura Digital A3 Notarial SRF"

REM Certificados A4

oidinfo -r 2.16.76.1.2.4.1 "ICP-Brasil Assinatura Digital A4 SERASA CD"
oidinfo -r 2.16.76.1.2.4.2 "ICP-Brasil Assinatura Digital A4 SERASA SRF"
oidinfo -r 2.16.76.1.2.4.3 "ICP-Brasil Assinatura Digital A4 Certisign"
oidinfo -r 2.16.76.1.2.4.4 "ICP-Brasil Assinatura Digital A4 Certisign SRF"
oidinfo -r 2.16.76.1.2.4.5 "ICP-Brasil Assinatura Digital A4 Imprensa Oficial SP"
oidinfo -r 2.16.76.1.2.4.6 "ICP-Brasil Assinatura Digital A4 Imprensa Oficial SP SRF"
oidinfo -r 2.16.76.1.2.4.7 "ICP-Brasil Assinatura Digital A4 PRODEMGE SRF"
oidinfo -r 2.16.76.1.2.4.8 "ICP-Brasil Assinatura Digital A4 Certisign Justica"
oidinfo -r 2.16.76.1.2.4.9 "ICP-Brasil Assinatura Digital A4"
oidinfo -r 2.16.76.1.2.4.10 "ICP-Brasil Assinatura Digital A4 SERASA JUS"
oidinfo -r 2.16.76.1.2.4.11 "ICP-Brasil Assinatura Digital A4"
oidinfo -r 2.16.76.1.2.4.12 "ICP-Brasil Assinatura Digital A4 SINCOR SRF"

REM Certificados S1

oidinfo -r 2.16.76.1.2.101.1 "ICP-Brasil Sigilo S1 SERASA CD"
oidinfo -r 2.16.76.1.2.101.2 "ICP-Brasil Sigilo S1 Imprensa Oficial SP"
oidinfo -r 2.16.76.1.2.101.3 "ICP-Brasil Sigilo S1 Certisign"
oidinfo -r 2.16.76.1.2.101.4 "ICP-Brasil Sigilo S1 PRODEMGE"
oidinfo -r 2.16.76.1.2.101.5 "ICP-Brasil Sigilo S1 Caixa Justica"
oidinfo -r 2.16.76.1.2.101.6 "ICP-Brasil Sigilo S1 Certisign Justica"
oidinfo -r 2.16.76.1.2.101.7 "ICP-Brasil Sigilo S1 SERASA JUS"
oidinfo -r 2.16.76.1.2.101.8 "ICP-Brasil Sigilo S1 Petrobras"
oidinfo -r 2.16.76.1.2.101.9 "ICP-Brasil Sigilo S1 SINCOR"

REM Certificados S2

oidinfo -r 2.16.76.1.2.102.1 "ICP-Brasil Sigilo S2 SERASA CD"
oidinfo -r 2.16.76.1.2.102.2 "ICP-Brasil Sigilo S2 Imprensa Oficial SP"
oidinfo -r 2.16.76.1.2.102.3 "ICP-Brasil Sigilo S2 Certisign"
oidinfo -r 2.16.76.1.2.102.4 "ICP-Brasil Sigilo S2 Caixa Justica"
oidinfo -r 2.16.76.1.2.102.5 "ICP-Brasil Sigilo S2 Certisign Justica"
oidinfo -r 2.16.76.1.2.102.6 "ICP-Brasil Sigilo S2 SERASA JUS"

REM Certificados S3

oidinfo -r 2.16.76.1.2.103.1 "ICP-Brasil Sigilo S3 SERASA CD"
oidinfo -r 2.16.76.1.2.103.2 "ICP-Brasil Sigilo S3"
oidinfo -r 2.16.76.1.2.103.3 "ICP-Brasil Sigilo S3 Certisign"
oidinfo -r 2.16.76.1.2.103.4 "ICP-Brasil Sigilo S3 Imprensa Oficial SP"
oidinfo -r 2.16.76.1.2.103.5 "ICP-Brasil Sigilo S3 PRODEMGE"
oidinfo -r 2.16.76.1.2.103.6 "ICP-Brasil Sigilo S3 Caixa Justica"
oidinfo -r 2.16.76.1.2.103.7 "ICP-Brasil Sigilo S3 Certisign Justica"
oidinfo -r 2.16.76.1.2.103.8 "ICP-Brasil Sigilo S3 SERASA JUS"

REM Certificados S4

oidinfo -r 2.16.76.1.2.104.1 "ICP-Brasil Sigilo S4 SERASA CD"
oidinfo -r 2.16.76.1.2.104.2 "ICP-Brasil Sigilo S4"
oidinfo -r 2.16.76.1.2.104.3 "ICP-Brasil Sigilo S4 Certisign"
oidinfo -r 2.16.76.1.2.104.4 "ICP-Brasil Sigilo S4 Imprensa Oficial SP"
oidinfo -r 2.16.76.1.2.104.5 "ICP-Brasil Sigilo S4 Certisign Justica"
oidinfo -r 2.16.76.1.2.104.6 "ICP-Brasil Sigilo S4 SERASA JUS"

REM AC 

oidinfo -r 2.16.76.1.2.201.1 "ICP-Brasil Sigilo AC SERASA"
oidinfo -r 2.16.76.1.2.201.2 "ICP-Brasil Sigilo AC CertiSign"
oidinfo -r 2.16.76.1.2.201.3 "ICP-Brasil Sigilo AC SRF"
oidinfo -r 2.16.76.1.2.201.4 "ICP-Brasil Sigilo AC Caixa"
oidinfo -r 2.16.76.1.2.201.5 "ICP-Brasil Sigilo AC JUS"
oidinfo -r 2.16.76.1.2.201.6 "ICP-Brasil Sigilo AC SERPRO"
oidinfo -r 2.16.76.1.2.201.7 "ICP-Brasil Sigilo AC Imprensa Oficial"
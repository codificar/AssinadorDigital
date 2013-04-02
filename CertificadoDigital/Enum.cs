namespace CertificadoDigital
{
    
    /// <summary>
    /// Define o formato do arquivo.
    /// </summary>
    public enum FileFormat
    {   
        PDFDocument,
        WordProcessingML,
        SpreadSheetML,
        PresentationML,
        XpsDocument,
    }

    /// <summary>
    /// Define o tipo do assunto
    /// </summary>
    public enum SubjectType
    { 
        Country,
        Organization,
        State,
        Locality,
        OrganizationalUnit,
        CommonName
    }

    /// <summary>
    /// Define as propriedades do arquivo    
    /// </summary>
    public enum FileProperties
    {
        Author,      
        LastModifiedBy,  
        Title,
        Description,
        Subject,
        CreationDate,
        ModDate
    }

}
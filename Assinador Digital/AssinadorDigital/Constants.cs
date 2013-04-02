using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssinadorDigital
{
    class Constants
    {

        public static string NotValidFormat =
            "Os arquivos selecionados não são dos formatos suportados pela aplicação.";

        public static string InsufficientMemory =
            "Memoria insuficiênte para executar a aplicação.";

        public static string DisposedDocument =
            "O Documento foi fechado ou descartado, portanto não é possível realizar operações com o mesmo.";

        public static string DontOverride =
            "Você optou por não sobrescrever as cópias de segurança caso elas já existam.\n" +
            "Deseja ainda assim prosseguir em remover as assinaturas de todos os documentos originais selecionados?";

    }
}

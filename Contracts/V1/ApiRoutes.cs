using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PiTech.API.Contracts.V1
{

    public static class ApiRoutes
    {

        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Presencas
        {
            public const string ObterTodas = Base + "/presencas";
            public const string Obter = Base + "/presencas/{id}";
            public const string Deletar = Base + "/presencas/{id}";
            public const string Atualizar = Base + "/presencas/{id}";
            public const string Criar = Base + "/presencas";
        }

        public static class Identity
        {
            public const string Login = Base + "/identity/login";
            public const string Register = Base + "/identity/register";
            public const string Refresh = Base + "/identity/refresh";
        }
    }
}

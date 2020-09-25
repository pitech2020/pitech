using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presence.API.Contracts.V1
{
    /// <summary>
    /// 
    /// </summary>
    public static class ApiRoutes
    {
        /// <summary>
        /// 
        /// </summary>
        public const string Root = "api";

        /// <summary>
        /// 
        /// </summary>
        public const string Version = "v1";

        /// <summary>
        /// 
        /// </summary>
        public const string Base = Root + "/" + Version;

        /// <summary>
        /// 
        /// </summary>
        public static class Presencas
        {
            /// <summary>
            /// 
            /// </summary>
            public const string ObterTodas = Base + "/presencas";

            /// <summary>
            /// 
            /// </summary>
            public const string Obter = Base + "/presencas/{id}";

            /// <summary>
            /// 
            /// </summary>
            public const string Deletar = Base + "/presencas/{id}";

            /// <summary>
            /// 
            /// </summary>
            public const string Atualizar = Base + "/presencas/{id}";

            /// <summary>
            /// 
            /// </summary>
            public const string Criar = Base + "/presencas";
        }

        /// <summary>
        /// 
        /// </summary>
        public static class Identity
        {
            /// <summary>
            /// 
            /// </summary>
            public const string Login = Base + "/identity/login";

            /// <summary>
            /// 
            /// </summary>
            public const string Register = Base + "/identity/register";

            /// <summary>
            /// 
            /// </summary>
            public const string Refresh = Base + "/identity/refresh";
        }
    }
}

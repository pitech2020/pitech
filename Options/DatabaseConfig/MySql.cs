using Presence.API.Extensions;
using Presence.API.Options.DatabaseConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Presence.API.Options
{
    /// <summary>
    /// 
    /// </summary>
    public class MySql : IConnectionStrings
    {
        private string _connectionString;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public MySql(string connectionString)
        {
            this._connectionString = connectionString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ObterConnectionString()
        {
            return this._connectionString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DatabaseTypes ObterTipoBancoDados()
        {
            return DatabaseTypes.MySql;
        }
    }
}

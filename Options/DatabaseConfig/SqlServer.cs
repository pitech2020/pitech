using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presence.API.Options.DatabaseConfig
{
    /// <summary>
    /// 
    /// </summary>
    public class SqlServer : IConnectionStrings
    {
        private string _connectionString;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public SqlServer(string connectionString)
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
            return DatabaseTypes.SqlServer;
        }
    }
}

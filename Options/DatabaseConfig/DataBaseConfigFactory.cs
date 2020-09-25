using Presence.API.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presence.API.Options.DatabaseConfig
{
    /// <summary>
    /// 
    /// </summary>
    public class DataBaseConfigFactory
    {
        private DatabaseTypes _tipo;
        private string _connectionString;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseConfig"></param>
        public DataBaseConfigFactory(DatabaseConfig databaseConfig)
        {
            this._tipo = databaseConfig.Server.ObterValorEnum<DatabaseTypes>();
            this._connectionString = databaseConfig.ConnectionString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IConnectionStrings ObterImplementacao()
        {
            switch (_tipo)
            {
                case DatabaseTypes.MySql:
                    return new MySql(this._connectionString);
                case DatabaseTypes.SqlServer:
                    return new SqlServer(this._connectionString);
                default:
                    throw new NotImplementedException("CosmosDb não implementado");
            }
        }
    }
}

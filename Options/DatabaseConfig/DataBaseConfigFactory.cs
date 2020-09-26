using PiTech.API.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PiTech.API.Options.DatabaseConfig
{
    public class DataBaseConfigFactory
    {
        private DatabaseTypes _tipo;
        private string _connectionString;
        public DataBaseConfigFactory(DatabaseConfig databaseConfig)
        {
            this._tipo = databaseConfig.Server.ObterValorEnum<DatabaseTypes>();
            this._connectionString = databaseConfig.ConnectionString;
        }

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

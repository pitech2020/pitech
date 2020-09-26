using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PiTech.API.Options.DatabaseConfig
{
    public class SqlServer : IConnectionStrings
    {
        private string _connectionString;
        public SqlServer(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public string ObterConnectionString()
        {
            return this._connectionString;
        }

        public DatabaseTypes ObterTipoBancoDados()
        {
            return DatabaseTypes.SqlServer;
        }
    }
}

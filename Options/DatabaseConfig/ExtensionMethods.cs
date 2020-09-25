using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presence.API.Options.DatabaseConfig
{
    public static class ExtensionMethods
    {
        public static DbContextOptionsBuilder Use(this DbContextOptionsBuilder options, IConnectionStrings databaseConfig)
        {
            var cs = databaseConfig.ObterConnectionString();
            var tipo = databaseConfig.ObterTipoBancoDados();

            switch (tipo)
            {
                case DatabaseTypes.MySql:
                    return options.UseMySql(cs);
                case DatabaseTypes.SqlServer:
                    return options.UseSqlServer(cs);
                default: return null;
            }
        }
    }
}

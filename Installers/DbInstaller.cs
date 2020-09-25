using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presence.API.Data;
using Presence.API.Extensions;
using Presence.API.Options;
using Presence.API.Options.DatabaseConfig;
using Presence.API.Services;

namespace Presence.API.Installers
{
    /// <summary>
    /// 
    /// </summary>
    public class DbInstaller : IInstaller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var databaseConfig = new DatabaseConfig();

            configuration.Bind(key: nameof(databaseConfig), databaseConfig);

            var connectionString = new DataBaseConfigFactory(databaseConfig)
                .ObterImplementacao();

            services.AddDbContext<DataContext>(
                    options => {
                        options.Use(connectionString);
                    }
                );
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<DataContext>();

            services.AddScoped<IPresencaService, PresencaService>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PiTech.API.Data;
using PiTech.API.Extensions;
using PiTech.API.Options;
using PiTech.API.Options.DatabaseConfig;
using PiTech.API.Services;

namespace PiTech.API.Installers
{

    public class DbInstaller : IInstaller
    {
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

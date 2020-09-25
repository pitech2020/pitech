using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presence.API.Installers
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInstaller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}

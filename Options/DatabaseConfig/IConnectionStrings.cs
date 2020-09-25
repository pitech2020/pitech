using Presence.API.Options.DatabaseConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presence.API.Options
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConnectionStrings
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string ObterConnectionString();
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        DatabaseTypes ObterTipoBancoDados();
    }
}

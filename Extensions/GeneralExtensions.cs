using Microsoft.AspNetCore.Http;
using Presence.API.Options.DatabaseConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Presence.API.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class GeneralExtensions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static string ObterIdUsuario(this HttpContext httpContext) =>
           httpContext.User == null 
            ? string.Empty 
            : httpContext.User.Claims.Single(u => u.Type == "id").Value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static string SepararCamelCase(this string texto)
        => Regex.Replace(Regex.Replace(texto, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
        

        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        public static T ObterValorEnum<T>(this string value) => (T)Enum.Parse(typeof(T), value, true);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Presence.API.Domain;

namespace Presence.API.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class DataContext : IdentityDbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Presenca> Presencas { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Presence.API.Domain
{
    public class RefreshToken
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string JwtId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DataCriacao { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DataExpiracao { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Usado { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool NaoValidado { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }
    }
}

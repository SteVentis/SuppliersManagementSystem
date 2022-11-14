using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.IdentityModels
{
    public class LoginModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

#nullable enable
        public long Id { get; set; }

        public string? UserName { get; set; }
        
        public string? Password { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpireTime { get; set; }
    }
}

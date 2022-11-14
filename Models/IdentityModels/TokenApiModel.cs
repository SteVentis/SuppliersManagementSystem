using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.IdentityModels
{
    public class TokenApiModel
    {
#nullable enable
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}

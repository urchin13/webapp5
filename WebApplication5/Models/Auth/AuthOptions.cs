using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication5.Models.Auth
{
    public class AuthOptions
    {
        public const string ISSUER = "Igorr";
        public const string AUDIENCE = "WebApp5";
        const string KEY = "AQAAAAEAACcQAAAAEEVqjVieNa6as0mjVuq2MaGIskTW0KFgU7wJ8MYySHMCTrlzQOHw";
        public const int LIFETIME = 60;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}

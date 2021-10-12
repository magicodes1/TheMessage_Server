using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMessage.Common
{
    public  class Hash
    {
        public static SymmetricSecurityKey Hashkey(string key) => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    }

}

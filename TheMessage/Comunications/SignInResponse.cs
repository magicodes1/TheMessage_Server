using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheMessage.Resources;

namespace TheMessage.Comunications
{
    public class SignInResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }

        public UserResource User { get; set; }
    }
}

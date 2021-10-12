using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheMessage.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool isOnline { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}

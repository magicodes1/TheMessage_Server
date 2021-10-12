using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheMessage.Resources;

namespace TheMessage.Comunications
{
    public class MessageResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public MessageResource MessageContent { get; set; }
    }
}

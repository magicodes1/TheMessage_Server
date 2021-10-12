using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TheMessage.Exceptions
{
    public class NotFoundException : Exception
    {
        public HttpStatusCode Status { get; private set; } = HttpStatusCode.NotFound;

        public NotFoundException(string msg) : base(msg)
        {
            
        }
    }
}

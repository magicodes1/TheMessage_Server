using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TheMessage.Exceptions
{
    public class BadRequestException : Exception
    {
        public HttpStatusCode Status { get; private set; } = HttpStatusCode.BadRequest;

        public BadRequestException(string msg) : base(msg)
        {

        }
    }
}

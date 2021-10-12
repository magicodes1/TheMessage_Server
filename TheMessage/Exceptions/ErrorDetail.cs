using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace TheMessage.Exceptions
{
    public class ErrorDetail
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public DateTime Time { get; set; }

        public ErrorDetail(Exception ex,int statusCode,DateTime time)
        {
            StatusCode = statusCode;
            Message = ex.Message;
            Time = time;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}

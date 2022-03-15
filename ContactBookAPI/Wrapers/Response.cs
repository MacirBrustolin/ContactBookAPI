using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactBookAPI.Wrapers
{
    public class Response<T>
    {
        public Response()
        {
        }
        public Response(T data, string message)
        {
            Succeeded = true;
            Message = message;
            Errors = null;
            Data = data;
        }
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
        public string Message { get; set; }
    }
}

using System;
using System.Net;

namespace CobranzaAPI.Core.Exceptions
{
    public class AppException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public AppException() : base("Se produjo un error en la aplicación!")
        {
            StatusCode = HttpStatusCode.InternalServerError;
        }

        public AppException(string message, HttpStatusCode statusCode) : base(message)
        {
            this.StatusCode = statusCode;
        }

        public AppException(string message, HttpStatusCode statusCode, Exception innerException) : base(message, innerException)
        {
            this.StatusCode = statusCode;
        }
    }
}
using System.Net;

namespace CobranzaAPI.Core.Exceptions
{
    public class AppNotFoundException : AppException
    {
        public AppNotFoundException() : base("Información no encontrada!", HttpStatusCode.NotFound)
        {
        }
    }
}
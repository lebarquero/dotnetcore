using System.Collections.Generic;
using System.Linq;
using System.Net;
using CobranzaAPI.Core.Infrastructure;

namespace CobranzaAPI.Core.Exceptions
{
    public class AppValidationException : AppException
    {
        public AppValidationException() : base("Se produjo uno o más fallos en las validaciones de negocios!", HttpStatusCode.BadRequest)
        {
            Failures = new Dictionary<string, string[]>();
        }

        public AppValidationException(List<ValidationFailure> failures) : this()
        {
            var propertyNames = failures.Select(e => e.PropertyName).Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures.Where(e => e.PropertyName == propertyName).Select(e => e.ErrorMessage).ToArray();
                Failures.Add(propertyName, propertyFailures);
            }
        }

        public IDictionary<string, string[]> Failures { get; }
    }
}
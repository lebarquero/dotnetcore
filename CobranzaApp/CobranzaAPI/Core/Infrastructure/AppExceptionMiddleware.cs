using System;
using System.Net;
using System.Threading.Tasks;
using CobranzaAPI.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CobranzaAPI.Core.Infrastructure
{
    public class AppExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public AppExceptionMiddleware(RequestDelegate next, ILogger<AppExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("La respuesta ya se inicio, por lo tanto el AppExceptionMiddleware no se ejecutara!");
                    throw;
                }
                
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var feature = context.Features.Get<IExceptionHandlerPathFeature>();
            // var exception = feature.Error;
            var exceptionType = exception.GetType();
            var status = HttpStatusCode.InternalServerError;
            string result = string.Empty;

            if (exceptionType == typeof(DbUpdateException))
            {
                status = HttpStatusCode.InternalServerError;
                result = "{\"title\":\"Se produjo un error en la BD de la aplicación!\", \"status\":500}";
            }
            else if (exceptionType == typeof(DbUpdateConcurrencyException))
            {
                status = HttpStatusCode.NotFound;
                result = "{\"title\":\"Información no encontrada!\", \"status\":404}";
            }
            else if (exceptionType == typeof(AppValidationException))
            {
                status = ((AppValidationException)exception).StatusCode;
                result = JsonConvert.SerializeObject(new { errors = ((AppValidationException)exception).Failures, title = exception.Message, status = (int)status });
            }
            else if (exception is AppException)
            {
                status = ((AppException)exception).StatusCode;
                result = JsonConvert.SerializeObject(new { title = exception.Message, status = (int)status });
            }
            else
            {
                result = "{\"title\":\"Se produjo un error en la aplicación!\", \"status\":500}";
            }

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)status;
            await context.Response.WriteAsync(result);
        }
    }
}
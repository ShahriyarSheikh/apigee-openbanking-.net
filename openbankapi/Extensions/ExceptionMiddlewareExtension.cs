
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using openbankapi.models.Models;
using System.Net;

namespace openbankapi.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger<Startup> _logger, bool isDevelopment)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        //Use App insights here
                        //_logger.TrackException(contextFeature.Error, contextFeature.Error.Message, null);
                        _logger.LogError(contextFeature.Error, contextFeature.Error.Message);

                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = isDevelopment ? contextFeature.Error.ToString() : "We are working on it. Please try again later."
                        }.ToString());
                    }
                });
            });
        }
    }
}

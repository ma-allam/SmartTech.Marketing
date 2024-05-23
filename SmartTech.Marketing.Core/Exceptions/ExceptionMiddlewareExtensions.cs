using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;

namespace SmartTech.Marketing.Core.Exceptions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                        if (contextFeature.Error is WebApiException except)
                        {
                            context.Response.Headers.Append("EXCEPTION", except.ToString());
                            await context.Response.WriteAsync(except.ToString());
                        }
                        else
                        {
                            var exception = new WebApiException(contextFeature.Error, WebApiExceptionSource.GeneralException,
                                "internal_server_error");
                            context.Response.Headers.Append("EXCEPTION", exception.ToString());
                            await context.Response.WriteAsync(exception.ToString());

                        }

                    }
                });
            });
        }


    }
}

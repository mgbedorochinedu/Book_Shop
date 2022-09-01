using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Shop.Dtos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Book_Shop.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(error => { 
                error.Run(async context =>
                {
                    var logger = loggerFactory.CreateLogger("ConfigureExceptionHandler");

                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var contextRequest = context.Features.Get<IHttpRequestFeature>();

                    if (contextFeature != null)
                    {
                        var errorVMString = new Error
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error. Please Try Again Later.",
                            Path = contextRequest.Path 
                        }.ToString();

                        logger.LogError($"Something went wrong : {contextFeature.Error.Message}");

                        await context.Response.WriteAsync(errorVMString);
                    }
                });
            });
        }



    }
}

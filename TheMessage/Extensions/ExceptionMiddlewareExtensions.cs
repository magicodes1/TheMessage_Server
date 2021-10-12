using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TheMessage.Exceptions;

namespace TheMessage.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {

                    int code = (int) HttpStatusCode.InternalServerError;

                    context.Response.ContentType = "application/json";


                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();


                    var exception = exceptionHandlerPathFeature?.Error;

                    if (exception is BadRequestException)
                    {
                        code = (int) HttpStatusCode.BadRequest;
                    }
                    else if (exception is NotFoundException)
                    {
                        code = (int) HttpStatusCode.NotFound;
                    }
                    

                    context.Response.StatusCode = code;

                    var errorResponse = new ErrorDetail(exception, code,DateTime.UtcNow);

                    await context.Response.WriteAsync(errorResponse.ToString());
                });
            });
        }
    }
}

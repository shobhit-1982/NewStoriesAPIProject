using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using StoriesAPI.Models.Common;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;


namespace StoriesAPI.Middleware
{
    /// <summary>
    /// Request Response Exception.
    /// </summary>
    public static class ExceptionMiddlewareExtensions
    {
        /// <summary>
        /// Extensan mathord
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
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
                //logger.LogError($"Something went wrong: {contextFeature.Error}");
                await context.Response.WriteAsync(new ErrorDetails()
                {
                  StatusCode = context.Response.StatusCode,
                  Message = "Internal Server Error."
                }.ToString());
              }
            });
          });
        }
  }
}

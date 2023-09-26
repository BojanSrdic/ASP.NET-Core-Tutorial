using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Exception_Handling.GlobalErrorHandling.BuiltinMiddleware.Extensions
{
	public static class ExceptionMiddlewareExtension
	{
		public static void ConfigureExceptionHandler(this IApplicationBuilder app)
		{
			app.UseExceptionHandler(appError => {
				appError.Run(async context => {
					context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					context.Response.ContentType = "application/json";

					var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
					if(contextFeature != null)
					{
						await context.Response.WriteAsync(new ErrorDetails { 
							StatusCode = context.Response.StatusCode,
							Message = "Internal Server Error"
						}.ToString());
					}
				});
			});
		}

		//// Register Extension Middleware for custom Exception Handler
		//public static void ConfigureCustomExtensionMiddleware(this IApplicationBuilder app)
		//{
		//	app.UseMiddleware<CustomExceptionMiddleware>();
		//}
	}
}

// In order to use this methode in Startup.cs class in configure methode set app.ConfigureExceptionHandler();

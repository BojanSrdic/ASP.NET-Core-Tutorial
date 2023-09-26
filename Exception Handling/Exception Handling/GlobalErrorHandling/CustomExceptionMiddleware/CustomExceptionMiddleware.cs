using Exception_Handling.GlobalErrorHandling.BuiltinMiddleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Exception_Handling.GlobalErrorHandling.CustomExceptionMiddleware
{
	public class CustomExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		//private readonly ILoggerManager _logger;

		public CustomExceptionMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try 
			{
				await _next(httpContext);
			} 
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				await HandleExceptionAsync(httpContext);
			}
			finally
			{
				
			}
		}

		private Task HandleExceptionAsync(HttpContext context)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

			return context.Response.WriteAsync(new ErrorDetails
			{
				StatusCode = context.Response.StatusCode,
				Message = "Internal Server Error from the custom middleware"
			}.ToString()); ;
		}
	}
}

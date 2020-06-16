using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ResourceProvisioning.Broker.Host.Api.Infrastructure.Middleware
{
	public class CustomExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger _logger;

		public CustomExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
		{
			_logger = loggerFactory.CreateLogger<CustomExceptionMiddleware>();
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
				_logger.LogError("CustomException occured");

				await HandleExceptionAsync(httpContext, ex, "Custom error");
			}
		}

		private static Task HandleExceptionAsync(HttpContext context, Exception exception, string message)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

			return context.Response.WriteAsync(new
			{
				context.Response.StatusCode,
				Message = message
			}.ToString());
		}
	}
}

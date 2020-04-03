using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace ResourceProvisioning.Broker.Host.Api.Infrastructure.Middleware
{
	public class CustomExceptionMiddleware
	{
		private const string CustomExceptionMiddlewareExceptionResourceKey = "EXCEPTION";
		private const string CustomExceptionMiddlewareMessageResourceKey = "MESSAGE";

		private readonly RequestDelegate _next;
		private readonly ILogger _logger;
		private readonly IStringLocalizer<CustomExceptionMiddleware> _localizer;

		public CustomExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IStringLocalizer<CustomExceptionMiddleware> localizer)
		{
			_logger = loggerFactory.CreateLogger<CustomExceptionMiddleware>();
			_localizer = localizer;
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
				_logger.LogError(string.Format(_localizer.GetString(CustomExceptionMiddlewareExceptionResourceKey), ex));

				await HandleExceptionAsync(httpContext, ex, _localizer.GetString(CustomExceptionMiddlewareMessageResourceKey));
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

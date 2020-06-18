﻿using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ResourceProvisioning.Broker.Host.Api.Infrastructure.Authentication;
using ResourceProvisioning.Broker.Host.Api.Infrastructure.Middleware;

namespace ResourceProvisioning.Broker.Host.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			services.AddCors(options =>
			{
				options.DefaultPolicyName = "open";
				options.AddDefaultPolicy(p =>
				{
					p.AllowAnyHeader();
					p.AllowAnyMethod();
					p.AllowCredentials();
					p.WithExposedHeaders("X-Pagination");
				});
			});

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "Broker API",
					Version = "v1"
				});
			});

			services.AddApplicationInsightsTelemetry("2ded7a03-d47a-4872-9886-08776ddfd311");

			Application.DependencyInjection.AddProvisioningBroker(services, options =>
			{
				Configuration.Bind(options);
			});

            ConfigureAuth(services);
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseCors("open");
			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Broker API V1");
			});

			app.UseMiddleware<CustomExceptionMiddleware>();
		}

		protected virtual void ConfigureAuth(IServiceCollection services)
		{
			services.AddAuthentication(AzureADDefaults.JwtBearerAuthenticationScheme)
					.AddAzureADBearer(options => Configuration.Bind("AzureAd", options));

			services.Configure<JwtBearerOptions>(AzureADDefaults.JwtBearerAuthenticationScheme, options =>
			{
				options.Authority += "/v2.0";

				// The web API accepts as audiences both the Client ID (options.Audience) and api://{ClientID}.
				options.TokenValidationParameters.ValidAudiences = new[]
				{
					options.Audience,
					$"api://{options.Audience}"
				};
				
				// Instead of using the default validation (validating against a single tenant,
				// as we do in line-of-business apps),
				// we inject our own multitenant validation logic (which even accepts both v1 and v2 tokens).
				options.TokenValidationParameters.IssuerValidator = AadIssuerValidator.GetIssuerValidator(options.Authority).Validate; ;
			});
		}
	}
}

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
			services.AddLogging();
			services.AddControllers();

			services.AddCors(options =>
			{
				options.DefaultPolicyName = "open";
				options.AddDefaultPolicy(p =>
				{
					p.AllowAnyHeader();
					p.AllowAnyMethod();

          // Disabled due to the authorization module for asp.net core not allowing any origin.
					//p.AllowAnyOrigin();
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

			Application.DependencyInjection.AddProvisioningBroker(services, options => {
				Configuration.Bind(options);
			});
			
			ConfigureAuth(services);
		}
		
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseCors("open");
			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthorization();
			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Broker API V1");
			});
			
      //
      /// Disabled due to it not working out of the box, and fixing it isn't in the scope of Story #2612 - CLI: login via OpenId
      //
			//app.UseMiddleware<CustomExceptionMiddleware>();
		}
		
		protected virtual void ConfigureAuth(IServiceCollection services)
		{
			services.AddAuthentication(options =>
				{
					options.DefaultAuthenticateScheme = "AzureADBearer";
					options.DefaultChallengeScheme = "AzureADBearer";
				})
				.AddAzureADBearer(options =>
				{
					Configuration.Bind("AzureAd", options);
				});

			services.Configure<JwtBearerOptions>(AzureADDefaults.JwtBearerAuthenticationScheme, options =>
			{
				// This is an Azure AD v2.0 Web API
				options.Authority += "/v2.0";

				// The valid audiences are both the Client ID (options.Audience) and api://{ClientID}
				options.TokenValidationParameters.ValidAudiences = new string[] { options.Audience, $"api://{options.Audience}" };

				// Instead of using the default validation (validating against a single tenant, as we do in line of business apps),
				// we inject our own multitenant validation logic (which even accepts both V1 and V2 tokens)
				options.TokenValidationParameters.IssuerValidator = AadIssuerValidator.ValidateAadIssuer;
			});
		}
	}
}

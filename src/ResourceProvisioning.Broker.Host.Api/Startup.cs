﻿using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ResourceProvisioning.Broker.Application;
using ResourceProvisioning.Broker.Host.Api.Infrastructure.Middleware;
using ResourceProvisioning.Broker.Infrastructure.EntityFramework;

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
					p.AllowAnyOrigin();
					p.AllowCredentials();
					p.WithExposedHeaders("X-Pagination");
				});
			});

			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "Broker API",
					Version = "v1"
				});
			});

			services.AddDbContext<DomainContext>(options => 
			{
				options.UseSqlite(Configuration.GetConnectionString(nameof(DomainContext)),
									sqliteOptionsAction: sqliteOptions => 
									{
										sqliteOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
										sqliteOptions.MigrationsHistoryTable(typeof(Startup).GetTypeInfo().Assembly.GetName().Name + "MigrationHistory");
									});
			}, ServiceLifetime.Scoped);

			//TODO: Register remaining dependencies (eventhandlers, commandhandlers, services, repos, etc).
			services.Configure<ProvisioningBrokerOptions>(Configuration);
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
	}
}

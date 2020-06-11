using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ResourceProvisioning.Cli.Core.Core.Errors;
using ResourceProvisioning.Cli.Core.Core.Repositories;

namespace ResourceProvisioning.Cli.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var inMemoryDesiredStateStateRepository = new InMemoryDesiredStateAndStateRepository();
            services.AddSingleton<IDesiredStateRepository>(inMemoryDesiredStateStateRepository);
            services.AddSingleton<IStateRepository>(inMemoryDesiredStateStateRepository);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var feature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = feature.Error;
            
                switch (exception)
                {
                    case EnvironmentDoesNotExistException _:
                        context.Response.StatusCode = (int) HttpStatusCode.NotFound;
                        break;
                    default:
                        throw exception;
                }
                
                var result = JsonConvert.SerializeObject(new {error = exception.Message});
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
            }));
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}

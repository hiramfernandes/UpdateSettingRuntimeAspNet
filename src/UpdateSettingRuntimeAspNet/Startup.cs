using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UpdateSettingRuntimeAspNet.Configuration;
using UpdateSettingRuntimeAspNet.Model;

namespace UpdateSettingRuntimeAspNet
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) => _configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IWritableOptions<ConnectionStrings>, WritableOptions<ConnectionStrings>>();
            services.ConfigureWritable<ConnectionStrings>(_configuration.GetSection("ConnectionStrings"));
            var connectionString = GetConnectionString(_configuration, services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }

        private string GetConnectionString(IConfiguration configuration, IServiceCollection serviceCollection)
        {
            var connectionString = configuration.GetConnectionString("SpecificConnectionString");
            var writableOptions = (IWritableOptions<ConnectionStrings>)serviceCollection.BuildServiceProvider().GetService<IWritableOptions<ConnectionStrings>>();
            
            if (connectionString.Contains("server"))
            {
                connectionString = connectionString.Replace("server", "_server_");
                writableOptions.Update(opt => opt.SpecificConnectionString = connectionString);
            }
            return connectionString;
        }
    }
}

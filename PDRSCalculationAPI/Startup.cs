using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using PDRSCalculationAPI.Entity;
using PDRSCalculationAPI.Hub;

namespace PDRSCalculationAPI
{
    public class Startup
    {

        private readonly IConfiguration _configuration;
        readonly string MyAllowSpecificOrigins = "MyCorsPolicy";
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddDbContext<PDRSDataContext>(options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));


            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Add Cors
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });

                options.AddPolicy("signalr",
                    builder => builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()

                    .AllowCredentials()
                    .SetIsOriginAllowed(hostName => true));
            });
            

            services.AddControllers();
            services.AddSignalR();
            services.AddControllers().AddJsonOptions(options=>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();
            app.UseRouting();            

            // Enable Cors
            app.UseCors(MyAllowSpecificOrigins);            

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {                
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller}/{id}");

                endpoints.MapHub<ServerSignalR>("/serverSignalR");
            });            
        }
    }
}

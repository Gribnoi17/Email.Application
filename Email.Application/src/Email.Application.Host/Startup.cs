using Email.Application.AppServices.Infrastructure.DependencyConfiguration;
using Email.Application.DataAccess.Infrastructure.DependencyConfiguration;
using Email.Application.Host.Infrastructure.DependencyConfiguration;
using Email.Application.Host.Infrastructure.Middleware;
using MailKit.Net.Smtp;

namespace Email.Application.Host
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        private IWebHostEnvironment WebHostEnvironment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();

            services.AddEmailDbContext(Configuration);

            services.AddSmtpSettings(Configuration);
            
            services.AddRepositories();

            services.AddHandlers();

            services.AddMiddlewareService(Configuration);
            
            services.AddTransient<SmtpClient>();
            
            services.AddSwaggerGen();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
                app.UseSwagger();

                app.UseSwaggerUI();
            }

            app.UseExceptionHandler("/Error");

            app.UseHsts();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseMiddleware<HeaderMiddleware>();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");

                endpoints.MapGet("/", () => "Hello World!");

                endpoints.MapControllers();
            });
        }
    }
}
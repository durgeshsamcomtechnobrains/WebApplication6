using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WebApplication1.Data;
using WebApplication1.Model;
using WebApplication1.Repository;
using WebApplication1.Repository.IRepository;

namespace WebApplication1
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUserService, UserService>();            
            services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddSingleton<ITokenService>(new TokenService(Configuration["Jwt:Key"]));

            services.AddControllers();

            // Register Swagger generator
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // Enable middleware to serve generated Swagger as a JSON endpoint
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.)
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddCors(options =>
        //    {
        //        options.AddPolicy("CorsPolicy", builder =>
        //        {
        //            builder.WithOrigins("http://localhost:3000")
        //                   .AllowAnyMethod()
        //                   .AllowAnyHeader()
        //                   .AllowCredentials();
        //        });
        //    });

        //    services.AddSignalR();
        //}

        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        //{
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //    }
            
        //    app.UseRouting();

        //    app.UseCors("CorsPolicy");
            
        //    app.UseEndpoints(endpoints =>
        //    {
        //        endpoints.MapControllers();
        //    });

        //    app.UseEndpoints(endpoints =>
        //    {
        //        endpoints.MapHub<NotificationHub>("/notificationHub");
        //    });
        //}
    }
}

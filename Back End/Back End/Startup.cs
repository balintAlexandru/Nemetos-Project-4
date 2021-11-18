using Back_End.Data;
using Back_End.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
<<<<<<< HEAD:Back End/MVC Company/Startup.cs
using MVC_Company.Data;
using MVC_Company.Services;
=======
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
>>>>>>> 4091b242df8ac9a447c8e074654863e20601e95f:Back End/Back End/Startup.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End
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
            services.AddDbContext<AdministratorContext>(options => {
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"));
            });
<<<<<<< HEAD:Back End/MVC Company/Startup.cs
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => {
                    options.LoginPath = "/login";
                });

            services.AddTransient<IEmployeeServices, EmployeeServices>();
            services.AddTransient<ISocialMediaServices, SocialMediaServices>();


=======
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Back_End", Version = "v1" });
            });
            services.AddTransient<IAdminServices, AdminServices>();
>>>>>>> 4091b242df8ac9a447c8e074654863e20601e95f:Back End/Back End/Startup.cs
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Back_End v1"));
            }
<<<<<<< HEAD:Back End/MVC Company/Startup.cs
            else
            {
                app.UseStatusCodePages();
                app.UseStatusCodePagesWithRedirects("/ErrorPage");
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
           


=======
>>>>>>> 4091b242df8ac9a447c8e074654863e20601e95f:Back End/Back End/Startup.cs

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

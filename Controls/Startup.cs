using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Controls.Data;
using Controls.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Controls
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
            services.AddDbContext<ControlsDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("ControlsDbConnection")));
            // Commentary from https://medium.freecodecamp.org/authentication-using-google-in-asp-net-core-2-0-5ec32c803e23
            services.AddIdentity<UserObject, UserRole>()
                .AddEntityFrameworkStores<ControlsDbContext>()
                .AddDefaultTokenProviders();
            // Add external authentication providers
            string CId = Configuration["TCGoogleClientID"];
            string CSec = Configuration["TCGoogleSecret"];
            services.AddAuthentication()
                .AddGoogle(o => { o.ClientId = CId; o.ClientSecret = CSec; });
            //                .AddAuthenticationCore(IServiceCollection coreService, Action<AuthenticationOptions> coreConfig);
            // need these services for account recovery or two factor authentication
            services.AddTransient<IEmailSender, ControlsEmailSender>();
            services.AddTransient<IEmailSender, ControlsSmsSender>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}

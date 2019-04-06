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
                .AddGoogle(o => { o.ClientId = CId; o.ClientSecret = CSec; o.CorrelationCookie.SameSite = SameSiteMode.None; })  // same site fix
                .AddPersonal(o => {
                    o.ClientId = "Personal"; o.ClientSecret = "bazzfazz";
                    o.Authority = "did:"; })
                .AddTrust(o => {
                    o.ClientId = "IDESG2rp"; o.ClientSecret = "bazzfazz";
                    o.Authority = "https://idesg-idp.azurewebsites.net"; });
            //                .AddAuthenticationCore(IServiceCollection coreService, Action<AuthenticationOptions> coreConfig);
            // need these services for account recovery or two factor authentication
            services.AddTransient<IEmailSender, ControlsEmailSender>();
            services.AddTransient<IEmailSender, ControlsSmsSender>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAuthentication()    // TODO this was added to overcome iOS 12 same site problem - I tried to remove it, but could not
                .Services.ConfigureExternalCookie(opt => 
                { opt.Cookie = new CookieBuilder { SameSite = SameSiteMode.None }; });
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

            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<ControlsDbContext>().Database.Migrate();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.None });  // try to handle some other way, like below

            app.UseAuthentication();
            /*
            // following code is designed to get around same-site restrictions introduced in Apple iOS 12 --  it only applies to ASP.NET 2.2
            // from https://brockallen.com/2019/01/11/same-site-cookies-asp-net-core-and-external-authentication-providers/
            // problems have been reported on that site using this code with chunked cookies
            app.Use(async (ctx, next) =>
            {
                var schemes = ctx.RequestServices.GetRequiredService<IAuthenticationSchemeProvider>();
                var handlers = ctx.RequestServices.GetRequiredService<IAuthenticationHandlerProvider>();
                foreach (var scheme in await schemes.GetRequestHandlerSchemesAsync())
                {
                    var handler = await handlers.GetHandlerAsync(ctx, scheme.Name) as IAuthenticationRequestHandler;
                    if (handler != null)
                    {
                        bool bRet =  await handler.HandleRequestAsync(); // returns true if request processing should stop
                        // start same-site cookie special handling
                        if (bRet)
                        {
                            string location = null;
                            if (ctx.Response.StatusCode == 302)
                            {
                                location = ctx.Response.Headers["location"];
                            }
                            else if (ctx.Request.Method == "GET" && !ctx.Request.Query["skip"].Any())
                            {
                                location = ctx.Request.Path + ctx.Request.QueryString + "&skip=1";
                            }

                            if (location != null)
                            {
                                ctx.Response.StatusCode = 200;
                                var html = $@"
                        <html><head>
                            <meta http-equiv='refresh' content='0;url={location}' />
                        </head></html>";
                                await ctx.Response.WriteAsync(html);
                            }
                        }
                        // end same-site cookie special handling
                       
                        return;
                    }
                }

                await next();
            });

 */
            app.UseMvc();
        }
    }
}

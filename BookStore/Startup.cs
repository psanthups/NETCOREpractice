using BookStore.Data;
using BookStore.Helpers;
using BookStore.Models;
using BookStore.Repository;
using BookStore.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()                                                                                                  /*here we configuring the identity framework core and installing all features of addidentity*/
                .AddEntityFrameworkStores<BookStoreContext>();                                                                                                  /*to work with db we need to provide which dbcontext we are using so the identity can work with that dbcontext.  here used ApplicationUser in place of IdentityUser cause we inherited this cls from that*/

            services.Configure<IdentityOptions>(Options =>                                                                                                         /*here we are configuring the password (custom requirements) */
            {
                Options.Password.RequiredLength = 5;
                Options.Password.RequiredUniqueChars = 1;
                Options.Password.RequireDigit = false;
                Options.Password.RequireLowercase = false;
                Options.Password.RequireNonAlphanumeric = false;
                Options.Password.RequireUppercase = false;
            });

            services.ConfigureApplicationCookie(config =>                                                                                                           /*here we using this service to configure login path if the user not loggedin for a athorized page*/
            {
                config.LoginPath = _configuration["Application:LoginPath"];                                                                                         /*here we can use "/login"  ofter = simply. but we are doing samething by reading appsettings from appsettings.json file*/
            });

            services.AddControllersWithViews();
#if DEBUG

            services.AddRazorPages().AddRazorRuntimeCompilation();

            //uncomment this code to disable the clientside validation (add it at end of above services line)
            //    .AddViewOptions(option =>
            //{
            //    option.HtmlHelperOptions.ClientValidationEnabled = false;
            //});
#endif  
            services.AddScoped<IBookRepository, BookRepository>();                                                                         //here we resolving dependency using this addscoped method (dependency we used in BookRepository class) : By registering these repositors in scoped pattern
            services.AddScoped<ILangugeRepository, LangugeRepository>();
            services.AddSingleton<IMessageRepository, MessageRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();                       /*here we telling our application that we are using the custom varsion of UserClaimsPrincipalFactory cls by regestering this service hare*/

            services.Configure<NewBookAlertConfig>("InternalBook", _configuration.GetSection("NewBookAlert"));                            /*Here we configuring IOption configuration by using services in configure services method*/
            services.Configure<NewBookAlertConfig>("ThirdPartyBook", _configuration.GetSection("ThirdPartyBook"));                         /*if the configuration done in this way then the second one override the first one so to resolve this we use named configs by passing name of configs(any name) as first parameters ex: InternalBook*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            /*app.UseStaticFiles(new StaticFileOptions()  //this is for access static files from other location
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "MyStaticFiles")),
                RequestPath = "/MyStaticFiles"
            });*/

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                //endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();

                /* endpoints.MapControllerRoute(                                                                            //this route is used to get Below "BookApp" after the localhost as manual route to get whatever name should present in that url after localhost
                     name : "default",     
                     pattern : "BookApp/{controller=Home}/{Action=Index}/{Id?}");*/

                //endpoints.MapControllerRoute(                                                                            //this is a conventional routing here we get the page using just "About-us" after local host. here no need mention controller ofter localhost. 
                //    name: "default",                                                                                       //we can do this easily by attribute routing where we no need to write these three lines of code.in attribute route we simply write [Route("about-us")] before action method.
                //    pattern: "About-us/{id?}",
                //    defaults: new { controller = "Home", action = "Aboutus" });

                /*Get("/", async context =>                                                                                //it is the default route we get when we create a project of mvc
                {
                    await context.Response.WriteAsync("Hello World!");
                });*/
            });
        }
    }
}

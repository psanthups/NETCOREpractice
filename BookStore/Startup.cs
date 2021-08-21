using BookStore.Data;
using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

            services.AddControllersWithViews();
#if DEBUG

            services.AddRazorPages().AddRazorRuntimeCompilation();

            //uncomment this code to disable the clientside validation (add it at end of above services line)
            //    .AddViewOptions(option =>
            //{
            //    option.HtmlHelperOptions.ClientValidationEnabled = false;
            //});
#endif  
            services.AddScoped<IBookRepository, BookRepository>();                                                       //here we resolving dependency using this addscoped method (dependency we used in BookRepository class)
            services.AddScoped<ILangugeRepository, LangugeRepository>();

            services.Configure<NewBookAlertConfig>(_configuration.GetSection("NewBookAlert"));                            /*Here we configuring IOption configuration by using services in configure services method*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            /*app.UseStaticFiles(new StaticFileOptions()  //this is for acces static files from other location
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "MyStaticFiles")),
                RequestPath = "/MyStaticFiles"
            });*/

            app.UseRouting();

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

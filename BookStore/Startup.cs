using BookStore.Data;
using BookStore.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BookStoreContext>(
                options => options.UseSqlServer("Server=RAVIKUMAR-PC;Database=BookStore;Integrated Security=True;"));

            services.AddControllersWithViews();
#if DEBUG

            services.AddRazorPages().AddRazorRuntimeCompilation();

            //uncomment this code to disable the clientside validation (add it at end of above services line)
            //    .AddViewOptions(option =>
            //{
            //    option.HtmlHelperOptions.ClientValidationEnabled = false;
            //});
#endif  
            services.AddScoped<BookRepository, BookRepository>();                                         //here we resolving dependency using this addscoped method (dependency we used in BookRepository class)
            services.AddScoped<LangugeRepository, LangugeRepository>();
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
                 endpoints.MapDefaultControllerRoute();

                /* endpoints.MapControllerRoute(  //this route is used to get Below "BookApp" after the localhost as manual route to get whatever name should present in that url after localhost
                     name : "default",     
                     pattern : "BookApp/{controller=Home}/{Action=Index}/{Id?}");*/
                 
                 /*Get("/", async context =>      //it is the default route we get when we create a project of mvc
                 {
                     await context.Response.WriteAsync("Hello World!");
                 });*/
             });
        }
    }
}

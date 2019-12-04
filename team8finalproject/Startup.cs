using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

//TODO: Once you have a database on Azure, you will need to uncomment this line 
//and make the name match your project name
//using team8finalproject.DAL;

//TODO: Update this namespace to match the name of your project
//Ex.  namespace Gray_Katie_HWX
namespace team8finalproject
{ 
    public class Startup
{
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();
        //TODO: Add a connection string here once you have created it on Azure
        //String connectionString = "";

        //TODO: Uncomment this line once you have your connection string
        //services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

        //TODO: Uncomment these lines once you have added Identity to your project
        ////NOTE: This is where you would change your password requirements
        /*services.AddIdentity<AppUser, IdentityRole>(opts => {
            opts.User.RequireUniqueEmail = true;
            opts.Password.RequiredLength = 6;
            opts.Password.RequireNonAlphanumeric = false;
            opts.Password.RequireLowercase = false;
            opts.Password.RequireUppercase = false;
            opts.Password.RequireDigit = false;
        })
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();*/

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        //These lines allow you to see more detailed error messages
        app.UseDeveloperExceptionPage();
        app.UseStatusCodePages();

        //This line allows you to use static pages like style sheets and images
        app.UseStaticFiles();

        //This line configures the routing for MVC
        app.UseMvc(routes => {
            routes.MapRoute(
                name: "default",
                template: "{controller}/{action}/{id?}",
                defaults: new { controller = "Home", action = "Index" });
        });

        //TODO: Once you have added Identity into your project, you will need to uncomment this line
        //app.UseAuthentication();
    }
}
}

// Import the important stuff
// Remember: these are namespaces
using WorkshopDev6B.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// Enter the namespace of the project
namespace WorkshopDev6B {
    /// <summary>
    /// The Startup class
    /// This is where you configure your website
    /// </summary>
    public class Startup {
        // The constructor
        public Startup( IConfiguration configuration ) {
            Configuration = configuration;
        }

        // A class variable that stores the configuration
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This is where the services that are required by the project are configured
        /// </summary>
        /// <param name="services">The collection of services</param>
        public void ConfigureServices( IServiceCollection services ) {
            // We add a services for our database to the collection of services
            services.AddDbContext<StudyContext>(
                // The database is configured by providing a connection string
                options => options.UseNpgsql( @"Host=localhost;Database=trialStudies;Username=postgres;Password=postgres" )
            );

            // Configures MVC
            services.AddMvc();
        }

        // This method is for changing the http configurations
        public void Configure( IApplicationBuilder app, IHostingEnvironment env ) {
            if ( env.IsDevelopment() ) {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware( new WebpackDevMiddlewareOptions {
                    HotModuleReplacement = true,
                    ReactHotModuleReplacement = true
                } );
            }
            else {
                app.UseExceptionHandler( "/Home/Error" );
            }

            app.UseStaticFiles();

            app.UseMvc( routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}" );

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" } );
            } );
        }
    }
}

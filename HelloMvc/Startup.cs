using System;
using Microsoft.AspNet.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ninject;
using Microsoft.Framework.DependencyInjection.Ninject;

namespace HelloMvc
{
    public class Startup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            // Create a new Ninject kernel for your bindings
            var kernel = new StandardKernel();

            // Set up your bindings for DI
            kernel.Load<HelloMvcModule>();

            // Add all the ASP.NET services to your Ninject kernel
            kernel.Populate(services);

            // Use Ninject to return an instance
            return kernel.Get<IServiceProvider>();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            
            app.UseIISPlatformHandler();
            
            app.UseDeveloperExceptionPage();

            app.UseMvcWithDefaultRoute();

            app.UseWelcomePage();
        }
    }
}
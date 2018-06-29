using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace DemoExceptionWebApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Exception Generator: Try pass counter value after URL to generate exception, example: http://YOUR_URL/?counter=10. It will raise exception between 1 to 3 second.");

                if (int.TryParse(context.Request.Query.SingleOrDefault(p => p.Key.ToLower() == "counter").Value, out var counter))
                {
                    ExcentionGenerator(counter);
                }
            });
        }

        private void ExcentionGenerator(int counter)
        {
            var rand = new Random();
            for (var i = 0; i < counter; i++)
            {
                Thread.Sleep(rand.Next(1000, 3000));
                try
                {
                    throw new NotSupportedException();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}

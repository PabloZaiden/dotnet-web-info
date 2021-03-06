﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace dotnet_web_info
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
                using (var process = new Process())
                {
                    process.StartInfo.FileName = "uptime";
                    process.StartInfo.RedirectStandardOutput = true;
                    process.Start();
                    var uptime = process.StandardOutput.ReadToEnd();
                    await context.Response.WriteAsync("Hello World!" + Environment.NewLine);
                    await context.Response.WriteAsync($"The hostname is: {Environment.MachineName}" + Environment.NewLine );
                    await context.Response.WriteAsync(uptime?.Trim());
                }
            });
        }
    }
}

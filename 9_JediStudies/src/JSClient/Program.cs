﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace JSClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = typeof(Program).AssemblyQualifiedName;

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://localhost:5003")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }
    }
}

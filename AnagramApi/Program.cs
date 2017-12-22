using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AnagramApi
{
  public class Program
  {
    public static void Main(string[] args)
    {
      BuildWebHost(args).Run();
    }

    public static IWebHost BuildWebHost(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
                .CaptureStartupErrors(false) //HB do not capture the errors on startup it leads to information disclosure.
                .UseApplicationInsights()
                //.ConfigureLogging((hostingContext, logging)=> 
                //{
                //  logging.AddEventSourceLogger(); //this is the ETW
                //  //logging.AddAzureWebAppDiagnostics(); // no need for .Net Core
                //})
                .UseStartup<Startup>()
                .Build();
  }
}

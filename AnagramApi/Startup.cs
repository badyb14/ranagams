using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AngramApi;
using AngramApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace AnagramApi
{
  public class Startup
  {
    public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
    {
      Configuration = configuration;
      AnagramExtensions.LoggerFactory = loggerFactory;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      //App logic
      services.AddFileBasedAnagramService(Configuration);
      services.AddDbContext<AnagramUsage>(opt => opt.UseInMemoryDatabase("AngramUsage"));

      //Versioning
      services.AddApiVersioning(apiVersioningOptions =>
      {
        apiVersioningOptions.DefaultApiVersion = new ApiVersion(1, 0);
        apiVersioningOptions.ReportApiVersions = true;
      });

      //Security
      services.Configure<MvcOptions>(options=>
      {
        options.Filters.Add(new RequireHttpsAttribute());
      });

      services.AddAntiforgery(); // will only make a difference for post or put API calls
      services.AddMvc();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseStatusCodePages("text/plain", "Status code page, status code: {0}");
      }
      
      app.UseMvc();
    }
  }
}

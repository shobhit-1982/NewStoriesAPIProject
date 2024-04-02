using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StoriesAPI.AutoMapper;
using StoriesAPI.Builders;
using StoriesAPI.Builders.Factory;
using StoriesAPI.Caching.Abstraction;
using StoriesAPI.Caching.Services;
using StoriesAPI.Middleware;
using StoriesAPI.Services.StoriesServices.Interface;
using StoriesAPI.Services.StoriesServices.Service;
using StoriesAPI.Services.ThirdPartyAPIService;
using System;
using System.Net.Http;

namespace StoriesAPI
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
          services.AddSingleton<IResponseBuilderFactory, ResponseBuilderFactory>();
          services.AddSingleton(new HttpClient() { BaseAddress = new Uri("https://hacker-news.firebaseio.com/v0/") });
          //services.AddSingleton(IMapper, Mapper);
          services.AddScoped<IStoriesService, StoriesService>();
          services.AddSingleton<ICachingService, CachingService>();
          services.AddTransient<IThirdPartyAPI, ThirdPartyAPI>();
            
      //services.AddControllers().AddNewtonsoftJson();
      // Auto Mapper Configurations
      var mapperConfig = new MapperConfiguration(mc =>
          {
            mc.AddProfile(new AutoMapperProfile());
          });

            services.AddCors(options =>
            {
                options.AddPolicy(
                  "CorsPolicy",
                  builder => builder.WithOrigins("http://localhost:4200")
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  //.AllowCredentials()
                  );
            });


            IMapper mapper = mapperConfig.CreateMapper();
          services.AddSingleton(mapper);
          services.AddMemoryCache();
          services.AddControllers();
          services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Coding challenge Services",
                    Description = "Stories Services Swagger UI",
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
         app.UseDeveloperExceptionPage();
      }
      app.ConfigureExceptionHandler();
      app.UseCors("CorsPolicy");
      app.UseRouting();
      app.UseSwagger();
      app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V2");
      });
      app.UseEndpoints(endpoints =>
         {
          endpoints.MapControllers();
         });

        }
  }
}

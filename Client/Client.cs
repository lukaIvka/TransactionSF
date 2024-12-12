﻿using System.Fabric;
using Microsoft.ServiceFabric.Services.Communication.AspNetCore;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace Client
{
    /// <summary>
    /// The FabricRuntime creates an instance of this class for each service type instance.
    /// </summary>
    internal sealed class Client : StatelessService
    {
        public Client(StatelessServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// Optional override to create listeners (like tcp, http) for this service instance.
        /// </summary>
        /// <returns>The collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new ServiceInstanceListener[]
            {
                new ServiceInstanceListener(serviceContext =>
                    new KestrelCommunicationListener(serviceContext, "ServiceEndpoint", (url, listener) =>
                    {
                        ServiceEventSource.Current.ServiceMessage(serviceContext, $"Starting Kestrel on {url}");

                        var builder = WebApplication.CreateBuilder();

                        builder.Services.AddSingleton<StatelessServiceContext>(serviceContext);
                        builder.WebHost
                                    .UseKestrel()
                                    .UseContentRoot(Directory.GetCurrentDirectory())
                                    .UseServiceFabricIntegration(listener, ServiceFabricIntegrationOptions.None)
                                    .UseUrls(url);
                        builder.Services.AddControllersWithViews();
                        builder.Services.AddSession(options =>
                        {
                            options.IdleTimeout = TimeSpan.FromMinutes(30); // Definiši koliko dugo sesija traje
                            options.Cookie.HttpOnly = true;
                            options.Cookie.IsEssential = true;
                        });
                        var app = builder.Build();
                        if (!app.Environment.IsDevelopment())
                        {
                        app.UseExceptionHandler("/Home/Error");
                        }
                        app.UseStaticFiles();
                        app.UseRouting();
                        app.UseSession();
                        app.UseAuthorization();
                        app.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");

                        return app;
                        /*
                        // Dodaj CORS uslugu
                        builder.Services.AddCors(options =>
                        {
                            options.AddPolicy("AllowReactApp", policy =>
                            {
                                policy.WithOrigins("http://localhost:3000") // URL tvoje React aplikacije
                                      .AllowAnyMethod()
                                      .AllowAnyHeader();
                            });
                        });
                        
                        builder.WebHost
                                    .UseKestrel()
                                    .UseContentRoot(Directory.GetCurrentDirectory())
                                    .UseServiceFabricIntegration(listener, ServiceFabricIntegrationOptions.None)
                                    .UseUrls(url);
                        // Dodaj Swagger
                        builder.Services.AddEndpointsApiExplorer();
                        builder.Services.AddSwaggerGen();

                        builder.Services.AddControllersWithViews();
                        builder.Services.AddSession(options =>
                        {
                            options.IdleTimeout = TimeSpan.FromMinutes(30); // Definiši koliko dugo sesija traje
                            options.Cookie.HttpOnly = true;
                            options.Cookie.IsEssential = true;
                        });
                        var app = builder.Build();
                        if (!app.Environment.IsDevelopment())
                        {
                        app.UseExceptionHandler("/Home/Error");
                        }

                        app.UseStaticFiles();

                        // Dodaj Swagger i Swagger UI
                        app.UseSwagger();
                        app.UseSwaggerUI(c =>
                        {
                            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Documentation V1");
                            c.RoutePrefix = "swagger";
                        });

                        // Omogući CORS pre rutiranja
                        app.UseCors("AllowReactApp");
                        app.UseRouting();
                        app.UseSession();
                        app.UseAuthorization();
                        app.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
                        
                        return app;*/

                    }))
            };
        }
    }
}

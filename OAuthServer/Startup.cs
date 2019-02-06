using System;

using IdentityServer4.Stores;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OAuthServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(x => x.AddPolicy("AllowAnyOrigin", y => {
                y.AllowAnyOrigin();
                y.AllowAnyMethod();
                y.AllowAnyHeader();
            }));

            var builder = services.AddIdentityServer()
                .AddTestUsers(Config.GetUsers())
                //.AddInMemoryIdentityResources(Config.GetIdentityResources())
                //.AddClientStore<IClientStore>()
                .AddInMemoryApiResources(Config.GetApis())
                .AddInMemoryClients(Config.GetClients());

            if (Environment.IsDevelopment())
            {
                builder.AddDeveloperSigningCredential();
            }
            else
            {
                var key = IdentityServerBuilderExtensionsCrypto.CreateRsaSecurityKey();
                builder.AddSigningCredential(key);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseIdentityServer();
            //app.UseCors();
        }
    }
}

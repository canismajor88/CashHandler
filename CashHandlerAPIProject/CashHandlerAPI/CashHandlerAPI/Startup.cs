using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashHandlerAPI.Data;
using CashHandlerAPI.Helper;
using CashHandlerAPI.Models;
using CashHandlerAPI.Repos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;

namespace CashHandlerAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            #region services

            IdentityBuilder builder = services.AddIdentityCore<UserCredential>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
            });
            builder.AddDefaultTokenProviders();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("TokenSettings:Key")),
                    ValidIssuer = Configuration["TokenSettings:IssuerLocation"], // location of ASP.NET?
                    ValidAudience = Configuration["TokenSettings:AudienceLocation"]
                };
            });

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .Build();

            });
            //enable cores
            services.AddCors(c =>
            {
                c.AddPolicy("AllowPolicy", options => options.AllowAnyOrigin().AllowAnyMethod());
            });
            //json serializer
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options
                    => options.SerializerSettings.ReferenceLoopHandling
                        = Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson(options =>
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            services.AddControllers();
            #endregion

            #region dependency injection
             services.AddSingleton(Configuration);
             services.AddSingleton<IUserCredentialsRepo, UserCredentialRepo>();
             services.AddSingleton<ITokenGenerator, TokenGenerator>();
             services.AddSingleton<ITokenHelper, TokenHelper>();
             services.AddSingleton<IEmailHelper, EmailHelper>();
             services.Configure<EmailOptions>(Configuration.GetSection("Mailjet"));

             #endregion


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(o => o.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

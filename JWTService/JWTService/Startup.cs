using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Jose;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace JWTService
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region=======net core 2.0========
       //     var secretKey = Base64Url.Decode("secertKey123123123123123");
       //     var tokenValidationParameters = new TokenValidationParameters
       //     {
       //         // Token signature will be verified using a private key.
       //         ValidateIssuerSigningKey = true,
       //         RequireSignedTokens = true,
       //         IssuerSigningKey = new SymmetricSecurityKey(secretKey),

       //         // Token will only be valid if contains "accelist.com" for "iss" claim.
       //         ValidateIssuer = true,//false不验证
       //         ValidIssuer = "GuanHua",

       //         // Token will only be valid if contains "accelist.com" for "aud" claim.
       //         ValidateAudience = true,//false不验证
       //         ValidAudience = "ab7ce2d75193492f9be8fd10ae3e32ff",

       //         // Token will only be valid if not expired yet, with 5 minutes clock skew.
       //         ValidateLifetime = true,
       //         RequireExpirationTime = true,
       //         ClockSkew = new TimeSpan(0, 0, 10),
       //         ValidateActor = false,

       //     };
       //     services.AddAuthentication(options =>
       //     {
       //         options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
       //         options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
       //     })
       //.AddJwtBearer(o =>
       //{
       //    //不使用https
       //    //o.RequireHttpsMetadata = false;
       //    o.TokenValidationParameters = tokenValidationParameters;
       //});
            #endregion

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();





            #region  ======net core 1.1=====
            //var secretKey = new byte[] { 164, 60, 194, 0, 161, 189, 41, 38, 130, 89, 141, 164, 45, 170, 159, 209, 69, 137, 243, 216, 191, 131, 47, 250, 32, 107, 231, 117, 37, 158, 225, 234 };
            var secretKey = Base64Url.Decode("secertKey123123123123123");//[177,231,30,174]
            //var tokenSecretKey1 =  Base64Url.Decode("secert");
            //var keyByteArry = Base64UrlEncoder.DecodeBytes(base64);
            //var tokenSecretKey = Encoding.UTF8.GetBytes("O1eLcx5Re1Y3nrLwqwonYaiHnsg7KZWvBvjiTnTDy4A");


            var tokenValidationParameters = new TokenValidationParameters
            {
                // Token signature will be verified using a private key.
                ValidateIssuerSigningKey = true,
                RequireSignedTokens = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),

                // Token will only be valid if contains "accelist.com" for "iss" claim.
                ValidateIssuer = true,//false不验证
                ValidIssuer = "GuanHua",

                // Token will only be valid if contains "accelist.com" for "aud" claim.
                ValidateAudience = true,//false不验证
                ValidAudience = "ab7ce2d75193492f9be8fd10ae3e32ff",

                // Token will only be valid if not expired yet, with 5 minutes clock skew.
                ValidateLifetime = true,
                RequireExpirationTime = true,
                ClockSkew = new TimeSpan(24, 0,0),
                ValidateActor = false,

            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                TokenValidationParameters = tokenValidationParameters,
            });
            #endregion


            app.UseMvc();
        }
    }
}

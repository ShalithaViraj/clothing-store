
using Clothing.API.Services;
using Clothing.Application.Common.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddClothingWebAPIServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddHttpContextAccessor();

            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            //services.AddHealthChecks()
            //   .AddDbContextCheck<ClothingDBContext>();

            services.AddSwaggerGen(options =>
            {

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Clothing. - Web API",
                    Version = "v1",
                    Description = "",
                    TermsOfService = new Uri("https://example.com/terms")
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                    }
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.RequireHttpsMetadata = false;
                   options.SaveToken = true;
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = configuration["Tokens:Issuer"],
                       ValidAudiences = new List<string>
                       {
                          "webapp"
                       },

                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Tokens:Key"])),
                       ClockSkew = TimeSpan.Zero
                   };
                   options.Events = new JwtBearerEvents
                   {
                       OnMessageReceived = context =>
                       {


                           var accessToken = context.Request.Query["access_token"];

                           if (string.IsNullOrEmpty(accessToken))
                           {
                               accessToken = context.Request.Headers["access_token"];
                           }

                           if (string.IsNullOrEmpty(accessToken))
                           {
                               accessToken = context.Request.Headers["Access_token"];
                           }

                           if (!string.IsNullOrEmpty(accessToken))
                           {
                               context.Token = accessToken;
                           }

                           return Task.CompletedTask;
                       }
                   };
               });

            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = long.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
                o.ValueCountLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            services.AddMvc(options =>
            {
                options.MaxModelBindingCollectionSize = int.MaxValue;
            });

            var allowedOrigins = new List<string>();

            var allowOrigins = configuration.GetValue<string>("AllowedOrigins")
                .Split(",");

            services.AddCors(options =>
            {
                options.AddPolicy(name: "CorsPolicy",
                          builder => builder.WithOrigins(allowOrigins)
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .WithExposedHeaders("Content-Disposition")
                          .SetIsOriginAllowed((x) => true)
                          .AllowCredentials()
                         );
            });



            return services;
        }
    }
}

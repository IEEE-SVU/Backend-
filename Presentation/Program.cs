using Application.Common.CloudinaryManagment;
using Application.Common.PasswordHasher;
using Application.Common.TokenGenerator;
using Application.Common.UserInfo; // Assuming CurrentUserService is here
using Domain.IRepositories;
using Infrastructure.DataBase;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer; // NEW
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens; // NEW
using Microsoft.OpenApi.Models;
using System.Text; // NEW

namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            #region Controller + Swagger Setup
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "IEEE API", Version = "v1" });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter your JWT token below in this format: Bearer {your token}"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
            #endregion

            #region DataBase
            builder.Services.AddDbContext<Context>(
                options => options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                ).UseLazyLoadingProxies()
            );
            #endregion

            #region MediatR Injection
            builder.Services.AddMediatR(typeof(Application.Services.UserServices.RegisterUser.Commands.RegisterUserCommand).Assembly);
            #endregion

            #region Dependency Injection
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
            builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

            builder.Services.Configure<CloudinarySettings>(
                builder.Configuration.GetSection("CloudinarySettings"));

            builder.Services.AddScoped<ICloudinaryService,CloudinaryService>();
            #endregion

            #region JWT Authentication Setup
            // 1. Grab the exact same configuration section you used in TokenGenerator
            var jwtSection = builder.Configuration.GetSection("JWT");
            var secretKey = jwtSection.GetValue<string>("SecretKey");

            if (string.IsNullOrEmpty(secretKey))
            {
                throw new InvalidOperationException("JWT SecretKey is not configured in appsettings.json");
            }

            var key = Encoding.ASCII.GetBytes(secretKey);

            // 2. Configure Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; // Keep false for local development
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // These rules MUST match what you set in TokenGenerator
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),

                    ValidateIssuer = true,
                    ValidIssuer = jwtSection.GetValue<string>("Issuer"),

                    ValidateAudience = true,
                    ValidAudience = jwtSection.GetValue<string>("Audience"),

                    ValidateLifetime = true, // This enforces the expiration!
                    ClockSkew = TimeSpan.Zero // Removes the default 5-minute grace period so it expires exactly when you said it would
                };
            });
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication(); // Reads and validates the token
            app.UseAuthorization();  // Enforces the [Authorize] attribute

            app.MapControllers();

            app.Run();
        }
    }
}
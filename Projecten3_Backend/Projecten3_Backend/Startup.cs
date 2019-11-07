using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;
using Projecten3_Backend.Data;
using Projecten3_Backend.Data.IRepository;
using Projecten3_Backend.Data.Repository;
using Projecten3_Backend.Models;
using System;
using System.Security.Claims;
using System.Text;

namespace Projecten3_Backend
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<MultimedDataInitializer>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITherapistRepository, TherapistRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IChallengeRepository, ChallengeRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddDbContext<Projecten3_BackendContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Projecten3_BackendContext")));
            services.AddOpenApiDocument(c => {
                c.DocumentName = "apidocs";
                c.Title = "Post API";
                c.Version = "v1";
                c.Description = "The Post API documentation description.";
                
                c.DocumentProcessors.Add(new SecurityDefinitionAppender("JWT Token", new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Copy 'Bearer' + valid JWT token into field"
                }));
                
                c.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT Token"));
                
            });

            services.AddIdentity<IdentityUser, IdentityRole>(cfg => cfg.User.RequireUniqueEmail = true).AddEntityFrameworkStores<Projecten3_BackendContext>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Multimed", policy => {
                    policy.RequireClaim(ClaimTypes.Role, "Multimed");
                    policy.RequireRole("Multimed");
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                });

                options.AddPolicy("Therapist", policy => {
                    policy.RequireClaim(ClaimTypes.Role, "Therapist");
                    policy.RequireRole("Therapist");
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                });

                options.AddPolicy("User", policy => {
                    policy.RequireClaim(ClaimTypes.Role, "User");
                    policy.RequireRole("User");
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                });
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true
                };
            });
            
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            services.AddCors(options => options.AddPolicy("AllowAllOrigins", builder => builder.AllowAnyOrigin()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, MultimedDataInitializer multimedDataInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
            app.UseCors("AllowAllOrigins");
            app.UseSwaggerUi3();
            app.UseOpenApi();

            //multimedDataInitializer.InitializeData().Wait();
        }
    }
}

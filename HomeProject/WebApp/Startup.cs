using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BLL.App;
using Contracts.BLL.App;
using Contracts.DAL.App;
using DAL.App.EF;
using DAL.App.EF.AppDataInit;
using Domain.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApp
{
    /// <summary>
    /// Startup class.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Startup constructor.
        /// </summary>
        /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options
                    .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging()
                );
            
            services.AddScoped<IAppUnitOfWork, AppUnitOfWork>(); 
            services.AddScoped<IAppBLL, AppBLL>(); 
            
            services.AddDatabaseDeveloperPageExceptionFilter();
            
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidIssuer = Configuration["JWT:Issuer"],
                            ValidAudience = Configuration["JWT:Issuer"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"])),
                            ClockSkew = TimeSpan.Zero
                        };
                    });

            services
                .AddIdentity<User, Role>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddDefaultUI()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            
            services.AddControllersWithViews();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsAllowAll", builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                });
            });

            // Mapper
            services.AddAutoMapper(
                typeof(DAL.App.DTO.MappingProfiles.AutoMapperProfile),
                typeof(BLL.App.DTO.MappingProfiles.AutoMapperProfile)
            );

            
            // support for API versioning
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
            });
            // support for machine-to-machine API documentation
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
            });
            // support for generating human readable documentation from m-to-m documentation
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var appSupportedCultures = new[]
                {
                    new CultureInfo("en-GB"),
                    new CultureInfo("et-EE")
                };
                
                options.SupportedCultures = appSupportedCultures;
                options.SupportedUICultures = appSupportedCultures;
                options.DefaultRequestCulture = new RequestCulture("en-GB", "en-GB");
                options.SetDefaultCulture("en-GB");
                options.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="apiVersionDescriptionProvider"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            // SetupAppData(app, Configuration);
            // SetupAppData(app); 
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var apiVersionDescription in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{apiVersionDescription.GroupName}/swagger.json",
                        apiVersionDescription.GroupName.ToUpperInvariant()
                        );
                }
            });
            
            app.UseStaticFiles();
            
            app.UseCors("CorsAllowAll"); 

            app.UseRouting();

            app.UseRequestLocalization(app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>()?.Value);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
        
        private static void SetupAppData(IApplicationBuilder app) // private static void SetupAppData(IApplicationBuilder app, IConfiguration configuration)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var ctx = serviceScope.ServiceProvider.GetService<AppDbContext>();
            
            if (ctx != null)
            {
                using var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                using var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<Role>>();

                if (userManager != null && roleManager != null)
                { 
                    DataInit.SeedIdentity(userManager, roleManager);
                }
                else
                { 
                    Console.Write($"No user manager {(userManager == null ? "null" : "ok")} or role manager {(roleManager == null ? "null" : "ok")}!" );
                }
              

                // if (configuration.GetValue<bool>("AppData:SeedData"))
                // {
                //     Console.Write("Seed database");
                //     DataInit.SeedAppData(ctx);
                //     Console.WriteLine(" - done");
                // }
            }
        }
    }
}

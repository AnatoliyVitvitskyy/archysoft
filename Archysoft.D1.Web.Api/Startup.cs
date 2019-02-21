using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Archysoft.D1.Data;
using Archysoft.D1.Data.Entities;
using Archysoft.D1.Data.Repositories.Abstract;
using Archysoft.D1.Data.Repositories.Concrete;
using Archysoft.D1.Model.Auth;
using Archysoft.D1.Model.Mappings;
using Archysoft.D1.Model.Services.Abstract;
using Archysoft.D1.Model.Services.Concrete;
using Archysoft.D1.Model.Settings;
using Archysoft.D1.Web.Api.Utilities.Filters;
using Archysoft.D1.Web.Api.Utilities.Middleware;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace Archysoft.D1.Web.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly ISettingsService _settingsService;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            var jwtSettings = new JwtSettings();

            Configuration.Bind(nameof(JwtSettings), jwtSettings);

            _settingsService = new SettingsService(jwtSettings);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>();

            services.AddIdentity<User, Role>(opts =>
                {
                    opts.Password.RequiredLength = 5;
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequireLowercase = false;
                    opts.Password.RequireUppercase = false;
                    opts.Password.RequireDigit = false;
                    opts.SignIn.RequireConfirmedEmail = true;
                })
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _settingsService.JwtSettings.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settingsService.JwtSettings.Key)),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddCors();
            services.AddResponseCaching();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();
            AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();
            services.AddSingleton(Log.Logger);

            services.AddSingleton<IMapper>(new Mapper(new MapperConfiguration(options =>
            {
                options.CreateMissingTypeMaps = false;
                options.AddProfile<UserMapping>();
            })));

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                config.Filters.Add(new AuthorizeFilter(policy));
                config.Filters.Add(new ValidateModelStateFilter());
            }).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<LoginModel>());

            // Services
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();

            // Repositories
            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
            services.AddTransient<IUserRepository, UserRepository>();

            // Settings
            services.AddSingleton(_settingsService);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDatabaseInitializer databaseInitializer)
        {
            app.Use(async (ctx, next) =>
            {
                await next();
                if (ctx.Response.StatusCode == 204)
                {
                    ctx.Response.ContentLength = 0;
                }
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                databaseInitializer.Initialize();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("X-File-Name"));

            app.UseAuthentication();

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

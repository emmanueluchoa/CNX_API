using AutoMapper;
using CNX_Domain.Application;
using CNX_Domain.AutoMapper;
using CNX_Domain.Interfaces.Application;
using CNX_Domain.Interfaces.Repository;
using CNX_Domain.Interfaces.Services;
using CNX_Domain.Services;
using CNX_Repository.Context;
using CNX_Repository.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CNX
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetValue<string>("CnxTKey"))),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddDbContext<CnxContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CnxConnection")));

            ConfigureAutoMapper(services);
            ConfigureRepositoryDependency(services);
            ConfigureServicesDependecy(services);
            ConfigureApplicationDepency(services);
        }

        private static void ConfigureAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperConfig));
        }

        private static void ConfigureApplicationDepency(IServiceCollection services)
        {
            services.AddScoped<IUserApplication, UserApplication>();
            services.AddScoped<IPlaylistApplication, PlaylistApplication>();
        }

        private static void ConfigureRepositoryDependency(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }

        private static void ConfigureServicesDependecy(IServiceCollection services)
        {
            services.AddScoped<IJWTApi, TokenService>();
            services.AddScoped<IWeatherApi, OpenWeatherService>();
            services.AddScoped<IMusicApi, SpotifyService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(option => option.AllowAnyOrigin()
                                        .AllowAnyMethod()
                                        .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

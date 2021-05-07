using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Maid.Auth.API
{
	public class Startup
	{
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services) {
			services.AddTransient<IReturnUrlParser, ReturnUrlParser>();
			services.AddControllers();
			services.AddDbContext<AppIdentityDbContext>(options => options.UseMySql(Configuration.GetConnectionString("Maid_Auth_ConnectionString")));

			services.AddIdentity<AppUser, IdentityRole>()
				.AddEntityFrameworkStores<AppIdentityDbContext>()
				.AddDefaultTokenProviders();

			services.AddCors(setup => {
				setup.AddDefaultPolicy(policy => {
					policy.AllowAnyHeader();
					policy.AllowAnyMethod();
					policy.WithOrigins("https://localhost:4200");
					policy.AllowCredentials();
				});
			});

			services.AddIdentityServer((options) => {
				options.UserInteraction.LoginUrl = "https://localhost:4200/auth";
				//options.UserInteraction.ErrorUrl = "http://localhost:4200/error.html";
				options.UserInteraction.LogoutUrl = "http://localhost:4200/auth";
				options.Events.RaiseErrorEvents = true;
				options.Events.RaiseInformationEvents = true;
				options.Events.RaiseFailureEvents = true;
				options.Events.RaiseSuccessEvents = true;
			})
				.AddDeveloperSigningCredential()
				.AddOperationalStore(options => {
					options.ConfigureDbContext = builder => 
						builder.UseMySql(Configuration.GetConnectionString("Maid_Auth_ConnectionString"), 
						sql => sql.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name));
					options.EnableTokenCleanup = true;
					options.TokenCleanupInterval = 30;
				})
				.AddInMemoryPersistedGrants()
				.AddInMemoryIdentityResources(Config.GetIdentityResources())
				.AddInMemoryApiResources(Config.GetApiResources())
				.AddInMemoryClients(Config.GetClients())
				.AddAspNetIdentity<AppUser>();

			//services.ConfigureApplicationCookie(config =>
			//{
			//	config.Cookie.Name = "IdentityServer.Cookie";
			//	config.LoginPath = "http://localhost:4200/auth";
			//});

			//services.AddJsonFormatters();

			var cors = new DefaultCorsPolicyService(new LoggerFactory().CreateLogger<DefaultCorsPolicyService>()) {
				AllowAll = true
			};

			services.AddSingleton<ICorsPolicyService>(cors);

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory log) {
			log.CreateLogger("Trace");
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthentication();

			app.UseCors();
			app.UseIdentityServer();

			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
			});

			app.UseCookiePolicy(new CookiePolicyOptions {
				HttpOnly = HttpOnlyPolicy.None,
				MinimumSameSitePolicy = SameSiteMode.None,
				Secure = CookieSecurePolicy.Always,
			}); ;
		}
	}
}

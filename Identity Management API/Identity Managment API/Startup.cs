using Identity_Managment_API.DbConnection;
using Identity_Managment_API.IdenittyManagment;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Threading.Tasks;

namespace Identity_Managment_API
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
			// Configure InMemory Provider 
			services.AddDbContext<DatabaseContext>(option => option.UseInMemoryDatabase("InMemory"));

			// Configuration CORS
			services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

			// Configure Authentication 
			services.AddInfrastructure(Configuration);

			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Identity_Managment_API", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity_Managment_API v1"));
			}

			var scope = app.ApplicationServices.CreateScope();
			var context = scope.ServiceProvider.GetService<DatabaseContext>();
			_ = AddTestDataAsync(serviceProvider, context);

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}

		private static async Task AddTestDataAsync(IServiceProvider serviceProvider, DatabaseContext context)
		{
			var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
			var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

			if (!await roleManager.RoleExistsAsync("Admin"))
			{
				await roleManager.CreateAsync(new IdentityRole("Admin"));
			}

			if (!await roleManager.RoleExistsAsync("User"))
			{
				await roleManager.CreateAsync(new IdentityRole("User"));
			}

			var defaultAdmin = new IdentityUser
			{
				UserName = "AdminUser",
				Email = "AdminUser@gmail.com"
			};
			await userManager.CreateAsync(defaultAdmin, "AdminUser123!!!");

			var user = await userManager.FindByNameAsync("AdminUser");

			if (user == null)
			{
				throw new Exception("The AdminUser password was probably not strong enough!");
			}

			await userManager.AddToRoleAsync(user, "Admin");
		}
	}
}

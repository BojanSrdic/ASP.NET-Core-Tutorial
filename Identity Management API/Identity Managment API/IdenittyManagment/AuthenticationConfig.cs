using Identity_Managment_API.DbConnection;
using Identity_Managment_API.Services;
using Identity_Managment_API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity_Managment_API.IdenittyManagment
{
	public static class AuthenticationConfig
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			// Configure Identity
			services.AddIdentity<IdentityUser, IdentityRole>()
				.AddRoles<IdentityRole>()
				//.AddDefaultTokenProviders()
				.AddEntityFrameworkStores<DatabaseContext>();

			// Customize password
			services.Configure<IdentityOptions>(options =>
			{
				options.Password.RequireDigit = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequiredLength = 8;
			});

			//JWT Authentication
			var key = Encoding.UTF8.GetBytes(configuration["AuthSettings:JWT_Secret"].ToString());

			services.AddAuthentication(x => {
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(x => {
				x.RequireHttpsMetadata = false;
				x.SaveToken = false;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false,
					ClockSkew = TimeSpan.Zero
				};
			});

			services.AddScoped<IAuthenticationService, AuthenticationService>();
			services.AddScoped<ICrudService, CrudService>();

			return services;
		}
	}
}

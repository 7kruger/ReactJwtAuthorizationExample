using WebApi.BLL.Interfaces;
using WebApi.BLL.Services;
using WebApi.DAL.Entities;
using WebApi.DAL.Interfaces;
using WebApi.DAL.Repositories;

namespace WebApi;

public static class Initializer
{
	public static void InitializeRepositories(this IServiceCollection services)
	{
		services.AddScoped<IRepository<User>, UserRepository>();
		services.AddScoped<ITokenRepository, TokenRepository>();
	}

	public static void InitializeServices(this IServiceCollection services)
	{
		services.AddScoped<ITokenService, TokenService>();
		services.AddScoped<IAccountService, AccountService>();
	}
}

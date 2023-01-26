using Microsoft.EntityFrameworkCore;
using WebApi.BLL.Interfaces;
using WebApi.BLL.Models;
using WebApi.DAL.Entities;
using WebApi.DAL.Enums;
using WebApi.DAL.Interfaces;

namespace WebApi.BLL.Services;

public class AccountService : IAccountService
{
	private readonly IRepository<User> _userRepository;

	public AccountService(IRepository<User> userRepository)
	{
		_userRepository = userRepository;
	}

	public async Task Register(RegisterModel model)
	{
		var user = new User
		{
			Name = model.Name,
			Password = model.Password,
			Role = Role.User,
		};
		await _userRepository.Create(user);
	}

	public async Task<bool> CheckIfUserExists(UserModel model)
	{
		var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Name);
		return user != null;
	}

	public async Task<bool> IsUserValid(UserModel model)
	{
		var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Name && x.Password == model.Password);
		return user != null;
	}
}

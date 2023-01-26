using WebApi.BLL.Models;

namespace WebApi.BLL.Interfaces;

public interface IAccountService
{
	Task Register(RegisterModel model);
	Task<bool> CheckIfUserExists(UserModel model);
	Task<bool> IsUserValid(UserModel model);
}

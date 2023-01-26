using WebApi.DAL.Enums;

namespace WebApi.BLL.Models;

public class UserModel
{
	public string Name { get; set; }
	public string Password { get; set; }
	public Role Role { get; set; }
}

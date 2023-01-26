using System.ComponentModel.DataAnnotations;

namespace WebApi.BLL.Models;

public class RegisterModel
{
	[Required(ErrorMessage = "Введите имя")]
	public string Name { get; set; }
	[Required(ErrorMessage = "Введите пароль")]
	public string Password { get; set; }
	[Required(ErrorMessage = "Пароли не совпадают")]
	[Compare("Password")]
	public string ConfirmPassword { get; set; }
}

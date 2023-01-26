using System.ComponentModel.DataAnnotations;
using WebApi.DAL.Enums;

namespace WebApi.DAL.Entities;

public class User
{
	[Key]
	public int Id { get; set; }
	[MaxLength(50)]
	public string Name { get; set; }
	[MaxLength(50)]
	public string Password { get; set; }
	public Role Role { get; set; }
	public UserToken UserToken { get; set; }
}

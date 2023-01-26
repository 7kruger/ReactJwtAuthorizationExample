using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.DAL.Entities;
using WebApi.DAL.Interfaces;

namespace WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class HomeController : Controller
{
	private readonly IRepository<User> _userRepository;

	public HomeController(IRepository<User> userRepository)
	{
		_userRepository = userRepository;
	}

	[HttpGet]
	[Route("GetUsers")]
	public async Task<IActionResult> GetUsers()
	{
		var data = new List<string>()
		{
			"1",
			"2",
			"3",
		};
		return Ok(data);
	}
}

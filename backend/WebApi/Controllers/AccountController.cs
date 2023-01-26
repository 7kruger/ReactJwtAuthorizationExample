using Microsoft.AspNetCore.Mvc;
using WebApi.BLL.Interfaces;
using WebApi.BLL.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : Controller
{
	private readonly IAccountService _accountService;
	private readonly ITokenService _tokenService;

	public AccountController(IAccountService accountService, ITokenService tokenService)
	{
		_accountService = accountService;
		_tokenService = tokenService;
	}

	[HttpPost]
	[Route("register")]
	public async Task<IActionResult> Register(RegisterModel model)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		await _accountService.Register(model);

		var token = await _tokenService.GenerateToken(new UserModel
		{
			Name = model.Name,
			Password = model.Password,
		});

		if (token == null)
		{
			return Unauthorized("Invalid attempt");
		}

		return Ok(token);
	}

	[HttpPost]
	[Route("authenticate")]
	public async Task<IActionResult> Authenticate(UserModel model)
	{
		var userValid = await _accountService.IsUserValid(model);

		if (!userValid)
		{
			return Unauthorized("Incorrect username or password!");
		}

		var token = await _tokenService.GenerateToken(model);

		if (token == null)
		{
			return Unauthorized("Invalid attempt");
		}

		return Ok(token);
	}

	[HttpPost]
	[Route("refresh")]
	public async Task<IActionResult> Refresh(TokenModel model)
	{
		var token = await _tokenService.Refresh(model);

		if (token == null)
		{
			return Unauthorized("Invalid attempt");
		}

		return Ok(token);
	}
}

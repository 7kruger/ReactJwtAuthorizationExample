using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApi.BLL.Interfaces;
using WebApi.BLL.Models;
using WebApi.DAL.Entities;
using WebApi.DAL.Interfaces;

namespace WebApi.BLL.Services;

public class TokenService : ITokenService
{
	private readonly IRepository<User> _userRepository;
	private readonly ITokenRepository _tokenRepository;
	private readonly IConfiguration _configuration;

	public TokenService(ITokenRepository tokenRepository,
						IConfiguration configuration,
						IRepository<User> userRepository)
	{
		_tokenRepository = tokenRepository;
		_configuration = configuration;
		_userRepository = userRepository;
	}

	public async Task<TokenModel> GenerateToken(UserModel model)
	{
		return await GenerateAsync(model);
	}

	public async Task<TokenModel> Refresh(TokenModel token)
	{
		var principal = GetPrincipalFromExpiredToken(token);
		var username = principal.Identity?.Name;

		var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == username);
		if (user == null)
		{
			throw new UnauthorizedAccessException("User not found");
		}

		var userToken = user.UserToken;

		if (userToken.RefreshToken != token.RefreshToken)
		{
			throw new SecurityTokenException("Invalid token");
		}

		return await GenerateAsync(new UserModel
		{
			Name = user.Name,
			Password = user.Password,
		});
	}

	private async Task<TokenModel> GenerateAsync(UserModel model)
	{
		try
		{
			var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Name);

			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name, user.Name),
				}),
				Expires = DateTime.Now.AddMinutes(1),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
			};

			var token = new TokenModel
			{
				AccessToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor)),
				RefreshToken = GenerateRefreshToken(),
			};

			var userToken = user.UserToken;
			if (userToken == null)
			{
				await _tokenRepository.Create(new UserToken
				{
					RefreshToken = token.RefreshToken,
					UserId = user.Id,
				});

				return token;
			}

			userToken.RefreshToken = token.RefreshToken;
			await _tokenRepository.Update(userToken);

			return token;
		}
		catch (Exception)
		{
			return null;
		}
	}

	private ClaimsPrincipal GetPrincipalFromExpiredToken(TokenModel token)
	{
		var key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

		var tokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = false,
			ValidateAudience = false,
			ValidateLifetime = false,
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(key),
			ClockSkew = TimeSpan.Zero,
		};

		var tokenHandler = new JwtSecurityTokenHandler();
		var principal = tokenHandler.ValidateToken(token.AccessToken, tokenValidationParameters, out SecurityToken securityToken);
		var jwtSecurityToken = securityToken as JwtSecurityToken;

		if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
		{
			throw new SecurityTokenException("Invalid token");
		}

		return principal;
	}

	private string GenerateRefreshToken()
	{
		var randomNumber = new byte[32];
		using (var num = RandomNumberGenerator.Create())
		{
			num.GetBytes(randomNumber);
			return Convert.ToBase64String(randomNumber);
		}
	}
}

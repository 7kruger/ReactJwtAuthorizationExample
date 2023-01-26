using WebApi.DAL.Entities;
using WebApi.DAL.Interfaces;

namespace WebApi.DAL.Repositories;

public class TokenRepository : ITokenRepository
{
	private readonly ApplicationDbContext _db;

	public TokenRepository(ApplicationDbContext db)
	{
		_db = db;
	}

	public async Task Create(UserToken token)
	{
		await _db.UserTokens.AddAsync(token);
		await _db.SaveChangesAsync();
	}

	public async Task Update(UserToken token)
	{
		_db.Update(token);
		await _db.SaveChangesAsync();
	}
}

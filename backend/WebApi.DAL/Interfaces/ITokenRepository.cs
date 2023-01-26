using WebApi.DAL.Entities;

namespace WebApi.DAL.Interfaces;

public interface ITokenRepository
{
	Task Create(UserToken token);
	Task Update(UserToken token);
}

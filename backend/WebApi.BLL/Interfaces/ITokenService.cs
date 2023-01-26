using WebApi.BLL.Models;

namespace WebApi.BLL.Interfaces;

public interface ITokenService
{
	Task<TokenModel> GenerateToken(UserModel model);
	Task<TokenModel> Refresh(TokenModel token);
}

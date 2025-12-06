using System;
using AuthService.Model.Memcache;
namespace AuthService.Repository.Memcache.Token;

public interface ITokenCacheRepository
{
    /// <summary>
    /// This function create token by passing its whole data structure. 
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task CreateToken(TokenCache token);

    /// <summary>
    /// 
    /// </summary>
    /// <exception cref=""ArgumentNullException></exception>
    /// <param name="tokenId"></param>
    /// <returns></returns>
    Task<TokenCache> GetTokenByIdAsync(Guid tokenId);
    Task<TokenCache?> GetTokenByRefreshIdAsync(Guid refreshId);
    Task<IEnumerable<TokenCache>> GetTokensByUserAsync(Guid userId);
    Task DeleteTokenAsync(Guid tokenId);
    Task<int> DeleteExpiredTokensAsync();
}

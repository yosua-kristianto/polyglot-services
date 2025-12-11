using System;
using AuthService.Model.Memcache;
using AuthService.Repository.Memcache;
using AuthService.Repository.Memcache.Token;
using StackExchange.Redis;

using System.Text.Json;
using AuthService.Config.Memcache;

namespace auth_service.Repository.Memcache.Token;

public class TokenCacheRepository(IRedisConnection ctx) : BaseMemcacheRepository(ctx), ITokenCacheRepository
{
    public async Task CreateToken(TokenCache token)
    {
        string key = $"{TokenCache.TableName}:{token.TokenId}";
        string data = JsonSerializer.Serialize(token);
        await this._redisConnection.StringSetAsync(key, data);
    }

    public async Task<int> DeleteExpiredTokensAsync()
    {
        long limit = DateTimeOffset.UtcNow.AddMonths(-1).ToUnixTimeSeconds();

        var resultSet = this._redisServer.Keys(pattern: $"{TokenCache.TableName}:*");
        int deleted = 0;

        foreach(var key in resultSet)
        {
            string? data = await this._redisConnection.StringGetAsync(key);
            if (data != null)
            {
                TokenCache token = JsonSerializer.Deserialize<TokenCache>(data);
                if (token.CreatedAt < limit)
                {
                    await this._redisConnection.KeyDeleteAsync(key);
                    deleted++;
                }
            }
        }

        return deleted;
    }

    public async Task DeleteTokenAsync(Guid tokenId)
    {
        string key = $"{TokenCache.TableName}:{tokenId}";

        await this._redisConnection.KeyDeleteAsync(key);
    }

    public async Task<TokenCache?> GetTokenByIdAsync(Guid tokenId)
    {
        string key = $"{TokenCache.TableName}:{tokenId}";

        string? data = this._redisConnection.StringGet(key);
        
        if (data != null)
        {
            TokenCache token = JsonSerializer.Deserialize<TokenCache>(data)!;
            return token;
        }

        return null;
    }

    public Task<TokenCache?> GetTokenByRefreshIdAsync(Guid refreshId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TokenCache>> GetTokensByUserAsync(Guid userId)
    {
        var resultSet = this._redisServer.Keys(pattern: $"{TokenCache.TableName}:*");

        List<TokenCache> tokens = new List<TokenCache>();

        foreach(var key in resultSet)
        {
            string? data = this._redisConnection.StringGet(key);
            if (data != null)
            {
                TokenCache token = JsonSerializer.Deserialize<TokenCache>(data)!;
                if (token.UserId == userId)
                {
                    tokens.Add(token);
                }
            }
        }

        return tokens;
    }
}

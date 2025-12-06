using AuthService.Config.Database;

namespace AuthService.Repository.UMA;

public abstract class BaseRepository
{
    protected readonly SystemDbUMAContext _context;
    public BaseRepository(SystemDbUMAContext context) { this._context = context; }
}
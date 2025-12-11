using System;
using AuthService.Config.Database;
using AuthService.Model.Entity;
using AuthService.Repository.UMA;

namespace auth_service.Repository.UMA;

public class UMARepository(SystemDbUMAContext ctx) : BaseRepository(ctx), IUMARepository
{
    public User CreateUser(User user)
    {
        this._context.Users.Add(user);
        this._context.SaveChanges();
        return user;
    }

    public User? FindUserByEmail(string email)
    {
        User? user = this._context.Users.FirstOrDefault(e => e.Email == email);

        return user;
    }

    public User? FindUserById(Guid userId)
    {
        User? user = this._context.Users.FirstOrDefault(e => e.Id == userId);

        return user;
    }

    public void updateUserStatus(Guid userId, short status)
    {
        User user = this.FindUserById(userId)!;

        user.Status = status;
        user.UpdatedAt = DateTime.UtcNow;
        this._context.Users.Update(user);
        this._context.SaveChanges();
    }
}

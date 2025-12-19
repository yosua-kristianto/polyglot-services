using System;
using AuthService.Model.Entity;

namespace AuthService.Repository.UMA;

public interface IUMARepository
{
    /// <summary>
    /// Create a new user in the database.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public User CreateUser(User user);

    /// <summary>
    /// Get a user by their email. May return null if not found.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public User? FindUserByEmail(string email);

    /// <summary>
    /// Get a user by their unique identifier. May return null if not found.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public User? FindUserById(Guid userId);

    /// <summary>
    /// Update user status.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="status"></param>
    public void updateUserStatus(Guid userId, short status);

    
}

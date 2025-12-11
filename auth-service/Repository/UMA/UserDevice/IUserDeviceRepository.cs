using System;

namespace AuthService.Repository.UMA.UserDevice;

public interface IUserDeviceRepository
{
    /// <summary>
    /// Register a new user device.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="deviceId"></param>
    public void RegisterUserDevice(Guid userId, string deviceId);
}

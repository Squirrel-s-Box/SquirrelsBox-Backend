using SquirrelsBox.Session.Domain.Models;
using SquirrelsBox.Session.Domain.Services;

namespace SquirrelsBox.Session.Domain.Repositories
{
    public interface IDeviceSessionRepository
    {
        Task<DeviceSession> GetDeviceSessionByUserIdAsync(int UserId);
    }
}

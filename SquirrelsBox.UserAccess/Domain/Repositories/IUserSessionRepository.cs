using SquirrelsBox.Session.Domain.Models;

namespace SquirrelsBox.Session.Domain.Repositories
{
    public interface IUserSessionRepository
    {
        Task<UserSession> GetUserSessionByUserIdAndOldTokenAsync(int UserId, string OldToken);
        Task<UserSession> GetUserSessionByUserIdAsync(int UserId);
    }
}

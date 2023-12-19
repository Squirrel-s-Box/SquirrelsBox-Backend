using Microsoft.EntityFrameworkCore;
using SquirrelsBox.Generic.Domain.Repositories;
using SquirrelsBox.Generic.Persistence.Repositories;
using SquirrelsBox.Session.Domain.Models;
using SquirrelsBox.Session.Domain.Repositories;
using SquirrelsBox.Session.Persistnce.Context;

namespace SquirrelsBox.Session.Persistnce.Repositories
{
    public class UserSessionRespository : BaseRepository<AppDbContext>, IGenericRepository<UserSession>, IUserSessionRepository
    {
        public UserSessionRespository(AppDbContext context) : base(context)
        {
        }
        public async Task AddAsync(UserSession model)
        {
            await _context.UserSession.AddAsync(model);
        }

        public void Delete(UserSession model)
        {
            _context.UserSession.Remove(model);
        }

        public async Task<UserSession> FindByCodeAsync(string value)
        {
            throw new NotImplementedException();
        }

        public async Task<UserSession> FindByIdAsync(int id)
        {
            return await _context.UserSession.FindAsync(id);
        }

        public async Task<UserSession> GetUserSessionByUserIdAsync(int UserId)
        {
            return await _context.UserSession.FirstOrDefaultAsync(i => i.UserId == UserId);
        }

        public async Task<UserSession> GetUserSessionByUserIdAndOldTokenAsync(int UserId, string OldToken)
        {
            return await _context.UserSession.FirstOrDefaultAsync(i => i.UserId == UserId && i.OldToken == OldToken);
        }

        public void Update(UserSession model)
        {
            _context.UserSession.Update(model);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using SquirrelsBox.Generic.Domain.Repositories;
using SquirrelsBox.Generic.Persistence.Repositories;
using SquirrelsBox.Session.Domain.Models;
using SquirrelsBox.Session.Domain.Repositories;
using SquirrelsBox.Session.Persistnce.Context;

namespace SquirrelsBox.Session.Persistnce.Repositories
{
    public class DeviceSessionRepository : BaseRepository<AppDbContext>, IGenericRepository<DeviceSession>, IDeviceSessionRepository
    {
        public DeviceSessionRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(DeviceSession model)
        {
            await _context.DeviceToken.AddAsync(model);
        }

        public void Delete(DeviceSession model)
        {
            _context.DeviceToken.Remove(model);
        }

        public Task<DeviceSession> FindByCodeAsync(string value)
        {
            throw new NotImplementedException();
        }

        public async Task<DeviceSession> FindByIdAsync(int id)
        {
            return await _context.DeviceToken.FindAsync(id);
        }

        public async Task<DeviceSession> GetDeviceSessionByUserIdAsync(int userId)
        {
            return await _context.DeviceToken.FirstOrDefaultAsync(i => i.UserId == userId);
        }

        public void Update(DeviceSession model)
        {
            _context.DeviceToken.Update(model);
        }
    }
}

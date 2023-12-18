using Microsoft.EntityFrameworkCore;
using SquirrelsBox.Generic.Domain.Repositories;
using SquirrelsBox.Generic.Persistence.Repositories;
using SquirrelsBox.Session.Domain.Models;
using SquirrelsBox.Session.Persistnce.Context;

namespace SquirrelsBox.Session.Persistnce.Repositories
{
    public class AccessSessionRepository : BaseRepository<AppDbContext>, IGenericRepository<AccessSession>
    {
        public AccessSessionRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(AccessSession model)
        {
            await _context.AccessSession.AddAsync(model);
        }

        public void Delete(AccessSession model)
        {
            _context.AccessSession.Remove(model);
        }

        public async Task<AccessSession> FindByCodeAsync(string value)
        {
            return await _context.AccessSession.FirstOrDefaultAsync(i => i.Code == value);
        }

        public async Task<AccessSession> FindByIdAsync(int id)
        {
            return await _context.AccessSession.FindAsync(id);
        }

        public void Update(AccessSession model)
        {
            _context.AccessSession.Update(model);
        }
    }
}

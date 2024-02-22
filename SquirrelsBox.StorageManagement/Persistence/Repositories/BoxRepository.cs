using Microsoft.EntityFrameworkCore;
using SquirrelsBox.Generic.Domain.Repositories;
using SquirrelsBox.Generic.Persistence.Repositories;
using SquirrelsBox.StorageManagement.Domain.Models;
using SquirrelsBox.StorageManagement.Persistence.Context;

namespace SquirrelsBox.StorageManagement.Persistence.Repositories
{
    public class BoxRepository : BaseRepository<AppDbContext>, IGenericRepository<Box>, IGenericReadRepository<Box>
    {
        public BoxRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Box model)
        {
            await _context.Box.AddAsync(model);
        }

        public void Delete(Box model)
        {
            _context.Box.Remove(model);
        }

        public async Task<Box> FindByCodeAsync(string value)
        {
            return await _context.Box.FirstOrDefaultAsync(i => i.UserCodeOwner == value);
        }

        public async Task<Box> FindByIdAsync(int id)
        {
            return await _context.Box.FindAsync(id);
        }

        public Task<IEnumerable<Box>> ListAllByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Box>> ListAllByUserCodeAsync(string userCode)
        {
            return await _context.Box.Where(b => b.UserCodeOwner == userCode).ToListAsync();
        }

        public void Update(Box model)
        {
            _context.Box.Update(model);
        }
    }
}

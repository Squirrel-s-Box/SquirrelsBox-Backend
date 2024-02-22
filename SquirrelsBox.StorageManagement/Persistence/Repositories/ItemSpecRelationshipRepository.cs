using Microsoft.EntityFrameworkCore;
using SquirrelsBox.Generic.Domain.Repositories;
using SquirrelsBox.Generic.Persistence.Repositories;
using SquirrelsBox.StorageManagement.Domain.Models;
using SquirrelsBox.StorageManagement.Domain.Services.Communication;
using SquirrelsBox.StorageManagement.Persistence.Context;

namespace SquirrelsBox.StorageManagement.Persistence.Repositories
{
    public class ItemSpecRelationshipRepository : BaseRepository<AppDbContext>, IGenericRepository<ItemSpecRelationship>, IGenericReadRepository<ItemSpecRelationship>
    {
        public ItemSpecRelationshipRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(ItemSpecRelationship model)
        {
            var SpecCreated = await _context.Spec.AddAsync(model.Spec);
            await _context.SaveChangesAsync();
            model.SpecId = SpecCreated.Entity.Id;
            await _context.ItemSpecRelationship.AddAsync(model);
        }

        public void Delete(ItemSpecRelationship model)
        {
            _context.ItemSpecRelationship.Remove(model);
            _context.Spec.Remove(model.Spec);
        }

        public Task<ItemSpecRelationship> FindByCodeAsync(string value)
        {
            throw new NotImplementedException();
        }

        public async Task<ItemSpecRelationship> FindByIdAsync(int id)
        {
            return await _context.ItemSpecRelationship
           .Include(b => b.Spec)
           .FirstOrDefaultAsync(x => x.SpecId == id);
        }

        public async Task<IEnumerable<ItemSpecRelationship>> ListAllByIdAsync(int id)
        {
            return await _context.ItemSpecRelationship
                .Include(b => b.SpecId)
                .Where(b => b.ItemId == id)
                .ToListAsync();
        }

        public Task<IEnumerable<ItemSpecRelationship>> ListAllByUserCodeAsync(string userCode)
        {
            throw new NotImplementedException();
        }

        public async void Update(ItemSpecRelationship model)
        {
            if (model.ItemId != 0)
            {
                await _context.UpdateItemSpecRelationship(
                    model.ItemId,
                    model.SpecId,
                    // NewValues
                    model.Spec.Id
                );
            }
            else if (model.ItemId == 0)
            {
                _context.Spec.Update(model.Spec);
            }
        }
    }
}

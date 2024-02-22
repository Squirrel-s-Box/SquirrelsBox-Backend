using Microsoft.EntityFrameworkCore;
using SquirrelsBox.Generic.Domain.Repositories;
using SquirrelsBox.Generic.Persistence.Repositories;
using SquirrelsBox.StorageManagement.Domain.Models;
using SquirrelsBox.StorageManagement.Persistence.Context;

namespace SquirrelsBox.StorageManagement.Persistence.Repositories
{
    public class SectionItemRelationshipRepository : BaseRepository<AppDbContext>, IGenericRepository<SectionItemRelationship>, IGenericReadRepository<SectionItemRelationship>
    {
        public SectionItemRelationshipRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(SectionItemRelationship model)
        {
            var ItemCreated = await _context.Item.AddAsync(model.Item);
            await _context.SaveChangesAsync();
            model.ItemId = ItemCreated.Entity.Id;
            await _context.SectionItemRelationship.AddAsync(model);
        }

        public void Delete(SectionItemRelationship model)
        {
            _context.SectionItemRelationship.Remove(model);
            _context.Item.Remove(model.Item);
        }

        public Task<SectionItemRelationship> FindByCodeAsync(string value)
        {
            throw new NotImplementedException();
        }

        public async Task<SectionItemRelationship> FindByIdAsync(int id)
        {
            return await _context.SectionItemRelationship
           .Include(b => b.Item)
           .FirstOrDefaultAsync(x => x.ItemId == id);
        }

        public async Task<IEnumerable<SectionItemRelationship>> ListAllByIdAsync(int id)
        {
            return await _context.SectionItemRelationship
                .Include(b => b.Item)
                .Where(b => b.SectionId == id)
                .ToListAsync();
        }

        public Task<IEnumerable<SectionItemRelationship>> ListAllByUserCodeAsync(string userCode)
        {
            throw new NotImplementedException();
        }

        public async void Update(SectionItemRelationship model)
        {
            if (model.SectionId != 0)
            {
                await _context.UpdateSectionItemRelationship(
                    model.SectionId,
                    model.ItemId,
                    // NewValues
                    model.Item.Id
                );
            }
            else if (model.SectionId == 0)
            {
                _context.Item.Update(model.Item);
            }
        }
    }
}

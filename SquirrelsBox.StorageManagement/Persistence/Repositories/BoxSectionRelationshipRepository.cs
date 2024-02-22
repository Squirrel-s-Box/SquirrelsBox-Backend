    using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SquirrelsBox.Generic.Domain.Repositories;
using SquirrelsBox.Generic.Persistence.Repositories;
using SquirrelsBox.StorageManagement.Domain.Models;
using SquirrelsBox.StorageManagement.Persistence.Context;

namespace SquirrelsBox.StorageManagement.Persistence.Repositories
{
    public class BoxSectionRelationshipRepository : BaseRepository<AppDbContext>, IGenericRepository<BoxSectionRelationship>, IGenericReadRepository<BoxSectionRelationship>
    {
        public BoxSectionRelationshipRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(BoxSectionRelationship model)
        {
            var SectionCreated = await _context.Section.AddAsync(model.Section);
            await _context.SaveChangesAsync();
            model.SectionId = SectionCreated.Entity.Id;
            await _context.BoxSectionRelationship.AddAsync(model);
        }

        public void Delete(BoxSectionRelationship model)
        {
            _context.BoxSectionRelationship.Remove(model);
            _context.Section.Remove(model.Section);
        }

        public Task<BoxSectionRelationship> FindByCodeAsync(string value)
        {
            throw new NotImplementedException();
        }

        public async Task<BoxSectionRelationship> FindByIdAsync(int id)
        {
            return await _context.BoxSectionRelationship
            .Include(b => b.Section)
            .FirstOrDefaultAsync(x => x.SectionId == id);

        }

        public async Task<IEnumerable<BoxSectionRelationship>> ListAllByIdAsync(int id)
        {
            return await _context.BoxSectionRelationship
                .Include(b => b.Section)
                .Where(b => b.BoxId == id)
                .ToListAsync();
        }

        public Task<IEnumerable<BoxSectionRelationship>> ListAllByUserCodeAsync(string userCode)
        {
            throw new NotImplementedException();
        }

        public async void Update(BoxSectionRelationship model)
        {
            if (model.BoxId != 0)
            {
                await _context.UpdateBoxSectionRelationship(
                    model.BoxId,
                    model.SectionId,
                    // NewValues
                    model.Section.Id
                );
            }
            else if (model.SectionId == 0)
            {
                _context.Section.Update(model.Section);
            }
            }

    }
}

using Microsoft.EntityFrameworkCore;
using SquirrelsBox.Generic.Domain.Repositories;
using SquirrelsBox.Generic.Persistence.Repositories;
using SquirrelsBox.Permissions.Domain.Models;
using SquirrelsBox.Permissions.Persistence.Context;

namespace SquirrelsBox.Permissions.Persistence.Repositories
{
    public class AssignedPermissionRepository : BaseRepository<AppDbContext>, IGenericReadRepository<AssignedPermission>, IGenericRepository<AssignedPermission>
    {
        public AssignedPermissionRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(AssignedPermission model)
        {
            await _context.AssignedPermissions.AddAsync(model);
        }

        public void Delete(AssignedPermission model)
        {
            _context.AssignedPermissions.Remove(model);
        }

        public Task<AssignedPermission> FindByCodeAsync(string value)
        {
            throw new NotImplementedException();
        }

        public Task<AssignedPermission> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AssignedPermission>> ListAllByUserCodeAsync(string userCode)
        {
            return await _context.AssignedPermissions
                        .Include(ap => ap.Permission) // Include the Permission entity
                        .Where(ap => ap.UserCode == userCode)
                        .ToListAsync();
        }

        public void Update(AssignedPermission model)
        {
            throw new NotImplementedException();
        }
    }
}

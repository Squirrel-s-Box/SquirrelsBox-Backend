using SquirrelsBox.Generic.Domain.Repositories;
using SquirrelsBox.Generic.Persistence.Repositories;
using SquirrelsBox.Session.Domain.Models;
using SquirrelsBox.Session.Persistnce.Context;

namespace SquirrelsBox.Session.Persistnce.Repositories
{
    public class UserSessionRespository : BaseRepository<AppDbContext>, IGenericRepository<UserSession>
    {
        public UserSessionRespository(AppDbContext context) : base(context)
        {
        }
        public async Task AddAsync(UserSession model)
        {
            await _context.SessionToken.AddAsync(model);
        }

        public void Delete(UserSession model)
        {
            _context.SessionToken.Remove(model);
        }

        public async Task<UserSession> FindByCodeAsync(string value)
        {
            throw new NotImplementedException();
        }

        public async Task<UserSession> FindByIdAsync(int id)
        {
            return await _context.SessionToken.FindAsync(id);
        }

        public void Update(UserSession model)
        {
            _context.SessionToken.Update(model);
        }
    }
}

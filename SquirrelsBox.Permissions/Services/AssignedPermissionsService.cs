using Azure;
using SquirrelsBox.Generic.Domain.Repositories;
using SquirrelsBox.Generic.Domain.Services;
using SquirrelsBox.Permissions.Domain.Models;
using SquirrelsBox.Permissions.Domain.Services.Communication;
using SquirrelsBox.Permissions.Persistence.Context;

namespace SquirrelsBox.Permissions.Services
{
    public class AssignedPermissionsService : IGenericReadService<AssignedPermission, AssignedPermissionResponse>, IGenericService<AssignedPermission, AssignedPermissionResponse>
    {
        private readonly IGenericReadRepository<AssignedPermission> _repository;
        private readonly IGenericRepository<AssignedPermission> _genericRepository;
        private readonly IUnitOfWork<AppDbContext> _unitOfWork;

        public AssignedPermissionsService(IGenericReadRepository<AssignedPermission> repository, IGenericRepository<AssignedPermission> genericRepository, IUnitOfWork<AppDbContext> unitOfWork)
        {
            _repository = repository;
            _genericRepository = genericRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<AssignedPermissionResponse> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AssignedPermissionResponse> FindByCodeAsync(string value)
        {
            throw new NotImplementedException();
        }

        public Task<AssignedPermissionResponse> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AssignedPermissionResponse>> ListAllByUserCodeAsync(string userCode)
        {
            var results = await _repository.ListAllByUserCodeAsync(userCode);
            var response = results.Select(result => new AssignedPermissionResponse(result));
            return response;
        }

        public async Task<AssignedPermissionResponse> SaveAsync(AssignedPermission model)
        {
            try
            {
                model.CreationDate = DateTime.UtcNow;
                model.LastUpdateDate = null;

                await _genericRepository.AddAsync(model);
                await _unitOfWork.CompleteAsync();

                return new AssignedPermissionResponse(model);
            }
            catch (Exception e)
            {
                return new AssignedPermissionResponse($"An error ocurred while saving the AccessSession: {e.Message}");
            }
        }

        public Task<AssignedPermissionResponse> UpdateAsync(int id, AssignedPermission model)
        {
            throw new NotImplementedException();
        }
    }
}

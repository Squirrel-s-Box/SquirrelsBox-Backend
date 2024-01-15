using SquirrelsBox.Generic.Domain.Repositories;
using SquirrelsBox.Generic.Domain.Services;
using SquirrelsBox.Generic.Persistence.Repositories;
using SquirrelsBox.StorageManagement.Domain.Models;
using SquirrelsBox.StorageManagement.Domain.Services.Communication;
using SquirrelsBox.StorageManagement.Persistence.Context;

namespace SquirrelsBox.StorageManagement.Services
{
    public class BoxService : IGenericService<Box, BoxResponse>, IGenericReadService<Box, BoxResponse>
    {
        private readonly IGenericRepository<Box> _repository;
        private readonly IGenericReadRepository<Box> _readRepository;
        private readonly IUnitOfWork<AppDbContext> _unitOfWork;

        public BoxService(IGenericRepository<Box> repository, IGenericReadRepository<Box> readRepository, IUnitOfWork<AppDbContext> unitOfWork)
        {
            _repository = repository;
            _readRepository = readRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BoxResponse> DeleteAsync(int id)
        {
            var result = await _repository.FindByIdAsync(id);
            if (result == null)
                return new BoxResponse("Box not found");

            try
            {
                _repository.Delete(result);
                await _unitOfWork.CompleteAsync();

                return new BoxResponse(result);
            }
            catch (Exception e)
            {
                return new BoxResponse($"An error occurred while deleting the Box: {e.Message}");
            }
        }

        public async Task<BoxResponse> FindByCodeAsync(string value)
        {
            var result = await _repository.FindByCodeAsync(value);

            if (result == null)
                return new BoxResponse("Box not found");

            try
            {
                await _unitOfWork.CompleteAsync();

                return new BoxResponse(result);
            }
            catch (Exception e)
            {
                return new BoxResponse($"Box not found: {e.Message}");
            }
        }

        public async Task<BoxResponse> FindByIdAsync(int id)
        {
            try
            {
                var result = await _repository.FindByIdAsync(id);
                await _unitOfWork.CompleteAsync();

                return new BoxResponse(result);
            }
            catch (Exception e)
            {
                return new BoxResponse($"Box not found: {e.Message}");
            }
        }

        public Task<IEnumerable<BoxResponse>> ListAllByIdCodeAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BoxResponse>> ListAllByUserCodeAsync(string userCode)
        {
            var results = await _readRepository.ListAllByUserCodeAsync(userCode);
            var response = results.Select(result => new BoxResponse(result));
            return response;
        }

        public async Task<BoxResponse> SaveAsync(Box model)
        {
            try
            {
                model.Favourite = false;
                model.State = true;

                model.CreationDate = DateTime.UtcNow;
                model.LastUpdateDate = null;

                await _repository.AddAsync(model);
                await _unitOfWork.CompleteAsync();

                return new BoxResponse(model);
            }
            catch (Exception e)
            {
                return new BoxResponse($"An error ocurred while saving the Box: {e.Message}");
            }
        }

        public async Task<BoxResponse> UpdateAsync(int id, Box model)
        {
            var result = await _repository.FindByIdAsync(id);
            if (result == null)
                return new BoxResponse("Box not found");

            try
            {
                result.Name = model.Name;
                result.Favourite = model.Favourite;
                result.State = model.State;
                result.LastUpdateDate = DateTime.UtcNow;

                _repository.Update(result);
                await _unitOfWork.CompleteAsync();

                return new BoxResponse(result);
            }
            catch (Exception e)
            {
                return new BoxResponse($"An error occurred while updating the Box: {e.Message}");
            }
        }
    }
}

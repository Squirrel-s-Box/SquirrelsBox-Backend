using SquirrelsBox.Generic.Domain.Repositories;
using SquirrelsBox.Generic.Domain.Services;
using SquirrelsBox.Session.Domain.Models;
using SquirrelsBox.Session.Domain.Services.Communication;
using SquirrelsBox.Session.Persistnce.Context;

namespace SquirrelsBox.Session.Services
{
    public class UserSessionService : IGenericService<UserSession, UserSessionResponse>
    {
        private readonly IGenericRepository<UserSession> _repository;
        private readonly IUnitOfWork<AppDbContext> _unitOfWork;

        public UserSessionService(IGenericRepository<UserSession> repository, IUnitOfWork<AppDbContext> unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<UserSessionResponse> DeleteAsync(int id)
        {
            var result = await _repository.FindByIdAsync(id);
            if (result == null)
                return new UserSessionResponse("AccessSession not found");

            try
            {
                _repository.Delete(result);
                await _unitOfWork.CompleteAsync();

                return new UserSessionResponse(result);
            }
            catch (Exception e)
            {
                return new UserSessionResponse($"An error occurred while deleting the AccessSession AccessSession: {e.Message}");
            }
        }

        public Task<UserSessionResponse> FindByCodeAsync(string value)
        {
            throw new NotImplementedException();
        }

        public async Task<UserSessionResponse> FindByIdAsync(int id)
        {
            try
            {
                var result = await _repository.FindByIdAsync(id);
                await _unitOfWork.CompleteAsync();

                return new UserSessionResponse(result);
            }
            catch (Exception e)
            {
                return new UserSessionResponse($"AccessSession AccessSession not found: {e.Message}");
            }
        }

        public async Task<UserSessionResponse> SaveAsync(UserSession model)
        {
            try
            {
                model.OldToken = null;
                model.CreationDate = DateTime.UtcNow;
                model.LastUpdateDate = null;

                await _repository.AddAsync(model);
                await _unitOfWork.CompleteAsync();

                return new UserSessionResponse(model);
            }
            catch (Exception e)
            {
                return new UserSessionResponse($"An error ocurred while saving the AccessSession AccessSession: {e.Message}");
            }
        }

        public async Task<UserSessionResponse> UpdateAsync(int id, UserSession model)
        {
            var result = await _repository.FindByIdAsync(id);
            if (result == null)
                return new UserSessionResponse("AccessSession AccessSession Token not found");

            try
            {
                result.Token = model.Token;
                result.LastUpdateDate = DateTime.UtcNow;

                _repository.Update(result);
                await _unitOfWork.CompleteAsync();

                return new UserSessionResponse(result);
            }
            catch (Exception e)
            {
                return new UserSessionResponse($"An error occurred while updating the AccessSession AccessSession: {e.Message}");
            }
        }
    }
}

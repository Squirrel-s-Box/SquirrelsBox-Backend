using SquirrelsBox.Generic.Domain.Repositories;
using SquirrelsBox.Generic.Domain.Services;
using SquirrelsBox.Session.Domain.Models;
using SquirrelsBox.Session.Domain.Services.Communication;
using SquirrelsBox.Session.Persistnce.Context;

namespace SquirrelsBox.Session.Services
{
    public class AccessSessionService : IGenericService<AccessSession, AccessSessionResponse>
    {
        private readonly IGenericRepository<AccessSession> _repository;
        private readonly IUnitOfWork<AppDbContext> _unitOfWork;

        public AccessSessionService(IGenericRepository<AccessSession> repository, IUnitOfWork<AppDbContext> unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<AccessSessionResponse> DeleteAsync(int id)
        {
            var result = await _repository.FindByIdAsync(id);
            if (result == null)
                return new AccessSessionResponse("Session not found");

            try
            {
                _repository.Delete(result);
                await _unitOfWork.CompleteAsync();

                return new AccessSessionResponse(result);
            }
            catch (Exception e)
            {
                return new AccessSessionResponse($"An error occurred while deleting the Session: {e.Message}");
            }
        }

        public async Task<AccessSessionResponse> FindByCodeAsync(string value)
        {
            var result = await _repository.FindByCodeAsync(value);

            if (result == null)
                return new AccessSessionResponse("Session not found");

            try
            {
                await _unitOfWork.CompleteAsync();

                return new AccessSessionResponse(result);
            }
            catch (Exception e)
            {
                return new AccessSessionResponse($"Session not found: {e.Message}");
            }
        }

        public async Task<AccessSessionResponse> FindByIdAsync(int id)
        {
            try
            {
                var result = await _repository.FindByIdAsync(id);
                await _unitOfWork.CompleteAsync();

                return new AccessSessionResponse(result);
            }
            catch (Exception e)
            {
                return new AccessSessionResponse($"Session not found: {e.Message}");
            }
        }

        public async Task<AccessSessionResponse> SaveAsync(AccessSession model)
        {
            try
            {
                AccessSession verification;

                model.CreationDate = DateTime.UtcNow;
                model.LastUpdateDate = null;

                do
                {
                    model.Code = Guid.NewGuid().ToString();
                    verification = await _repository.FindByCodeAsync(model.Code);
                } while (verification != null);

                await _repository.AddAsync(model);
                await _unitOfWork.CompleteAsync();

                return new AccessSessionResponse(model);
            }
            catch (Exception e)
            {
                return new AccessSessionResponse($"An error ocurred while saving the userData: {e.Message}");
            }
        }

        public async Task<AccessSessionResponse> UpdateAsync(int id, AccessSession model)
        {
            var result = await _repository.FindByIdAsync(id);
            if (result == null)
                return new AccessSessionResponse("Session not found");

            try
            {
                if (result.Attempt == 3 &&
                    (DateTime.UtcNow - result.LastUpdateDate) >= TimeSpan.FromMinutes(30))
                {
                    result.Attempt = 0;
                    result.LastUpdateDate = DateTime.UtcNow;
                }
                else if (result.Attempt < 3)
                {
                    result.Attempt += 1;
                    result.LastUpdateDate = DateTime.UtcNow;
                }
                else
                {
                    return new AccessSessionResponse("You have tried too much, wait 30 mins and try again else check your password");
                }

                _repository.Update(result);
                await _unitOfWork.CompleteAsync();

                return new AccessSessionResponse(result);
            }
            catch (Exception e)
            {
                return new AccessSessionResponse($"An error occurred while updating the user data: {e.Message}");
            }
        }
    }
}

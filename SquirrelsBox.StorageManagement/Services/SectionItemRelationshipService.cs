﻿using SquirrelsBox.Generic.Domain.Repositories;
using SquirrelsBox.Generic.Domain.Services;
using SquirrelsBox.StorageManagement.Domain.Models;
using SquirrelsBox.StorageManagement.Domain.Services.Communication;
using SquirrelsBox.StorageManagement.Persistence.Context;
using System.Linq;

namespace SquirrelsBox.StorageManagement.Services
{
    public class SectionItemRelationshipService : IGenericService<SectionItemRelationship, SectionItemRelationshipResponse>, IGenericReadService<SectionItemRelationship, SectionItemRelationshipResponse>
    {
        private readonly IGenericRepository<SectionItemRelationship> _repository;
        private readonly IGenericReadRepository<SectionItemRelationship> _readRepository;
        private readonly IUnitOfWork<AppDbContext> _unitOfWork;

        public SectionItemRelationshipService(IGenericRepository<SectionItemRelationship> repository, IGenericReadRepository<SectionItemRelationship> readRepository, IUnitOfWork<AppDbContext> unitOfWork)
        {
            _repository = repository;
            _readRepository = readRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SectionItemRelationshipResponse> DeleteAsync(int id)
        {
            var result = await _repository.FindByIdAsync(id);
            if (result == null)
                return new SectionItemRelationshipResponse("Item not found");

            try
            {
                _repository.Delete(result);
                await _unitOfWork.CompleteAsync();

                return new SectionItemRelationshipResponse(result);
            }
            catch (Exception e)
            {
                return new SectionItemRelationshipResponse($"An error occurred while deleting the Item: {e.Message}");
            }
        }

        public Task<SectionItemRelationshipResponse> FindByCodeAsync(string value)
        {
            throw new NotImplementedException();
        }

        public async Task<SectionItemRelationshipResponse> FindByIdAsync(int id)
        {
            try
            {
                var result = await _repository.FindByIdAsync(id);
                await _unitOfWork.CompleteAsync();

                return new SectionItemRelationshipResponse(result);
            }
            catch (Exception e)
            {
                return new SectionItemRelationshipResponse($"Item not found: {e.Message}");
            }
        }

        public async Task<IEnumerable<SectionItemRelationshipResponse>> ListAllByIdCodeAsync(int id)
        {
            var results = await _readRepository.ListAllByIdAsync(id);
            var response = results.Select(result => new SectionItemRelationshipResponse(result));
            return response;
        }

        public Task<IEnumerable<SectionItemRelationshipResponse>> ListAllByUserCodeAsync(string userCode)
        {
            throw new NotImplementedException();
        }

        public async Task<SectionItemRelationshipResponse> SaveAsync(SectionItemRelationship model)
        {
            try
            {
                model.Item.State = true;

                model.Item.CreationDate = DateTime.UtcNow;
                model.Item.LastUpdateDate = null;

                await _repository.AddAsync(model);
                await _unitOfWork.CompleteAsync();

                return new SectionItemRelationshipResponse(model);
            }
            catch (Exception e)
            {
                return new SectionItemRelationshipResponse($"An error ocurred while saving the Item: {e.Message}");
            }
        }

        public async Task<SectionItemRelationshipResponse> UpdateAsync(int id, SectionItemRelationship model)
        {
            var result = await _repository.FindByIdAsync(id);
            if (result == null)
                return new SectionItemRelationshipResponse("Item not found");

            try
            {
                if (model.SectionId != 0)
                {
                    //It works as th enew Box Id
                    result.Item.Id = model.SectionId;
                    _repository.Update(result);

                    return new SectionItemRelationshipResponse(result);
                }
                else
                {
                    var lastSectionId = result.SectionId;
                    var lastItemId = result.ItemId;

                    result.SectionId = 0;
                    result.ItemId = 0;
                    result.Item.Name = model.Item.Name;
                    result.Item.Description = model.Item.Description;
                    result.Item.Amount = model.Item.Amount;
                    result.Item.ItemPhoto = model.Item.ItemPhoto;
                    result.Item.State = model.Item.State;
                    result.Item.LastUpdateDate = DateTime.UtcNow;

                    _repository.Update(result);

                    result.SectionId = lastSectionId;
                    result.ItemId = lastItemId;

                    await _unitOfWork.CompleteAsync();

                    return new SectionItemRelationshipResponse(result);
                }
            }
            catch (Exception e)
            {
                return new SectionItemRelationshipResponse($"An error occurred while updating the Item  : {e.Message}");
            }
        }
    }
}

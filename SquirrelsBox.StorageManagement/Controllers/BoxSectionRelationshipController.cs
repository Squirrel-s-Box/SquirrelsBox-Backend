﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SquirrelsBox.Generic.Domain.Services;
using SquirrelsBox.Generic.Domain.Services.Communication;
using SquirrelsBox.Generic.Extensions;
using SquirrelsBox.Generic.Resources;
using SquirrelsBox.StorageManagement.Domain.Models;
using SquirrelsBox.StorageManagement.Domain.Services.Communication;
using SquirrelsBox.StorageManagement.Resources;

namespace SquirrelsBox.StorageManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoxSectionRelationshipController : ControllerBase
    {
        private readonly IGenericService<BoxSectionRelationship, BoxSectionRelationshipResponse> _service;
        private readonly IGenericReadService<BoxSectionRelationship, BoxSectionRelationshipResponse> _readService;
        private readonly IMapper _mapper;

        public BoxSectionRelationshipController(IGenericService<BoxSectionRelationship, BoxSectionRelationshipResponse> service, IGenericReadService<BoxSectionRelationship, BoxSectionRelationshipResponse> readService, IMapper mapper)
        {
            _service = service;
            _readService = readService;
            _mapper = mapper;
        }

        [HttpGet("sectionlist/{id}")]
        public async Task<IActionResult> GetAllByIdCodeAsync(int id)
        {
            var model = await _readService.ListAllByIdCodeAsync(id);
            var list = model.Select(response => _mapper.Map<BoxSectionRelationship, ReadBoxSectionRelationshipResource>(response.Resource));
            return Ok(new { SectionList = list });
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveBoxSectionsListResource data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ErrorMessagesExtensions.GetErrorMessages(ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList())));

            var model = _mapper.Map<SaveBoxSectionsListResource, BoxSectionRelationship>(data);

            var result = await _service.SaveAsync(model);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(_mapper.Map<BaseResponse<BoxSectionRelationship>, ValidationResource>(result));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromBody] UpdateBoxSectionsListResource data, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ErrorMessagesExtensions.GetErrorMessages(ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList())));

            var model = _mapper.Map<UpdateBoxSectionsListResource, BoxSectionRelationship>(data);
            model.Section.Id= id;
            var result = await _service.UpdateAsync(id, model);

            if (!result.Success)
                return BadRequest(result.Message);

            var itemResource = _mapper.Map<BaseResponse<BoxSectionRelationship>, ValidationResource>(result);
            return Ok(itemResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ErrorMessagesExtensions.GetErrorMessages(ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList())));

            var result = await _service.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var itemResource = _mapper.Map<BaseResponse<BoxSectionRelationship>, ValidationResource>(result);
            return Ok(itemResource);
        }
    }
}

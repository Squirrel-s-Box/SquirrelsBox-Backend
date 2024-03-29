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
    public class SectionItemRelationshipController : ControllerBase
    {
        private readonly IGenericService<SectionItemRelationship, SectionItemRelationshipResponse> _service;
        private readonly IGenericReadService<SectionItemRelationship, SectionItemRelationshipResponse> _readService;
        private readonly IMapper _mapper;

        public SectionItemRelationshipController(IGenericService<SectionItemRelationship, SectionItemRelationshipResponse> service, IGenericReadService<SectionItemRelationship, SectionItemRelationshipResponse> readService, IMapper mapper)
        {
            _service = service;
            _readService = readService;
            _mapper = mapper;
        }

        [HttpGet("sectionlist/{id}")]
        public async Task<IActionResult> GetAllByIdCodeAsync(int id)
        {
            var model = await _readService.ListAllByIdCodeAsync(id);
            var list = model.Select(response => _mapper.Map<SectionItemRelationship, ReadSectionItemRelationshipResource>(response.Resource));
            return Ok(new { SectionList = list });
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveSectionItemResource data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ErrorMessagesExtensions.GetErrorMessages(ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList())));

            var model = _mapper.Map<SaveSectionItemResource, SectionItemRelationship>(data);

            var result = await _service.SaveAsync(model);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(_mapper.Map<BaseResponse<SectionItemRelationship>, ValidationResource>(result));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromBody] UpdateSectionItemListResource data, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ErrorMessagesExtensions.GetErrorMessages(ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList())));

            var model = _mapper.Map<UpdateSectionItemListResource, SectionItemRelationship>(data);
            model.Item.Id = id;
            var result = await _service.UpdateAsync(id, model);

            if (!result.Success)
                return BadRequest(result.Message);

            var itemResource = _mapper.Map<BaseResponse<SectionItemRelationship>, ValidationResource>(result);
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

            var itemResource = _mapper.Map<BaseResponse<SectionItemRelationship>, ValidationResource>(result);
            return Ok(itemResource);
        }
    }
}

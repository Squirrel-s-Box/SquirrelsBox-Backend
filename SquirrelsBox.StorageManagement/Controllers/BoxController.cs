using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SquirrelsBox.Generic.Domain.Services;
using SquirrelsBox.Generic.Domain.Services.Communication;
using SquirrelsBox.Generic.Extensions;
using SquirrelsBox.Generic.Resources;
using SquirrelsBox.StorageManagement.Domain.Models;
using SquirrelsBox.StorageManagement.Domain.Services.Communication;
using SquirrelsBox.StorageManagement.Resources;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SquirrelsBox.StorageManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoxController : ControllerBase
    {
        private readonly IGenericService<Box, BoxResponse> _service;
        private readonly IGenericReadService<Box, BoxResponse> _readService;
        private readonly IMapper _mapper;

        public BoxController(IGenericService<Box, BoxResponse> service, IGenericReadService<Box, BoxResponse> readService, IMapper mapper)
        {
            _service = service;
            _readService = readService;
            _mapper = mapper;
        }

        [HttpGet("boxlist/{userCode}")]
        public async Task<IActionResult> GetAllByUserCodeAsync(string userCode)
        {

            var model = await _readService.ListAllByUserCodeAsync(userCode);
            var list = model.Select(response => new { box = _mapper.Map<Box, ReadBoxResource>(response.Resource) });
            return Ok(new { BoxList = list });

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ErrorMessagesExtensions.GetErrorMessages(ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList())));

            var result = await _service.FindByIdAsync(id);

            return Ok(result);
        }

        [HttpPost("{userCode}")]
        public async Task<IActionResult> PostAsync([FromBody] SaveBoxResource data, string userCode)
        {
            if (!ModelState.IsValid)
                return BadRequest(ErrorMessagesExtensions.GetErrorMessages(ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList())));

            var model = _mapper.Map<SaveBoxResource, Box>(data);
            model.UserCodeOwner= userCode;

            var result = await _service.SaveAsync(model);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(_mapper.Map<BaseResponse<Box>, ValidationResource>(result));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromBody] UpdateBoxResource data, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ErrorMessagesExtensions.GetErrorMessages(ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList())));

            var model = _mapper.Map<UpdateBoxResource, Box>(data);
            var result = await _service.UpdateAsync(id, model);

            if (!result.Success)
                return BadRequest(result.Message);

            var itemResource = _mapper.Map<BaseResponse<Box>, ValidationResource>(result);
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

            var itemResource = _mapper.Map<BaseResponse<Box>, ValidationResource>(result);
            return Ok(itemResource);
        }
    }
}

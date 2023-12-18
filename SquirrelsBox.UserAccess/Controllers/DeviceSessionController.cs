﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SquirrelsBox.Generic.Domain.Services;
using SquirrelsBox.Generic.Domain.Services.Communication;
using SquirrelsBox.Generic.Extensions;
using SquirrelsBox.Generic.Resources;
using SquirrelsBox.Session.Domain.Models;
using SquirrelsBox.Session.Domain.Services.Communication;
using SquirrelsBox.Session.Resources;

namespace SquirrelsBox.Session.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceSessionController : ControllerBase
    {
        private readonly IGenericService<DeviceSession, DeviceSessionResponse> _service;
        private readonly IGenericService<Domain.Models.AccessSession, AccessSessionResponse> _userService;
        private readonly IMapper _mapper;

        public DeviceSessionController(IGenericService<DeviceSession, DeviceSessionResponse> service, IGenericService<Domain.Models.AccessSession, AccessSessionResponse> userService, IMapper mapper)
        {
            _service = service;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("{userCode}")]
        public async Task<IActionResult> PostAsync([FromBody] SaveTokenResource data, string userCode)
        {
            if (!ModelState.IsValid)
                return BadRequest(ErrorMessagesExtensions.GetErrorMessages(ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList())));

            var response = await _userService.FindByCodeAsync(userCode);
            if (!response.Success)
                return BadRequest(response.Message);

            var model = _mapper.Map<SaveTokenResource, SaveFoundTokenByUserIdResource>(data);
            model.UserId = response.Resource.Id;

            var result = await _service.SaveAsync(_mapper.Map<SaveFoundTokenByUserIdResource, DeviceSession>(model));
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(_mapper.Map<BaseResponse<DeviceSession>, ValidationResource>(result));
        }

        [HttpPut("{userCode}")]
        public async Task<IActionResult> PutAsync([FromBody] SaveTokenResource data, string userCode)
        {
            if (!ModelState.IsValid)
                return BadRequest(ErrorMessagesExtensions.GetErrorMessages(ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList())));

            var findUser = await _userService.FindByCodeAsync(userCode);

            if (findUser.Resource != null)
            {
                var model = _mapper.Map<SaveTokenResource, DeviceSession>(data);
                var result = await _service.UpdateAsync(findUser.Resource.Id, model);

                if (!result.Success)
                    return BadRequest(result.Message);

                var itemResource = _mapper.Map<BaseResponse<DeviceSession>, ValidationResource>(result);
                return Ok(itemResource);
            }
            else
            {
                return BadRequest(findUser.Message);
            }
        }

        [HttpDelete("{userCode}")]
        public async Task<IActionResult> DeleteAsync(string userCode)
        {
            if (!ModelState.IsValid)
                return BadRequest(ErrorMessagesExtensions.GetErrorMessages(ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList())));

            var findUser = await _userService.FindByCodeAsync(userCode);

            if (findUser.Resource != null)
            {
                var result = await _service.DeleteAsync(findUser.Resource.Id);

                if (!result.Success)
                    return BadRequest(result.Message);

                var itemResource = _mapper.Map<BaseResponse<DeviceSession>, ValidationResource>(result);
                return Ok(itemResource);
            }
            else
            {
                return BadRequest(findUser.Message);
            }
        }

    }
}

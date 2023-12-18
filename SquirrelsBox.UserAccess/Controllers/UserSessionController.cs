using AutoMapper;
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
    public class UserSessionController : ControllerBase
    {
        private readonly IGenericService<UserSession, UserSessionResponse> _service;
        private readonly IGenericService<AccessSession, AccessSessionResponse> _userService;
        private readonly IMapper _mapper;

        public UserSessionController(IGenericService<UserSession, UserSessionResponse> service, IGenericService<AccessSession, AccessSessionResponse> userService, IMapper mapper)
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

            var result = await _service.SaveAsync(_mapper.Map<SaveFoundTokenByUserIdResource, UserSession>(model));
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(_mapper.Map<BaseResponse<UserSession>, ValidationResource>(result));
        }


    }
}

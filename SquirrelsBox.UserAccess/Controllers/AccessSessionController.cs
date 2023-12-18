using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
    public class AccessSessionController : ControllerBase
    {
        private readonly IGenericService<Domain.Models.AccessSession, AccessSessionResponse> _service;
        private readonly IMapper _mapper;

        public AccessSessionController(IGenericService<Domain.Models.AccessSession, AccessSessionResponse> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync()
        {
            if (!ModelState.IsValid)
            {
                var errorState = ModelState.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
                );

                var errorMessages = ErrorMessagesExtensions.GetErrorMessages(errorState);

                return BadRequest(errorMessages);
            }

            Domain.Models.AccessSession model = new Domain.Models.AccessSession();
            var result = await _service.SaveAsync(model);

            if (!result.Success)
                return BadRequest(result.Message);

            var itemResource = _mapper.Map<BaseResponse<AccessSession>, ValidationResource>(result);

            var res = new { itemResource, code = result.Resource.Code };
            return Ok(res);
        }

        [HttpPut("{code}")]
        public async Task<IActionResult> PutAsync(string code)
        {
            if (!ModelState.IsValid)
            {
                var errorState = ModelState.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
                );

                var errorMessages = ErrorMessagesExtensions.GetErrorMessages(errorState);

                return BadRequest(errorMessages);
            }

            var findUser = await _service.FindByCodeAsync(code);

            if (findUser.Resource != null)
            {
                Domain.Models.AccessSession model = new Domain.Models.AccessSession();
                var result = await _service.UpdateAsync(findUser.Resource.Id, model);

                if (!result.Success)
                    return BadRequest(result.Message);

                var itemResource = _mapper.Map<BaseResponse<AccessSession>, ValidationResource>(result);
                return Ok(itemResource);
            }
            else
            {
                return BadRequest(findUser.Message);
            }
        }

        [HttpDelete("{code}")]
        public async Task<IActionResult> DeleteAsync(string code)
        {
            var findUser = await _service.FindByCodeAsync(code);

            if (findUser.Resource != null)
            {
                var result = await _service.DeleteAsync(findUser.Resource.Id);

                if (!result.Success)
                    return BadRequest(result.Message);

                var itemResource = _mapper.Map<BaseResponse<AccessSession>, ValidationResource>(result);
                return Ok(itemResource);
            }
            else
            {
                return BadRequest(findUser.Message);
            }
        }
    }
}

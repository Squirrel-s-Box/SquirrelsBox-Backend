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
using System.Text.RegularExpressions;
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

        [HttpPost()]    
        public async Task<IActionResult> ReadE()
        {
            // Call the function to count letters passing the file path
            var count = await CountWordsWithThreeEs("D:\\Squirrels_box\\SquirrelsBox\\SquirrelsBox.StorageManagement\\text1.txt");

            // Do something with the count, for example, return it as a response
            return Ok(count);
        }

        private async Task<int> CountWordsWithThreeEs(string filePath)
        {
            try
            {
                // Read the content of the file asynchronously
                using (var reader = new StreamReader(filePath))
                {
                    var content = await reader.ReadToEndAsync();

                    // Split the content into words based on spaces
                    var words = content.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    // Initialize count of words containing exactly three 'e's
                    int count = 0;

                    // Loop through each word and check if it contains exactly three 'e's
                    foreach (var word in words)
                    {
                        // Check if the word is lowercase and contains exactly three 'e's
                        if (word.ToLower() == word && CountEs(word.ToLower()) == 3)
                        {
                            count++;
                        }
                    }

                    return count;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                Console.WriteLine("Error: " + ex.Message);
                return -1; // or throw an exception, depending on your application's requirements
            }
        }

        // Function to count occurrences of 'e' in a word
        private int CountEs(string word)
        {
            int count = 0;

            foreach (char c in word)
            {
                if (c == 'e')
                {
                    count++;
                }
            }

            return count;
        }
    }
}

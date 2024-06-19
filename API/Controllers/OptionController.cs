using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObject.Model;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Repository;
using Repository.IRepository;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionController : ControllerBase
    {
        private readonly OptionIRepository _optionRepository;

        public OptionController(OptionIRepository optionRepository)
        {
            _optionRepository = optionRepository;
        }

        [HttpPost]
        public async Task<ActionResult<List<Option>>> AddOptionAsync(List<Option> option)
        {
            var addedOption = await _optionRepository.AddOptionAsync(option);
            return Ok(addedOption);
        }
    }
}

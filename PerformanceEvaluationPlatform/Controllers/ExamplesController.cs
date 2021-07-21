using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.DAL.Models.Examples.Dto;
using PerformanceEvaluationPlatform.DAL.Repositories.Examples;
using PerformanceEvaluationPlatform.Models.Example.RequestModels;
using PerformanceEvaluationPlatform.Models.Example.ViewModels;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class ExamplesController : ControllerBase
    {
        private readonly IExamplesRepository _examplesRepository;

        public ExamplesController(IExamplesRepository examplesRepository)
        {
            _examplesRepository = examplesRepository ?? throw new ArgumentNullException(nameof(examplesRepository));
        }

        [HttpGet("examples")]
        public async Task<IActionResult> Get([FromQuery]ExampleListFilterRequestModel filter)
        {
            var filterDto = new ExampleListFilterDto
            {
                Search = filter.Search,
                Skip = filter.Skip,
                Take = filter.Take,
                StateId = filter.StateId,
                TitleSortOrder = (int?) filter.TitleSortOrder,
                TypeIds = filter.TypeIds
            };

            var itemsDto = await _examplesRepository.GetList(filterDto);
            var items = itemsDto.Select(t => new ExampleListItemViewModel
            {  
                Id = t.Id,
                State = t.StateName,
                Type = t.TypeName,
                Title = t.Title
            });
            return Ok(items);
        }

        [HttpGet("examples/types")]
        public async Task<IActionResult> GetTypes()
        {
            var itemsDto = await _examplesRepository.GetTypesList();
            var items = itemsDto
                .Select(t => new ExampleTypeListItemViewModel
                {
                    Id = t.Id,
                    Title = t.Title
                });

            return Ok(items);
        }

        [HttpGet("examples/states")]
        public async Task<IActionResult> GetStates()
        {
            var itemsDto = await _examplesRepository.GetStatesList();
            var items = itemsDto
                .Select(t => new ExampleStateListItemViewModel
                {
                    Id = t.Id,
                    Name = t.Title
                });

            return Ok(items);
        }

        [HttpGet("examples/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var detailsDto = await _examplesRepository.GetDetails(id);
            if (detailsDto == null)
            {
                return NotFound();
            }

            var detailsVm = new ExampleDetailsViewModel
            {
                Id = detailsDto.Id,
                Title = detailsDto.Title,
                StateName = detailsDto.StateName,
                TypeName = detailsDto.TypeName
            };

            return Ok(detailsVm);
        }

    }
}

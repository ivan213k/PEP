using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.DAL.Models.Examples.Dao;
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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

        [HttpPut("examples/{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody]UpdateExampleRequestModel requestModel)
        {
            var entity = await _examplesRepository.Get(id);
            if (entity == null)
            {
                return NotFound();
            }

            var exampleType = await _examplesRepository.GetType(requestModel.TypeId);
            if (exampleType == null)
            {
                return BadRequest("Type does not exists.");
            }

            var exampleState = await _examplesRepository.GetState(requestModel.StateId);
            if (exampleState == null)
            {
                return BadRequest("State does not exists.");
            }


            entity.Title = requestModel.Title;
            entity.ExampleTypeId = requestModel.TypeId;
            entity.ExampleStateId = requestModel.StateId;

            await _examplesRepository.SaveChanges();
            return Ok();
        }

        [HttpPost("examples")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody]CreateExampleRequestModel requestModel)
        {
            var exampleType = await _examplesRepository.GetType(requestModel.TypeId);
            if (exampleType == null)
            {
                return BadRequest("Type does not exists.");
            }

            var exampleState = await _examplesRepository.GetState(requestModel.StateId);
            if (exampleState == null)
            {
                return BadRequest("State does not exists.");
            }

            var example = new Example
            {
                Title = requestModel.Title,
                ExampleState = exampleState,
                ExampleType = exampleType
            };

            await _examplesRepository.Create(example);
            await _examplesRepository.SaveChanges();

            var result = new ObjectResult(new {Id = example.Id}) {StatusCode = 201};
            return result;

        }

    }
}

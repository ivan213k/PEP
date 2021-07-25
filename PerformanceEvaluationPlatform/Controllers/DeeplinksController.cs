using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.DAL.Models.Deeplinks.Dto;
using PerformanceEvaluationPlatform.DAL.Repositories.Deeplinks;
using PerformanceEvaluationPlatform.Models.Deeplink.RequestModels;
using PerformanceEvaluationPlatform.Models.Deeplink.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    public class DeeplinksController : ControllerBase
    {
        private readonly IDeeplinksRepository _deeplinksRepository;

        public DeeplinksController(IDeeplinksRepository deeplinkRepository)
        {
            _deeplinksRepository = deeplinkRepository ?? throw new ArgumentNullException(nameof(deeplinkRepository));
        }

        [HttpGet("deeplinks")]
        public async Task<IActionResult> Get([FromQuery] DeeplinkListFilterRequestModel filter)
        {
            var filterDto = new DeeplinkListFilterDto
            {
                Search = filter.Search,
                SentToId = filter.SentToId,
                StateIds = filter.StateIds,
                ExpiresAtFrom = filter.ExpiresAtFrom,
                ExpiresAtTo = filter.ExpiresAtTo,
                SentToOrder = (int?)filter.OrderSentTo,
                ExpiresAtOrder = (int?)filter.OrderExpiresAt,
                Skip = filter.Skip,
                Take = filter.Take
            };

            var itemsDto = await _deeplinksRepository.GetList(filterDto);
            var items = itemsDto.Select(t => new DeeplinkListItemViewModel
            {
                Id = t.Id,
                State = t.State,
                SentTo = $"{t.SentToFirstName} {t.SentToLastName}",
                ExpiresAt = t.ExpireDate,
                FormTemplateName = t.FormTemplate

            });
            return Ok(items);
        }
        [HttpGet("deeplinks/states")]
        public async Task<IActionResult> GetStates()
        {
            var itemsDto = await _deeplinksRepository.GetStatesList();
            var items = itemsDto
                .Select(t => new DeeplinkStateListItemViewModel
                {
                    Id = t.Id,
                    Name = t.Title
                });

            return Ok(items);
        }


     /*   [HttpGet("deeplinks/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var detailsDto = await _deeplinksRepository.GetDetails(id);
            if (detailsDto == null)
            {
                return NotFound();
            }

            var detailsVm = new DeeplinkDetailsViewModel
            {
                Id = detailsDto.Id,
                SentTo = $"{detailsDto.SentToFirstName} {detailsDto.SentToSecondtName}",
                SentToEmail = detailsDto.SentToEmail,
                SentAt = detailsDto.SentAt,
                SentBy = detailsDto.SentBy,
                State = detailsDto.StateName,
                ExpiresAt = detailsDto.ExpiresAt,
                FormTemplateName = detailsDto.FormTemplateName
            };

            return Ok(detailsVm);
        }
      */
       
        
        [HttpGet("deeplinks/{id}")]
        public IActionResult GetDeeplinkDetails([FromRoute] int id)
        {
            var DeeplinkDetails = new DeeplinkDetailsViewModel
            {
                SentTo = "User Test",
                SentToEmail = "Test@gmail.com",
                SentAt = new DateTime(2021,07,11),
                SentBy = "Another user",
                State = "InProgrees",
                ExpiresAt = new DateTime(2022,2,26),
                FormTemplateName = "Form 1"
            };

            return Ok(DeeplinkDetails);
        }
        
        
    }
}

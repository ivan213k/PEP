using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.DAL.Models.Deeplinks.Dao;
using PerformanceEvaluationPlatform.DAL.Models.Deeplinks.Dto;
using PerformanceEvaluationPlatform.DAL.Repositories.Deeplinks;
using PerformanceEvaluationPlatform.DAL.Repositories.FormTemplates;
using PerformanceEvaluationPlatform.DAL.Repositories.Surveys;
using PerformanceEvaluationPlatform.DAL.Repositories.Users;
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
        private readonly IUserRepository _usersRepository;
        private readonly ISurveysRepository _surveysRepository;
        private readonly IFormTemplatesRepository _formTemplatesRepository;

        public DeeplinksController(IDeeplinksRepository deeplinkRepository, IUserRepository userRepository, ISurveysRepository surveysRepository, IFormTemplatesRepository formTemplatesRepository)
        {
            _deeplinksRepository = deeplinkRepository ?? throw new ArgumentNullException(nameof(deeplinkRepository));
            _usersRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _surveysRepository = surveysRepository ?? throw new ArgumentNullException(nameof(surveysRepository));
            _formTemplatesRepository = formTemplatesRepository ?? throw new ArgumentNullException(nameof(formTemplatesRepository));
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

        [HttpGet("deeplinks/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {

            // Deeplink Data
            var detailsDeeplinkDto = await _deeplinksRepository.GetDetails(id);
            if (detailsDeeplinkDto == null)
            {
                return NotFound();
            }


            var detailsVm = new DeeplinkDetailsViewModel
            {
                Id = detailsDeeplinkDto.Id,
                SentTo = $"{detailsDeeplinkDto.SentTo.FirstName} {detailsDeeplinkDto.SentTo.LastName}",
                SentToEmail = detailsDeeplinkDto.SentTo.Email,
                SentAt = detailsDeeplinkDto.SentAt,
                SentBy = $"{detailsDeeplinkDto.SentBy.FirstName} {detailsDeeplinkDto.SentBy.LastName}",
                State = detailsDeeplinkDto.StateName,
                ExpiresAt = detailsDeeplinkDto.ExpiresAt,
                FormTemplateName = detailsDeeplinkDto.FormTemplateName
            };

            return Ok(detailsVm);
        }

        [HttpPut("deeplinks/{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateDeeplinkRequestModel requestModel)
        {
            if (requestModel is null)
            {
                return BadRequest();
            }
            if (requestModel.ExpiresAt <= DateTime.Today)
            {
                return BadRequest();
            }
            var entity = await _deeplinksRepository.Get(id);
            if (entity == null)
            {
                return NotFound();
            }


            entity.ExpireDate = requestModel.ExpiresAt;

            await _deeplinksRepository.SaveChanges();
            return Ok();
        }

        [HttpPost("deeplinks")]
        public async Task<IActionResult> Create([FromBody] CreateDeeplinkRequestModel requestModel)
        {
            if(requestModel == null)
            {
                return BadRequest();
            }
            //Exist?
            // User To
            var userTo = await _usersRepository.Get(requestModel.UserId);
            if(userTo == null)
            {
                return BadRequest("User does not exists");
            }
            // User by
            var userBy = await _usersRepository.Get(requestModel.SentById);
            if (userBy == null)
            {
                return BadRequest("User does not exists");
            }

            //  Survey
            var survey = await _surveysRepository.Get(requestModel.SurveyId);
            if (survey == null)
            {
                return BadRequest("Survey does not exists");
            }
            // ID Users
            if (ContainsSameUserIds(requestModel.UserId,requestModel.SentById))
            {
                return BadRequest("Contains same user id");
            }

            var deeplink = new Deeplink
            {
                StateId= GetNewStateId(),
                UserId = requestModel.UserId,
                SentById = requestModel.SentById,
                ExpireDate = requestModel.ExpiresDate,
                SurveyId = requestModel.SurveyId,
                Code = Guid.NewGuid(),
                SentAt = DateTime.Today

            };

            await _deeplinksRepository.Create(deeplink);
            await _deeplinksRepository.SaveChanges();

            var result = new ObjectResult(new { Id = deeplink.Id }) { StatusCode = 201 };
            return result;

        }
        private bool ContainsSameUserIds(int SentToId, int SentById)
        {
            return (SentToId == SentById);
        }
        private int GetNewStateId()
        {
            return 1;
        }
    }
}

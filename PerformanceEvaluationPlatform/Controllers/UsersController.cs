using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using PerformanceEvaluationPlatform.Application.Services.Users;
using PerformanceEvaluationPlatform.Models.User.Auth0;
using PerformanceEvaluationPlatform.Models.User.RequestModels;
using PerformanceEvaluationPlatform.Models.User.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _host;
        public UsersController(IUserService userService,
            IWebHostEnvironment host)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _host = host ?? throw new ArgumentNullException(nameof(host));
        }

        //   [Authorize(Policy = Policy.User)]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] UserFilterRequestModel userFilter)
        {
            
            var itemsResponse = await _userService.GetList(userFilter.AsDto());
            if (TryGetErrorResult(itemsResponse, out IActionResult errorResult))
            {
                return errorResult;
            }

            var itemsVm = itemsResponse.Payload.Select(t => t.AsViewModel());
            return Ok(itemsVm);
        }

        //  [Authorize(Policy = Policy.User)]
        [HttpGet("userstate")]
        public async Task<IActionResult> GetUserStates()
        {
            var itemsResponse = await _userService.GetStates();
            if (TryGetErrorResult(itemsResponse, out IActionResult errorResult))
            {

                return errorResult;
            }
            var itemsVm = itemsResponse.Payload.Select(t => t.AsViewModel());
            return Ok(itemsVm);
        }

        [HttpGet("systemrole")]
        public async Task<IActionResult> GetSystemRoles()
        {
            var itemsResponse = await _userService.GetSystemRoles();
            if (TryGetErrorResult(itemsResponse, out IActionResult errorResult))
            {
                return errorResult;
            }
            var itemsVm = itemsResponse.Payload.Select(t => t.AsViewModel());
            return Ok(itemsVm);
        }

        //[Authorize(Policy = Policy.User)]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var itemsResponse = await _userService.GetDetail(id);
            if (TryGetErrorResult(itemsResponse, out IActionResult errorResult))
            {
                return errorResult;
            }
            return Ok(itemsResponse.Payload.AsViewModel());
        }

        //[Authorize(Policy = Policy.Admin)]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> EditUser(int id, [FromBody] EditUserRequestModel editedUser)
        {
            var serviceResponse = await _userService.Update(id,editedUser.AsDto());
            return TryGetErrorResult(serviceResponse, out IActionResult errorResult)?errorResult: Ok();
        }


        [HttpPost()]
        //  [Authorize(Policy =Policy.Admin)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestModel createUserRequest)
        {
            var serviceReponse = await _userService.Create(createUserRequest.AsDto(),_host.IsDevelopment());
            return TryGetErrorResult(serviceReponse, out IActionResult errorResult) ? errorResult : Ok();
        }

        [HttpPut("{id:int}/activate")]
        // [Authorize(Policy = Policy.Admin)]
        public async Task<IActionResult> ActivateUser(int id)
        {
            var serviceResponse = await _userService.Activate(id);
            return TryGetErrorResult(serviceResponse, out IActionResult errorResult) ? errorResult : Ok();
        }
        [HttpPut("{id:int}/suspend")]
        //[Authorize(Policy = Policy.Admin)]
        public async Task<IActionResult> SuspendUser(int id)
        {
            var serviceResponse = await _userService.Suspend(id);
            return TryGetErrorResult(serviceResponse, out IActionResult errorResult) ? errorResult : Ok();
        }



    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;
using OnlineShop.APIs.DTOs;
using OnlineShop.APIs.Features;
using OnlineShop.Application.Commands.User.Create;
using OnlineShop.Application.Commands.User.CreateOption;
using OnlineShop.Application.Commands.User.CreateTag;
using OnlineShop.Application.Commands.User.Delete;
using OnlineShop.Application.Commands.User.DeleteOption;
using OnlineShop.Application.Commands.User.DeleteTag;
using OnlineShop.Application.Commands.User.ToggleActivation;
using OnlineShop.Application.Commands.User.Update;
using OnlineShop.Application.Queries.User.GetList;
using OnlineShop.DomainService.Features;
using OnlineShop.DomainService.Services;

namespace OnlineShop.APIs.Controllers
{
    [ApiController]
    [Route("Users")]
    public class UserController(IUserService userService, IMediator mediator, IFeatureManager featureManager) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto newUser, CancellationToken cancellation)
        {
            var command = new CreateUserCommand(newUser.FirstName, newUser.LastName, newUser.PhoneNumber, newUser.Coordinate, newUser.Type);
            await mediator.Send(command, cancellation);

            return Ok(BaseResult.Success());
        }




        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] UpdateUserDto input, CancellationToken cancellation)
        {
            var command = new UpdateUserCommand(id, input.FirstName, input.LastName, input.PhoneNumber, input.Coordinate, input.Type);
            await mediator.Send(command, cancellation);

            return Ok(BaseResult.Success());
        }




        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellation)
        {
            var command = new DeleteUserCommand(id, cancellation);
            await mediator.Send(command, cancellation);

            return Ok();
        }





        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellation)
        {
            var user = await userService.GetByIdAsync(id, cancellation);
            return Ok(BaseResult.Success(user));
        }







        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] string? q, [FromQuery] int pageSize, [FromQuery] int pageNumber, OrderType orderType, CancellationToken cancellation)
        {
            //var viewModel = await userService.GetListAsync(q, pageSize, pageNumber, orderType, cancellation);

            var query = new GetUserListQueries(q, pageSize, pageNumber, orderType);
            
            var viewModel = await mediator.Send(query, cancellation);

            return Ok(BaseResult.Success(viewModel));
        }


        [HttpGet("{Prefix}/{code}")]
        public async Task<IActionResult> GetByTrackingCode([FromRoute] string code, [FromRoute] string Prefix, CancellationToken cancellation)
        {
            var user = await userService.GetTrackingCodeAsync(code, Prefix, cancellation);
            return Ok(BaseResult.Success(user));
        }



        [HttpPut("{id:int}/ToggleActivation")]
        public async Task<IActionResult> ToggleActivation([FromRoute] int id, CancellationToken cancellation)
        {
            var command = new ToggleActivationUserCommand(id, cancellation);
            await mediator.Send(command, cancellation);

            return Ok(BaseResult.Success());
        }

        



        [HttpPost("{id:int}/Options")]
        public async Task<IActionResult> CreateOption([FromRoute] int id, [FromBody] CreateUserOptionDTO input, CancellationToken cancellationToken)
        {
            var command = new CreateUserOptionCommand(id, input.Description);
            await mediator.Send(command, cancellationToken);

            return Ok(BaseResult.Success());
        }




        [HttpDelete("{id:int}/Options/{optionId:int}")]
        public async Task<IActionResult> DeleteOption([FromRoute] int id, [FromRoute] int optionId, CancellationToken cancellationToken)
        {
            var command = new DeleteUserOptionCommand(id, optionId);
            await mediator.Send(command, cancellationToken);

            return Ok(BaseResult.Success());
        }




        [HttpPost("{id:int}/Tags")]
        public async Task<IActionResult> CreateTag([FromRoute] int id, [FromBody] CreateUserTagDTO input, CancellationToken cancellationToken)
        {
            var command = new CreateUserTagCommand(id, input.Title, input.Priority);
            await mediator.Send(command, cancellationToken);

            return Ok(BaseResult.Success());
        }




        [HttpDelete("{id:int}/Tags")]
        public async Task<IActionResult> DeleteTag([FromRoute] int id, [FromQuery] string title, [FromQuery] int priority, CancellationToken cancellationToken)
        {
            var command = new DeleteUserTagCommand(id, title, priority);
            await mediator.Send(command, cancellationToken);

            return Ok(BaseResult.Success());
        }



        [HttpGet("/Key")]
        [FeatureGate("GetKeyFeature")]
        public async Task<IActionResult> GetKey()
        {
            List<string> featureNames = [];

            await foreach (var featureName in featureManager.GetFeatureNamesAsync())
            {
                featureNames.Add(featureName);
            }

            return Ok(BaseResult.Success(featureNames));
        }
    }
}

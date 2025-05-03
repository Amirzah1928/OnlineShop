using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using OnlineShop.DTOs;
using OnlineShop.Models;
using OnlineShop.Services;
using OnlineShop.ViewModels;

namespace OnlineShop.Controllers
{
    [ApiController]
    [Route("Users")]
    public class UserController(IUserService userService) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto newUser, CancellationToken cancellation)
        {
            await userService.CreateAsync(newUser,cancellation);

            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] UpdateUserDto input, CancellationToken cancellation)
        {
            await userService.UpdateAsync(id,input,cancellation);

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellation)
        {
            await userService.DeleteAsync(id,cancellation);

            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellation)
        {
            var result = await userService.GetByIdAsync(id,cancellation);
            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] string? q, CancellationToken cancellation)
        {
           var viewModel = await userService.GetListAsync(q,cancellation);

            return Ok(viewModel);
        }

        [HttpPut("{id:int}/ToggleActivation")]
        public async Task<IActionResult> ToggleActivation([FromRoute] int id, [FromBody] UpdateUserDto input, CancellationToken cancellation)
        {
            await userService.ToggleActivationAsync(id,input,cancellation);

            return Ok();
        }
    }
}

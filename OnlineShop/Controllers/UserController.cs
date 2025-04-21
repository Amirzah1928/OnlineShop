using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.DTOs;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    [ApiController]
    [Route("Users")]
    public class UserController(OnlineShopDBContext db) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto newUser, CancellationToken cancellation)
        {
            var user = new User
            {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                PhoneNumber = newUser.PhoneNumber,
            };

            await db.Users.AddAsync(user, cancellation);
            await db.SaveChangesAsync(cancellation);

            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] UpdateUserDto input, CancellationToken cancellation)
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == id, cancellation);
            if (user == null)
                return NotFound();
            if (input.FirstName != string.Empty)
                user.FirstName = input.FirstName;

            if (input.LastName != string.Empty)
                user.LastName = input.LastName;

            db.Users.Update(user);
            await db.SaveChangesAsync(cancellation);

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id, [FromBody] CreateUserDto newUser, CancellationToken cancellation)
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == id, cancellation);
            if (user == null)
                return NotFound();

            db.Users.Remove(user);
            await db.SaveChangesAsync(cancellation);

            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUsers([FromRoute] int id, CancellationToken cancellation)
        {
            var users = await db.Users.FirstOrDefaultAsync(x => x.Id == id, cancellation);
            if (users == null)
                return NotFound();

            return Ok(users);
        }


        [HttpGet]
        public async Task<IActionResult> GetUsersToList(CancellationToken cancellation)
        {
            var users = await db.Users.ToListAsync(cancellation);
            return Ok(users);
        }

        [HttpPut("{id:int}/ToggleActivation")]
        public async Task<IActionResult> ToggleActivation([FromRoute] int id, [FromBody] UpdateUserDto input, CancellationToken cancellation)
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == id, cancellation);
            if (user == null)
                return NotFound();

            user.Isactive = !user.Isactive;


            db.Users.Update(user);
            await db.SaveChangesAsync(cancellation);

            return Ok();
        }

        [HttpGet("{id:int}/GetFullName")]
        public async Task<IActionResult> GetUsersFullName([FromRoute] int id, CancellationToken cancellation)
        {
            var users = await db.Users.FirstOrDefaultAsync(x => x.Id == id, cancellation);
            if (users == null)
                return NotFound();

            var user = new GetUsersWithFullNameDto
            {
                Id = users.Id,
                FirstName = users.FirstName,
                LastName = users.LastName,
                PhoneNumber = users.PhoneNumber,
                Isactive = users.Isactive,
                FullName = users.FirstName + " " + users.LastName,
            };
           
            return Ok(user);
        }


        [HttpGet("SearchUsers")]
        public async Task<IActionResult> SearchUsers([FromQuery] string q, CancellationToken cancellation)
        {
            var usersList = await db.Users.Where(u => u.FirstName.Contains(q) || u.LastName.Contains(q))
            .ToListAsync(cancellation);
            if (usersList.Count == 0)
                return NotFound();

            

            return Ok(usersList);
        }
    }
}

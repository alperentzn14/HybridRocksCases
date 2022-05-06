using HybridRocksCases.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HybridRocksCases.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly HybridDbContext _HybridContext;
        public UserController(HybridDbContext HybridContext)
        {
            _HybridContext = HybridContext;
        }
        //// GETLİST
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetList()
        {
            return await _HybridContext.Users.ToListAsync();
        }

        //// GET
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _HybridContext.Users.FindAsync(id);
            
            if(user == null)
            {
                return NotFound();
            }
            return user;
        }

        // POST
        [HttpPost]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            var userItem = new User
            {
                Name = user.Name,
                Surname = user.Surname
            };

            _HybridContext.Users.Add(userItem);
            await _HybridContext.SaveChangesAsync();

            return user;
        }


        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            var userItem = await _HybridContext.Users.FindAsync(id);
            if (userItem == null)
            {
                return NotFound();
            }

            userItem.Name = user.Name;
            userItem.Surname= user.Surname;

            try
            {
                await _HybridContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!UserItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }
        // DELETE

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var userItem = await _HybridContext.Users.FindAsync(id);

            if (userItem == null)
            {
                return NotFound();
            }

            _HybridContext.Users.Remove(userItem);
            await _HybridContext.SaveChangesAsync();

            return NoContent();
        }
        private bool UserItemExists(int id)
        {
            return _HybridContext.Users.Any(u => u.Id == id);
        }
      

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsersManagementApi.Data;

namespace UsersManagementApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private static List<User> users = new List<User>
        {
            {
                new User
                {
                    Id =1,
                    Name= "Aziz",
                    Age= 20,
                    Address = "Madinna jadida "
                }
                }

        };
        private readonly AppDbContext  _context;
        public UsersController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet()]
        public async Task<ActionResult<List<User>>> Get()
        {
            // return Ok(users);
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return BadRequest("User Not Found");
            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult<List<User>>> AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(await _context.Users.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<List<User>>> UpdateUser(User request)
        {

            var user = await _context.Users.FindAsync(request.Id);
            if (user == null)
                return BadRequest("User Not Found");
            user.Name = request.Name;
            user.Age = request.Age;
            user.Address = request.Address;
            await _context.SaveChangesAsync();
            return Ok(await _context.Users.ToListAsync());


        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<User>>> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return BadRequest("User Not Found");
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Ok(await _context.Users.ToListAsync());
        }




    }
}

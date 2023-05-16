using CRUDNetCore6.Server.Services;
using CRUDNetCore6.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDNetCore6.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser iUser;

        public UserController(IUser iUser)
        {
            this.iUser = iUser;
        }

        [HttpGet]
        public async Task<List<User>> UserList()
        {
            return await Task.FromResult(iUser.DataUsers());
        }
        [HttpPost]
        public void Post(User user)
        {
            iUser.NewUser(user);
        }

        [HttpGet("{id}")]
        public IActionResult GetDataUser(int id)
        {
            User user = iUser.GetUser(id);
            if(user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut]
        public void EditUser(User user)
        {
            iUser.UpdateUser(user);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            iUser.DeleteUser(id);
            return Ok();
        }
    }
}

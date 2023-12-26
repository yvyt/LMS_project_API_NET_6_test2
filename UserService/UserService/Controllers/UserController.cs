using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Data;
using UserService.Model;
using UserService.Repository;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository) {
            _repository=repository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var data= _repository.getAll();
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);

            }catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetById(string id) {
            try
            {
                return Ok(_repository.getById(id));

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(string id,UserVM user) {
            try
            {
                if(id != user.Id){
                    return BadRequest();
                }
                _repository.Update(user);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteById(string id)
        {
            try
            {
                _repository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        [HttpPost]
        public IActionResult AddUser(UserVM user)
        {
            try
            {
                
                return Ok(_repository.Add(user));
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }
    }

}

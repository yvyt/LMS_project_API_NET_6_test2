using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
        private readonly AppSettings _appSettings;

        public UserController(IUserRepository repository, IOptionsMonitor<AppSettings> optionsMonitor) {
            _repository=repository;
            _appSettings = optionsMonitor.CurrentValue;
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

        [HttpPost("Login")]
        public IActionResult Validate(UserLogin user)
        {
            var us = _repository.Validate(user);
            if (us == null)
            {
                return Ok(new
                {
                    Success=false,
                    Message="Invalid Email or Passwork"
                });
            }
            User user1= new User
            {
                Id = us.Id,
                Email = us.Email,
                FirstName = us.FirstName,
                LastName = us.LastName,
                Password = us.Password,
                Gender = us.Gender,
                Phone = us.Phone,
                DateOfBirth = us.DateOfBirth,
                Avatar = us.Avatar,
                IsFirstLogin = us.IsFirstLogin,
                Address = us.Address,
                IsActive = us.IsActive,
                CreatedAt = us.CreatedAt,
                UpdatedAt = us.UpdatedAt,
            };
            return Ok(new
            {
                Success = true,
                Message = "Authenticate success",
                Data=GenerateToken(user1),
            });
        }

        private string GenerateToken(User user)
        {
            var jwtToken = new JwtSecurityTokenHandler();
            var secretKeyByte= Encoding.UTF8.GetBytes(_appSettings.SecretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("TokenId", Guid.NewGuid().ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyByte),SecurityAlgorithms.HmacSha512Signature)
                
            };

            var token=jwtToken.CreateToken(tokenDescription);
            return jwtToken.WriteToken(token);

        }
    }

}

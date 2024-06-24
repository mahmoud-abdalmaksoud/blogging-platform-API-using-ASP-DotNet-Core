using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Data;
using UdemyProject.Data;
using UdemyProject.Data.Models;
using UdemyProject.Dtos;
using AutoMapper;
using Microsoft.VisualBasic;
namespace UdemyProject.Controllers
{
    
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
         AppDbContext _contex;
         IMapper _mapper;

        public UserController(AppDbContext context , IMapper mapper) {
            _contex = context;
            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AddUserDto, User>();
            }));

        }
        [HttpGet("GetUsers")]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            var records = _contex.Set<User>().ToList();
            return Ok(records);
        }
        [HttpGet("GetUser/{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var record = _contex.Users.Single(u => u.UserId == id);
            return Ok(record);
        }

        [HttpPost("AddUser")]
        public ActionResult<User> AddUser(AddUserDto userDto)
        {
          
            User user = _mapper.Map<User>(userDto);
            _contex.Users.Add(user);
            _contex.SaveChanges();
            return Ok();
        }

        [HttpPut("EditUser/{id}")]
        public IActionResult EditUser(User user, int id)
        {
            var usr = _contex.Users.FirstOrDefault(u => u.UserId == id);

            usr.FirstName = user.FirstName;
            usr.LastName = user.LastName;
            usr.Email = user.Email;
            usr.Gender = user.Gender;
            usr.Active = user.Active;

            _contex.SaveChanges();

            return Ok();
        }
        [HttpDelete("deleteUser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _contex.Users.FirstOrDefault(usr => usr.UserId == id);
            _contex.Users.Remove(user);
            _contex.SaveChanges();
            return Ok();
        }

    }
}

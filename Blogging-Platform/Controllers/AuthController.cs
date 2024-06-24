using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyProject.Data;
using UdemyProject.Data.Models;
using UdemyProject.Dtos;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using UdemyProject.Helper;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace UdemyProject.Controllers

{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        AppDbContext _context;
        IMapper _mapper;
        AuthHelper _authHelper;
        public AuthController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _authHelper = new AuthHelper(config);

            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserForRegistrationDto, User>();
            }));
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public ActionResult RegisterUser(UserForRegistrationDto userToRegister)
        {
            if (userToRegister.Password != userToRegister.PasswordConfirm)
                throw new Exception("passwords do not match");
            bool checkIfUserExist = _context.Users.Any(u => u.Email == userToRegister.Email);
            if (checkIfUserExist)
            {
                throw new Exception("user with this Email Already Exist");
            }
            byte[] passwordSalt = new byte[128 / 8];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetNonZeroBytes(passwordSalt);
            }

            byte[] passwordHash = _authHelper.GetPasswordHash(userToRegister.Password, passwordSalt);
            var auth = new Auth()
            {
                Email = userToRegister.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            _context.UsersAuth.Add(auth);
            if (_context.SaveChanges() > 1) throw new Exception("Failed to add register");


            var user = _mapper.Map<User>(userToRegister);
            _context.Users.Add(user);
            if (_context.SaveChanges() > 1) throw new Exception("failed to add a user");



            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public ActionResult Login(UserLoginDTO userToLogin)
        {
            var userToConfirm = _context.UsersAuth.Where(usr => usr.Email == userToLogin.Email).FirstOrDefault();
            if (userToConfirm == null) throw new Exception(" user with this email does not exist");

            byte[] passwordHash = _authHelper.GetPasswordHash(userToLogin.Password, userToConfirm.PasswordSalt);
            for (int i = 0; i < passwordHash.Length; i++)
            {
                if (passwordHash[i] != userToConfirm.PasswordHash[i])
                {
                    return StatusCode(401, "Incorrect password!");
                }
            }
            var user = _context.Users.SingleOrDefault(usr => usr.Email == userToLogin.Email);

            return Ok(_authHelper.CreateToken(user.UserId));
        }


        [HttpPut]
        [Route("Reset-Password")]
        public ActionResult Reset(ResetPasswordDto resetPasswordDto)
        {
            if (resetPasswordDto.password != resetPasswordDto.confirmPassword)
                throw new Exception("passwords do not match");

            int userToResetId = int.Parse(User.FindFirst("userId")!.Value);
            string userEmail = _context.Users.FirstOrDefault(usr => usr.UserId == userToResetId)!.Email;

            var userToResetAuth = _context.UsersAuth.FirstOrDefault(usrAuth => usrAuth.Email == userEmail);

            byte[] passwordSalt = new byte[128 / 8];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetNonZeroBytes(passwordSalt);
            }

            byte[] passwordHash = _authHelper.GetPasswordHash(resetPasswordDto.password, passwordSalt);
            userToResetAuth.PasswordHash = passwordHash;
            userToResetAuth.PasswordSalt = passwordSalt;
            if (_context.SaveChanges() >  0) return Ok();

            throw new Exception("failed to reset user");
        }


        [HttpGet("RefreshToken")]
        public string RefreshToken()
        {

            // extract the User id from the token
            var userId = User.FindFirst("userId")?.Value;

            return _authHelper.CreateToken(int.Parse(userId));
        }
    }

}

using Microsoft.AspNetCore.Mvc;
using PasswordManager5._0.Entities;
using System.Collections.Generic;
using PasswordManager5._0.Models;
using System.Linq;
using AutoMapper;
using PasswordManager5._0.Functions;
using System.Security.Policy;
using System;

namespace PasswordManager5._0.Controllers
{
    [Route("api/PasswordManager")]
    public class PasswordManagerController : ControllerBase
    {
        private readonly PasswordManagerDbContext _dbContext;
        private readonly IMapper _mapper;
        public PasswordManagerController(PasswordManagerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public ActionResult<IEnumerable<User>> GetAll()
        {
            return Ok("Hello ;) ");
        }

        [HttpGet("/password")]
        public ActionResult<Password> Get([FromHeader]string hash)
        {
            int userIdFromHash = 0;
            int haveUserAnyPassword = 0;
            int isUserExist = _dbContext
                    .Users
                    .Count(r => r.UserHash == hash);
            if(isUserExist == 1)
            {
                 userIdFromHash = _dbContext
                    .Users
                    .FirstOrDefault(r => r.UserHash == hash)
                    .userId;
            }
            else { return BadRequest("Something went Wrong"); }

            if(isUserExist ==1 && userIdFromHash > 0)
            {
                haveUserAnyPassword = _dbContext
                .Passwords
                .Count(r => r.UserId == userIdFromHash);
            }
            else { return BadRequest("User have not any password"); }
            if (isUserExist == 1 && userIdFromHash > 0 && haveUserAnyPassword > 0)
            {
                var allUserPassword = _dbContext
                    .Passwords
                    .Where(r => r.UserId == userIdFromHash)
                    .ToList();
                return Ok(allUserPassword);
            }
            else { return BadRequest("User have not any password"); }
        }

        [HttpPost("/password")]
        public ActionResult AddNewPassword([FromHeader] string hash, [FromBody] AddNewPasswordDTO dto)
        {
            var passwordCandidate = _mapper.Map<Password>(dto);
            int userIdFromHash = 0;
            int isUserExist = _dbContext
                    .Users
                    .Count(r => r.UserHash == hash);
            if (isUserExist == 1)
            {
                userIdFromHash = _dbContext
                   .Users
                   .FirstOrDefault(r => r.UserHash == hash)
                   .userId;
            }
            else { return BadRequest("Something went Wrong"); }
            int IsPasswordExist = _dbContext
                .Passwords
                .Count(r => r.UserId == userIdFromHash &&
                r.ServiceName == passwordCandidate.ServiceName &&
                r.Login == passwordCandidate.Login &&
                r.PasswordValue == passwordCandidate.PasswordValue);

            if (IsPasswordExist>0)
            {
                return BadRequest("PasswordExist");
            }
            else
            {
                string time = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
                passwordCandidate.Timestamp = time;
                passwordCandidate.UserId = userIdFromHash;
                _dbContext.Passwords.Add(passwordCandidate);
                _dbContext.SaveChanges();
                return Ok("password Saved");
            }
        }

        [HttpDelete("/password")]
        public ActionResult Delete([FromHeader] string hash, [FromBody] DeletePasswordDTO dto)
        {
            var passwordCandidate = _mapper.Map<Password>(dto);
            int userIdFromHash = 0;
            int isUserExist = _dbContext
                    .Users
                    .Count(r => r.UserHash == hash);
            if (isUserExist == 1)
            {
                userIdFromHash = _dbContext
                   .Users
                   .FirstOrDefault(r => r.UserHash == hash)
                   .userId;
            }
            else
            {
                return BadRequest("Something went Wrong");
            }
            var delPass = _dbContext
                .Passwords
                .FirstOrDefault(r => r.UserId == userIdFromHash
                && r.ServiceName == passwordCandidate.ServiceName
                && r.Login == passwordCandidate.Login
                && r.PasswordValue == passwordCandidate.PasswordValue);
            if(delPass is null)
            {
                return BadRequest("Something went Wrong");
            }
            else
            {
                _dbContext.Passwords.Remove(delPass);
                _dbContext.SaveChanges();

                return Ok("Removed");
            }
        }

        [HttpPost("/signup")]
        public ActionResult SignupUser([FromBody] SignupUserDTO dto, Userhash userhash)
        {
            var user = _mapper.Map<User>(dto);

            int isUserExist = _dbContext
                .Users
                .Count(r => r.Username == user.Username || r.Email == user.Email);
                

            
            if(isUserExist == 0)
            {
            user.UserHash = userhash.GetUserHash(user.Username, user.Password, user.Email);
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
                return Ok("user created");
            }

            return BadRequest("UserExist");
        }

        [HttpPost("/login")]
        public ActionResult LoginUser([FromBody] LoginUserDTO dto)
        {
            var user = _mapper.Map<User>(dto);

            if ((String.IsNullOrEmpty(user.Username)) ||
                (String.IsNullOrEmpty(user.Password)) ||
                (String.IsNullOrEmpty(user.Email))
                )
            {
                return BadRequest("IncorrectData");
            }

            int isUserExist = _dbContext
                .Users
                .Count(r => r.Username == user.Username && r.Email == user.Email && r.Password == user.Password);
        
            if(isUserExist == 1)
            {
                var userHashFromData = _dbContext
                    .Users
                    .FirstOrDefault(r => r.Username == user.Username && r.Email == user.Email && r.Password == user.Password)
                    .UserHash;

                return Ok(userHashFromData);
            }

            return BadRequest("Something went wrong");


        }
    }
}


using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using web_application_task.Dtos.User;
using web_application_task.Models;

namespace web_application_task.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthRepository(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<User>> Login(UserLoginDto usernameLogin)
        {
            var response = new ServiceResponse<User>();
            var user = await _context.Users.
                                FirstOrDefaultAsync(u => u.Email.ToLower().Equals(usernameLogin.Email.ToLower()));
            if (user is null)
            {
                response.Success = false;
                response.Message = "User Not Found";
            }
            else if (!VerifyPasswordHash(usernameLogin.Password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong Password";
            }
            else
            {
                response.Data = user;
            }
            return response;
        }

        public async Task<ServiceResponse<int>> Register(UserRegisterDto userRegisterDto)
        {
            User user = new User();
            user.Email = userRegisterDto.Email;
            var response = new ServiceResponse<int>();

            if (await UserExists(user.Email))
            {
                response.Success = false;
                response.Message = "User Already Exists.";
                return response;
            }


            CreatePasswordHash(userRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            UserProfile userProfile = new UserProfile { Id = user.Id, Email= user.Email, Name = userRegisterDto.Username, User=user };

            _context.Users.Add(user);
            _context.UserProfiles.Add(userProfile);
            await _context.SaveChangesAsync();

            response.Data = user!.Id;
            return response;
        }

        public async Task<bool> UserExists(string email)
        {
            if (await _context.Users.AnyAsync(u => u.Email.ToLower() == email))
            {
                return true;
            }
            return false;
        }

        private void CreatePasswordHash(string password,
                                        out byte[] passwordHash,
                                        out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password,
                                        byte[] passwordHash,
                                        byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash =
                    hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }

        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var appSettingsToken = _configuration.GetSection("AppSettings:Token").Value;

            if (appSettingsToken is null)
                throw new Exception("AppSetting Token is Null!");

            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(appSettingsToken));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}


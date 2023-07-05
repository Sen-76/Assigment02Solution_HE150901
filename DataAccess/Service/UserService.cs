using BusinessObject;
using BusinessObject.Model;
using BusinessObject.ViewModel;
using DataAccess.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly EStoreAPContext _context;
        private readonly IConfiguration _configuration;
        public UserService(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            EStoreAPContext eStoreAp, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = eStoreAp;
            _configuration = configuration;
        }
        private DateTime ConverUnixTimeToDateTime(long utcExpireDate)
        {
            var dateTImeInterval = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTImeInterval.AddSeconds(utcExpireDate).ToUniversalTime();
            return dateTImeInterval;
        }
        public Task<ApiResponse> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> GetList()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> Update(User newValue)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> Create(User newValue)
        {
            throw new NotImplementedException();
        }


        public async Task<ApiResponse> Regis(UserVM newValue)
        {
            try
            {
                var userExistMail = await _userManager.FindByEmailAsync(newValue.Email);
                if (userExistMail != null)
                {
                    return new ApiResponse()
                    {
                        Message = "User has been already existed!",
                        Success = false
                    };
                }
                var user = new User()
                {
                    Email = newValue.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = newValue.Email,
                };
                var resultCreateUser = await _userManager.CreateAsync(user, newValue.Password);

                if (!resultCreateUser.Succeeded)
                {
                    return new ApiResponse()
                    {
                        Message = "Error when create user",
                        Success = false
                    };
                }
                if (!await _roleManager.RoleExistsAsync("User"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("User"));
                }
                if (!await _roleManager.RoleExistsAsync("Admin"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                }
                if (await _roleManager.RoleExistsAsync("User"))
                {
                    await _userManager.AddToRoleAsync(user, "User");
                }

                return new ApiResponse()
                {
                    Success = true,
                    Message = "OK",
                    Data = newValue
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse()
                {
                    Success= false,
                    Message = ex.Message
                };
            }
        }

        public async Task<string> GenerateToken(UserVM user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>();

            if (user.Email.Equals(_configuration["AdminAccount:Username"]))
            {
                claims = new List<Claim>
                {
                    new (ClaimTypes.Email, user.Email),
                    new (ClaimTypes.Name, "Admin"),
                    new (ClaimTypes.Role, "Admin"),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
            }
            else
            {
                var userC = await _userManager.FindByEmailAsync(user.Email);
                claims = new List<Claim>()
                {
                    new(ClaimTypes.Name, userC.UserName),
                    new(ClaimTypes.Email, userC.Email),
                    new("UserId", userC.Id),
                    new(ClaimTypes.Role, "User"),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
            }

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: credentials
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            await _context.SaveChangesAsync();

            return accessToken;
        }

        public async Task<ApiResponse> Login(UserVM newValue)
        {
            try
            {
                if (newValue.Email.Equals(_configuration["AdminAccount:Username"]) && newValue.Password.Equals(_configuration["AdminAccount:Password"]))
                {
                    var token = await GenerateToken(newValue);
                    return new ApiResponse()
                    {
                        Success= true,
                        Data = token,
                        Message = "Welcome admin"
                    };
                }
                var userIdentity = await _userManager.FindByEmailAsync(newValue.Email);
                if (userIdentity != null && await _userManager.CheckPasswordAsync(userIdentity, newValue.Password))
                {
                    var token = await GenerateToken(newValue);
                    return new ApiResponse
                    {
                        Success= true,
                        Data = token,
                        Message = "Welcome user"
                    };
                }

                return new ApiResponse()
                {
                    Success= false,
                    Message = "Username or password is not correct"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse()
                {
                    Success= false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ApiResponse> GetUserById(string id)
        {
            try
            {
                var userEntity = await _userManager.FindByIdAsync(id.ToString());
                if (userEntity == null)
                    return new ApiResponse()
                    {
                        Success= false,
                        Message = "Can't find user"
                    };
                return new ApiResponse()
                {
                    Success= true,
                    Data = userEntity
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse()
                {
                    Success= false,
                    Message = ex.Message
                };
            }

        }
    }
}

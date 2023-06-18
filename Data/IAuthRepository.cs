using System;
using web_application_task.Dtos.User;
using web_application_task.Models;

namespace web_application_task.Data
{
	public interface IAuthRepository
	{
        Task<ServiceResponse<int>> Register(UserRegisterDto userRegisterDto);
        Task<ServiceResponse<User>> Login(UserLoginDto usernameLogin);
        Task<bool> UserExists(string username);
    }
}


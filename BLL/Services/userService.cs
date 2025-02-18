using DAL.Models;
using DAL.Repositories;

namespace BLL.Services;

public class UserService
{
    private readonly LoginRepository _loginRepository;

        public UserService(LoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public async Task<User?> AuthenticateUserAsync(string email, string password)
        {
            var user = await _loginRepository.GetUserByEmailAsync(email);
            if (user != null && user.Password == password)
            {
                return user;
            }
            return null;
        }
}

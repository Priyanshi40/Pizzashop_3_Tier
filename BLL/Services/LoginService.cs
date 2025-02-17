using DAL.Models;
using DAL.Repositories;
namespace BLL.Services;

public class LoginService
{
    private readonly LoginRepository _loginRepository;

        public LoginService(LoginRepository loginRepository)
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

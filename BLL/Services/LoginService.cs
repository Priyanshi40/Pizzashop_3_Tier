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
        public async Task<User?> GetUser(string email)
        {
            var user = await _loginRepository.GetUserByEmailAsync(email);
            return user;
        }
        public void UpdatePasswordService(User user, string newPassword)
        {
            _loginRepository.UpdatePassword(user, newPassword);
            
        }
        public async Task<User?> AuthenticateUserAsync(string email, string password)
        {
            var user = await _loginRepository.GetUserByEmailAsync(email);
            if ( user != null && user.Password == password || BCrypt.Net.BCrypt.EnhancedVerify(password, user.Password))
            {
                return user;
            }
            return null;
        }
}

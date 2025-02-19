using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DAL.Repositories;

public class LoginRepository
{
    private readonly PizzashopContext _context;
    public LoginRepository(PizzashopContext context)
    {
        _context = context;
    }
    public async Task<User?> GetUserByEmailAsync(string email)
   
    {
        // var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        // Console.WriteLine(user.Email);
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
    public void UpdatePassword(User user, string newPassword)
    {
        var pass = BCrypt.Net.BCrypt.EnhancedHashPassword(newPassword);
        user.Password = pass;
        // user.Password = newPassword;
        _context.SaveChanges();
    } 
}

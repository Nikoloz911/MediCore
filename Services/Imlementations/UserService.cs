using MediCore.Data;
using MediCore.Models;
using MediCore.Enums;
using MediCore.Services.Interaces;
using System.Security.Claims;

namespace MediCore.Services.Imlementations;
public class UserService : IUser
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    // GET ALL USERS
    public List<User> GetAllUsers()
    {
        var currentUserRole = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
        if (currentUserRole != USER_ROLE.ADMIN.ToString())
        {
            return null;  
        }
        else
        {
            return _context.Users.ToList();
        }   
    }
    // GET USER BY ID
    public User GetUserById(int id)
    {
        var foundUser = _context.Users.FirstOrDefault(u => u.Id == id);
        if (foundUser == null)
        {
            return null;
        }
        else
        {
            return foundUser;
        }
    }

    // UPDATE USER BY ID
    public User UpdateUserById(int id, User user)
    {
        var existingUser = _context.Users.FirstOrDefault(u => u.Id == id);
        if (existingUser == null)
        {
            return null;  
        }
        else
        {
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;  // 
            existingUser.Role = user.Role;          //
            existingUser.Status = user.Status;
            _context.SaveChanges();
            return existingUser;
        }
    }
    // DELETE USER BY ID
    public User DeleteUserById(int id)
    {
        var currentUserRole = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
        if (currentUserRole != USER_ROLE.ADMIN.ToString())
        {
            return null;
        }
        else
        {
            var userToDelete = _context.Users.FirstOrDefault(u => u.Id == id);
            if (userToDelete == null)
            {
                return null;
            }
            else
            {
                userToDelete.Status = USER_STATUS.INACTIVE;
                _context.SaveChanges();
                return userToDelete;
            }
        }
    }
}

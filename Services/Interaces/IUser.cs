using MediCore.Models;
namespace MediCore.Services.Interaces;
public interface IUser
{
    List<User> GetAllUsers(); // (ADMIN)
    User GetUserById(int id);
    User UpdateUserById(int id, User user);
    User DeleteUserById(int id); // (ADMIN)
}

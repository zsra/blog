using Blog.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Core.Interfaces.Services
{
    public interface IUserService
    {
        ValueTask<User> SignUp(string firstname, string lastname, string ageGroup, string username, string applicationUserId);
        ValueTask<User> GetUserById(int id);
        ValueTask<IEnumerable<User>> GetAll();
        ValueTask<User> WhoAmI(string applicationUserId);
    }
}

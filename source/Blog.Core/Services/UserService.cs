using Blog.Core.Entities;
using Blog.Core.Extensions;
using Blog.Core.Interfaces;
using Blog.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IAsyncRepository<User> _userRepository;

        public UserService(IAsyncRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async ValueTask<User> GetUserById(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async ValueTask<IEnumerable<User>> GetAll()
        {
            return await _userRepository.ListAllAsync();
        }

        public async ValueTask<User> SignUp(string firstname, string lastname, string ageGroup, string username, string applicationUserId)
        {
            _ = Enum.TryParse(ageGroup, out AgeGroup _ageGroup);
            User user = new()
            {
                FirstName = firstname,
                LastName = lastname,
                AgeGroup = _ageGroup,
                Username = username,
                ApplicationUserId = applicationUserId
            };

            return await _userRepository.AddAsync(user);
        }

        public async ValueTask<User> WhoAmI(string applicationUserId)
        {
            IReadOnlyList<User> users = await _userRepository.ListAllAsync();

            return users.FirstOrDefault(user => user.ApplicationUserId == applicationUserId);
        }
    }
}

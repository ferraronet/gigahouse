using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Interfaces.Repositories;
using GigaHouse.Infrastructure.Interfaces.Services;

namespace GigaHouse.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CreateAsync(User user, CancellationToken cancellationToken = default)
        {
            var userRepository = await _userRepository.GetByIdAsync(user.Id);

            if (userRepository == null)
            {
                user.CreatedAt = DateTime.Now;
                await _userRepository.CreateAsync(user, cancellationToken);
            }
            else
            {
                throw new Exception("User found");
            }

            return user;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var userRepository = await _userRepository.GetByIdAsync(id);

            if (userRepository != null)
                await _userRepository.DeleteAsync(userRepository);
            else
                return false;

            return true;
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var listUsers = await _userRepository.GetAllAsync();
            return listUsers.FirstOrDefault(f => f.Email == email);
        }

        public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var userRepository = await _userRepository.GetByIdAsync(id);
            return userRepository;
        }
    }
}

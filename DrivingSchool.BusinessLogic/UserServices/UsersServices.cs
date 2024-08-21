using DrivingSchool.Core.Enum;
using DrivingSchool.Core.Models;
using DrivingSchool.DataAccess.Repositories;
using DrivingSchool.Infrastructure;

namespace DrivingSchool.BusinessLogic.UserServices
{
    public class UsersServices : IUsersServices
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public UsersServices(IUsersRepository usersRepository,
            IPasswordHasher passwordHasher,
            IJwtProvider jwtProvider)
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            return await _usersRepository.GetUserAsync();
        }

        public async Task<UserModel> GetUsersByIdAsync(Guid userId)
        {
            return await _usersRepository.GetUserByIdAsync(userId);
        }

        public async Task<UserModel> GetUsersByIdAsync(string jwtToken)
        {
            var userId = _jwtProvider.GetUserIdFromToken(jwtToken);

            return await _usersRepository.GetUserByIdAsync(userId);
        }

        public async Task<Guid> CreateUserAsync(UserModel user)
        {
            return await _usersRepository.CreateAsync(user);
        }

        public async Task<string> RegisterUserAsync(string userName, string email, string password, RoleEnum role)
        {
            var (user, error) = UserModel.Create(Guid.NewGuid(), userName, email, password, role);

            if(!string.IsNullOrEmpty(error)) { return error; }

            await _usersRepository.CreateAsync(user);

            return error;
        }

        public async Task<(string token, string error)> LoginUserAsync(string email, string password)
        {
            string error = string.Empty;
            var user = await _usersRepository.GetByEmailAsync(email);

            var isVerify = _passwordHasher.Verify(password, user.Password);

            if (!isVerify)
            {
                error = "Failed to login";
            }

            var token = _jwtProvider.GenerateToken(user).ToString();

            return (token, error);
        }

        public async Task<Guid> UpdateUserAsync(Guid idUser, string userName, string email, string password, RoleEnum role)
        {
            return await _usersRepository.UpdateAsync(idUser, userName, email, password, role);
        }

        public async Task<UserModel> AddModeratorRole(Guid idUser)
        {
            return await _usersRepository.AddModerator(idUser);
        }        
        
        public async Task<UserModel> DeleteModeratorRole(Guid idUser)
        {
            return await _usersRepository.DeleteModerator(idUser);
        }

        public async Task<UserModel> UserNameChange(Guid idUser, string newUserName)
        {
            return await _usersRepository.UserNameChange(idUser, newUserName);
        }

        public async Task<Guid> DeleteUserAsync(Guid idUser)
        {
            return await _usersRepository.DeleteAsync(idUser);
        }
    }
}

using DrivingSchool.Core.Enum;
using DrivingSchool.Core.Models;
using DrivingSchool.DataAccess.Entities;
using DrivingSchool.Infrastructure;
using DrivingSchool.Infrastructure.CustomException;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.DataAccess.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DrivingSchoolDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public UsersRepository(DrivingSchoolDbContext context,
            IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<Guid> CreateAsync(UserModel user)
        {
            Console.WriteLine(user.Email);
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == user.Email);

            if (existingUser == null)
            {
                var userEntity = new UserEntity
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = _passwordHasher.Generate(user.Password),
                    Role = user.Role,
                };

                await _context.Users.AddAsync(userEntity);
                await _context.SaveChangesAsync();

                return userEntity.Id;
            }
            throw new CustomException("Пользователь с такой почтой уже есть");
        }

        public async Task<List<UserModel>> GetUsersAsync()
        {
            var usersEntities = await _context.Users
                .AsNoTracking()
                .Include(u => u.AnswerUserTests)
                .ThenInclude(a => a.Test)
                .ThenInclude(t => t.Category)
                .ToListAsync();

            var users = usersEntities.Select(u =>
                UserModel.Create(
                    u.Id,
                    u.AnswerUserTests.Select(a => AnswerUserTestModel.Create(a.Id, 
                        TestModel.Create(a.Test.Id,
                            CategoryModel.Create(a.Test.Category.Id, a.Test.Category.NameCategory).category,
                        a.Test.NameTest).test, a.ResultTest).answer).TakeLast(5).ToList(),
                    u.UserName,
                    u.Email,
                    u.Password,
                    u.Role
                ).user
            ).ToList();

            return users;
        }

        public async Task<UserModel> GetUserByIdAsync(Guid id)
        {
            var usersEntities = await _context.Users
                .Include(u => u.AnswerUserTests)
                .ThenInclude(a => a.Test)
                .ThenInclude(t => t.Category)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usersEntities != null)
            {

                var user = UserModel.Create(usersEntities.Id, usersEntities.AnswerUserTests
                    .Select(a => AnswerUserTestModel.Create(a.Id,
                        TestModel.Create(a.Test.Id,
                            CategoryModel.Create(a.Test.Category.Id, a.Test.Category.NameCategory).category,
                        a.Test.NameTest).test, a.ResultTest).answer).TakeLast(5).ToList(),
                    usersEntities.UserName,
                    usersEntities.Email,
                    usersEntities.Password,
                    usersEntities.Role).user;

                return user;
            }
            throw new CustomException("Не удалось найти пользователя");
        }

        public async Task<UserModel> GetByEmailAsync(string email)
        {
            var usersEntities = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception();

            if (usersEntities != null)
            {
                var user = UserModel.Create(usersEntities.Id,
                    usersEntities.UserName,
                    usersEntities.Email,
                    usersEntities.Password,
                    usersEntities.Role).user;

                return user;
            }
            throw new CustomException("Не удалось найти пользователя");
        }

        public async Task<Guid> UpdateAsync(Guid idUser, string username, string email, string password, RoleEnum role)
        {
            await _context.Users
               .Where(u => u.Id == idUser)
               .ExecuteUpdateAsync(u => u
                   .SetProperty(u => u.UserName, username)
                   .SetProperty(u => u.Email, email)
                   .SetProperty(u => u.Password, password)
                   .SetProperty(u => u.Role, role)
               );

            return idUser;
        }

        public async Task<UserModel> AddModerator(Guid idUser)
        {
            var userEntitie = await _context.Users
                .Include(u => u.AnswerUserTests)
                .ThenInclude(a => a.Test)
                .ThenInclude(t => t.Category)
                .FirstOrDefaultAsync(u => u.Id == idUser); ;

            if (userEntitie != null) 
            { 
                userEntitie.Role = RoleEnum.Moderator;
                await _context.SaveChangesAsync();

                var user = UserModel.Create(userEntitie.Id, userEntitie.AnswerUserTests
                    .Select(a => AnswerUserTestModel.Create(a.Id,
                        TestModel.Create(a.Test.Id,
                            CategoryModel.Create(a.Test.Category.Id, a.Test.Category.NameCategory).category,
                        a.Test.NameTest).test, a.ResultTest).answer).ToList(),
                    userEntitie.UserName,
                    userEntitie.Email,
                    userEntitie.Password,
                    userEntitie.Role).user;

                return user;
            }

            throw new CustomException("Ошибка выдачи роли Moderator");
        }

        public async Task<UserModel> DeleteModerator(Guid idUser)
        {
            var userEntitie = await _context.Users
                .Include(u => u.AnswerUserTests)
                .ThenInclude(a => a.Test)
                .ThenInclude(t => t.Category)
                .FirstOrDefaultAsync(u => u.Id == idUser); ;

            if (userEntitie != null)
            {
                userEntitie.Role = RoleEnum.User;
                await _context.SaveChangesAsync();

                var user = UserModel.Create(userEntitie.Id, userEntitie.AnswerUserTests
                    .Select(a => AnswerUserTestModel.Create(a.Id,
                        TestModel.Create(a.Test.Id,
                            CategoryModel.Create(a.Test.Category.Id, a.Test.Category.NameCategory).category,
                        a.Test.NameTest).test, a.ResultTest).answer).ToList(),
                    userEntitie.UserName,
                    userEntitie.Email,
                    userEntitie.Password,
                    userEntitie.Role).user;

                return user;
            }

            throw new CustomException("Ошибка удаления роли Moderator");
        }

        public async Task<UserModel> UserNameChange(Guid idUser, string newUserName)
        {
            var userEntitie = await _context.Users
                .Include(u => u.AnswerUserTests)
                .ThenInclude(a => a.Test)
                .ThenInclude(t => t.Category)
                .FirstOrDefaultAsync(u => u.Id == idUser); ;

            if (userEntitie != null)
            {
                userEntitie.UserName = newUserName;
                await _context.SaveChangesAsync();

                var user = UserModel.Create(userEntitie.Id, userEntitie.AnswerUserTests
                    .Select(a => AnswerUserTestModel.Create(a.Id,
                        TestModel.Create(a.Test.Id,
                            CategoryModel.Create(a.Test.Category.Id, a.Test.Category.NameCategory).category,
                        a.Test.NameTest).test, a.ResultTest).answer).ToList(),
                    userEntitie.UserName,
                    userEntitie.Email,
                    userEntitie.Password,
                    userEntitie.Role).user;

                return user;
            }

            throw new CustomException("Ошибка изменения имени пользователя");
        }

        public async Task<Guid> DeleteAsync(Guid idUser)
        {
            await _context.Users
                .Where(u => u.Id == idUser)
                .ExecuteDeleteAsync();

            return idUser;
        }
    }
}

using DrivingSchool.Core.Enum;
using DrivingSchool.Core.Models;
using DrivingSchool.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.DataAccess.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DrivingSchoolDbContext _context;
        public UsersRepository(DrivingSchoolDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(UserModel user)
        {
            var userEntity = new UserEntity
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role,
            };

            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();

            return userEntity.Id;
        }

        public async Task<List<UserModel>> GetUserAsync()
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
                        a.Test.NameTest).test, a.ResultTest).answer).ToList(),
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

            var user = UserModel.Create(usersEntities.Id, usersEntities.AnswerUserTests
                .Select(a => AnswerUserTestModel.Create(a.Id,
                    TestModel.Create(a.Test.Id,  
                        CategoryModel.Create(a.Test.Category.Id, a.Test.Category.NameCategory).category,
                    a.Test.NameTest).test, a.ResultTest).answer).TakeLast(5).ToList(), 
                usersEntities.UserName, 
                usersEntities.Email,
                usersEntities.Password, 
                usersEntities.Role).user;

            foreach(var answer in usersEntities.AnswerUserTests)
            {
                Console.WriteLine(answer);
            }

            return user;
        }

        public async Task<UserModel> GetByEmailAsync(string email)
        {
            var usersEntities = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception();

            var user = UserModel.Create(usersEntities.Id, usersEntities.UserName, usersEntities.Email, usersEntities.Password, usersEntities.Role).user;

            return user;
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

            throw new Exception("Ошибка выдачи роли Moderator");
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

            throw new Exception("Ошибка удаления роли Moderator");
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

            throw new Exception("Ошибка изменения имени пользователя");
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

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

        public async Task<List<UserModel>> GetAsync()
        {
            var usersEntities = await _context.Users
                .AsNoTracking()
                .Include(u => u.AnswerUserTests)
                .ThenInclude(a => a.Test)
                .ToListAsync();

            var users = usersEntities.Select(u =>
                UserModel.Create(
                    u.Id,
                    u.AnswerUserTests.Select(a => AnswerUserTestModel.Create(a.Id, TestModel.Create(a.Test.Id, a.Test.NameTest).test, a.ResultTest).answer).ToList(),
                    u.UserName,
                    u.Email,
                    u.Password,
                    u.Role
                ).user
            ).ToList();

            return users;
        }

        public async Task<UserModel> GetByIdAsync(Guid id)
        {
            var usersEntities = await _context.Users.FindAsync(id);

            var user = UserModel.Create(usersEntities.Id, usersEntities.UserName, usersEntities.Email, usersEntities.Password, usersEntities.Role).user;

            return user;
        }

        public async Task<Guid> UpdateAsync(Guid idUser, string username, string email, string password, string role)
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

        public async Task<Guid> DeleteAsync(Guid idUser)
        {
            await _context.Users
                .Where(u => u.Id == idUser)
                .ExecuteDeleteAsync();

            return idUser;
        }
    }
}

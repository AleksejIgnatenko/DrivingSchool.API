﻿using DrivingSchool.Core.Abstractions;
using DrivingSchool.Core.Models;
using DrivingSchool.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingSchool.DataAccess.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DrivingSchoolDbContext _context;
        public UsersRepository(DrivingSchoolDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Create(UserModel user)
        {
            var userEntity = new UserEntity
            {
                IdUser = user.IdUser,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role,
            };

            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();

            return userEntity.IdUser;
        }

        public async Task<List<UserModel>> Get()
        {
            var usersEntities = await _context.Users
                .AsNoTracking()
                .ToListAsync();

            var users = usersEntities
                .Select(u => UserModel.Create(u.IdUser, u.UserName, u.Email, u.Password, u.Role).user)
                .ToList();

            return users;
        }

        public async Task<Guid> Update(Guid idUser, string username, string email, string password, string role)
        {
            await _context.Users
               .Where(u => u.IdUser == idUser)
               .ExecuteUpdateAsync(u => u
                   .SetProperty(u => u.UserName, username)
                   .SetProperty(u => u.Email, email)
                   .SetProperty(u => u.Password, password)
                   .SetProperty(u => u.Role, role)
               );

            return idUser;
        }

        public async Task<Guid> Delete(Guid idUser)
        {
            await _context.Users
                .Where(u => u.IdUser == idUser)
                .ExecuteDeleteAsync();

            return idUser;
        }
    }
}

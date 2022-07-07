using Microsoft.EntityFrameworkCore;
using SocialeNet.Core.Domain.Entities;
using SocialNet.Core.Application.Helpers;
using SocialNet.Core.Application.Interfaces.Repository;
using SocialNet.Core.Application.ViewModels.User;
using SocialNet.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNet.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly SocialNetContext _dbContext;
        public UserRepository(SocialNetContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public override async Task<User> AddAsync(User entity)
        {
            entity.Password = PasswordEncryptation.ComputeSha256(entity.Password);
            await base.AddAsync(entity);
            return entity;
        }
        public async Task<User> LoginAsync(LoginViewModel loginVm)
        {
            string PasswordEncrypt = PasswordEncryptation.ComputeSha256(loginVm.Password);
            User user = await _dbContext.Set<User>().FirstOrDefaultAsync(user=> user.UserName == loginVm.UserName && user.Password == PasswordEncrypt);
            return user;
        }


    }
}

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
    public class UserRepository : GenericRepository<Users>, IUserRepository
    {
        private readonly SocialNetContext _dbContext;
        public UserRepository(SocialNetContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public override async Task<Users> AddAsync(Users entity)
        {
            entity.Password = PasswordEncryptation.ComputeSha256(entity.Password);
            await base.AddAsync(entity);
            return entity;
        }
        public async Task<Users> LoginAsync(LoginViewModel loginVm)
        {
            string PasswordEncrypt = PasswordEncryptation.ComputeSha256(loginVm.Password);
            Users user = await _dbContext.Set<Users>().FirstOrDefaultAsync(user=> user.UserName == loginVm.UserName && user.Password == PasswordEncrypt);
            return user;
        }
        public async Task<Users> ActivateUserAsync(ConfirmUserViewModel confirmVm)
        {
            Users user = await _dbContext.Set<Users>().FirstOrDefaultAsync(user => user.UserName == confirmVm.UserName && user.ConfirmationCode == confirmVm.ConfirmationCode);
            return user;
        }
        public async Task<Users> FindUserNameAsync(SaveUserViewModel vm)
        {
            Users user = await _dbContext.Set<Users>().FirstOrDefaultAsync(user => user.UserName == vm.UserName);
            return user;
        }


    }
}

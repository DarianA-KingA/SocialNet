using SocialeNet.Core.Domain.Entities;
using SocialNet.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNet.Core.Application.Interfaces.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> LoginAsync(LoginViewModel loginVm);
    }
}

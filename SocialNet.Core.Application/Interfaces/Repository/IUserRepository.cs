using SocialeNet.Core.Domain.Entities;
using SocialNet.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNet.Core.Application.Interfaces.Repository
{
    public interface IUserRepository : IGenericRepository<Users>
    {
        Task<Users> LoginAsync(LoginViewModel loginVm);
        Task<Users> ActivateUserAsync(ConfirmUserViewModel confirmVm);
        Task<Users> FindUserNameAsync(SaveUserViewModel vm);

    }
}

using SocialeNet.Core.Domain.Entities;
using SocialNet.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNet.Core.Application.Interfaces.Service
{
    public interface IUserService : IGenericService<SaveUserViewModel, UserViewModel, Users>
    {
        Task<UserViewModel> Login(LoginViewModel vm);
        Task<List<UserViewModel>> GetAllViewModelWithInclude();
        Task<UserViewModel> ActivateUSer(ConfirmUserViewModel vm);
        Task<Users> FindUserName(SaveUserViewModel vm);

    }
}


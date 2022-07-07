using SocialeNet.Core.Domain.Entities;
using SocialNet.Core.Application.ViewModels.Friend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNet.Core.Application.Interfaces.Service
{
    public interface IFriendService : IGenericService<SaveFriendViewModel, FriendViewModel, Friends>
    {
        Task<List<FriendViewModel>> GetAll();
    }
}

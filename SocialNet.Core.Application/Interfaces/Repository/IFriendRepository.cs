using SocialeNet.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNet.Core.Application.Interfaces.Repository
{
    public interface IFriendRepository: IGenericRepository<Friend>
    {
        Task<List<Friend>> GetAllAsync(int id);
    }
}

using SocialeNet.Core.Domain.Entities;
using SocialNet.Core.Application.Interfaces.Repository;
using SocialNet.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNet.Infrastructure.Persistence.Repositories
{
    public class FriendRepository : GenericRepository<Friend>,IFriendRepository
    {
        private readonly SocialNetContext _dbContext;

        public FriendRepository(SocialNetContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public  async Task<List<Friend>> GetAllAsync(int id)
        {
            var Friend = await base.GetAllAsync();
            return Friend.Where(friend => friend.FromId == id || friend.ToId == id).ToList();
        }
    }
}

﻿using SocialeNet.Core.Domain.Entities;
using SocialNet.Core.Application.Interfaces.Repository;
using SocialNet.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNet.Infrastructure.Persistence.Repositories
{
    public class FriendRepository : GenericRepository<Friends>,IFriendRepository
    {
        private readonly SocialNetContext _dbContext;

        public FriendRepository(SocialNetContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public  async Task<List<Friends>> GetAllWithUser()
        {
             
            return await base.GetAllAsync();
        }
    }
}

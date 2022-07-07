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
    public class ComentaryRepository : GenericRepository<Comentary>, IComentaryRepository
    {
        private readonly SocialNetContext _dbContext;

        public ComentaryRepository(SocialNetContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}

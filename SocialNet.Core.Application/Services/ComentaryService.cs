using AutoMapper;
using SocialeNet.Core.Domain.Entities;
using SocialNet.Core.Application.Interfaces.Repository;
using SocialNet.Core.Application.Interfaces.Service;
using SocialNet.Core.Application.ViewModels.Commentary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNet.Core.Application.Services
{
    public class ComentaryService : GenericService<SaveComentaryViewModel, ComentaryViewModel, Comentary>, IComentaryService
    {
        private readonly IComentaryRepository _comentaryRepository;
        private readonly IMapper _mapper;

        public ComentaryService(IComentaryRepository comentaryRepository, IMapper mapper) : base(comentaryRepository, mapper)
        {
            _comentaryRepository = comentaryRepository;
            _mapper = mapper;
        }
    }
}

using AutoMapper;
using SocialeNet.Core.Domain.Entities;
using SocialNet.Core.Application.Interfaces.Repository;
using SocialNet.Core.Application.Interfaces.Service;
using SocialNet.Core.Application.ViewModels.Publication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNet.Core.Application.Services
{
    public class PublicationService :  GenericService<SavePublicationViewModel, PublicationViewModel, Publications>, IPublicationService
    {
        private readonly IPublicationRepository _publicationRepository;
        private readonly IMapper _mapper;

        public PublicationService(IPublicationRepository publicationRepository,IMapper mapper) : base(publicationRepository, mapper)
        {
            _publicationRepository = publicationRepository;
            _mapper = mapper;
        }
        
        public async Task<List<PublicationViewModel>> GetAllAsyncByUser(int id)
        {
            var publication = await _publicationRepository.GetAllAsync();
            var filter= publication.Where(publication => publication.UserId == id).ToList();
            return _mapper.Map<List<PublicationViewModel>>(filter);
        }

        public async Task<List<PublicationViewModel>> GetAllAsyncByFriend(List<int> idFriends, int idUSer)
        {
            var publication = await _publicationRepository.GetAllAsync();
            List<Publications> publications = publication.Where(publication => publication.UserId != idUSer).ToList();//filtra las publicaciones del usuario
            var filter = publication;//creo una variable para filtrar la lista
            foreach (var item in filter)
            {
                filter.Remove(item);//vacio la variable para retornarla filtrada
            }
            foreach (int id in idFriends)//navego cada id que se me pasa para traer su publicacion
            {
                var select = publication.Where(p => p.UserId == id).ToList();//aqui esta nueva variable contiene la publicacion de ese Id especifico
                foreach (var item in select)
                {
                    filter.Add(item);//se agrega cada publicacin de ese Id a la lista de filtrado;
                }
            }
            return _mapper.Map<List<PublicationViewModel>>(filter);//se manda la lista de pubblicaciones totalmente filtrada 
        } 
    }
}

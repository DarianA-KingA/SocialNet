using AutoMapper;
using Microsoft.AspNetCore.Http;
using SocialeNet.Core.Domain.Entities;
using SocialNet.Core.Application.Helpers;
using SocialNet.Core.Application.Interfaces.Repository;
using SocialNet.Core.Application.Interfaces.Service;
using SocialNet.Core.Application.ViewModels.Publication;
using SocialNet.Core.Application.ViewModels.User;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel userViewModel;

        public PublicationService(IPublicationRepository publicationRepository,IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(publicationRepository, mapper)
        {
            _publicationRepository = publicationRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public async Task<List<PublicationViewModel>> GetAllAsyncByUser()
        {
            var publication = await _publicationRepository.GetAllAsync();
            var filter= publication.Where(publication => publication.UserId == userViewModel.Id).ToList();
            return _mapper.Map<List<PublicationViewModel>>(filter);
        }

        public async Task<List<PublicationViewModel>> GetAllAsyncByFriend(List<int> idFriends)
        {
            var publication = await _publicationRepository.GetAllAsync();
            List<Publications> publications = publication.Where(publication => publication.UserId != userViewModel.Id).ToList();//filtra las publicaciones del usuario
            List<Publications> FriendsPublications = new List<Publications>();
            foreach (int id in idFriends)//navego cada id que se me pasa para traer su publicacion
            {
                var select = publication.Where(p => p.UserId == id).ToList();//aqui esta nueva variable contiene la publicacion de ese Id especifico
                foreach (var item in select)
                {
                    FriendsPublications.Add(item);//se agrega cada publicacin de ese Id a la lista de filtrado;
                }
            }
            var filtredPublication=  _mapper.Map<List<PublicationViewModel>>(FriendsPublications).ToList();//se manda la lista de pubblicaciones totalmente filtrada
            return filtredPublication;
        } 
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Http;
using SocialeNet.Core.Domain.Entities;
using SocialNet.Core.Application.Helpers;
using SocialNet.Core.Application.Interfaces.Repository;
using SocialNet.Core.Application.Interfaces.Service;
using SocialNet.Core.Application.ViewModels.Friend;
using SocialNet.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNet.Core.Application.Services
{
    public class FriendService : GenericService<SaveFriendViewModel, FriendViewModel, Friends>, IFriendService
    {
        private readonly IFriendRepository _friendRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel userViewModel;

        public FriendService(IFriendRepository friendRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(friendRepository, mapper)
        {
            _friendRepository = friendRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }
        public async Task<List<FriendViewModel>> GetAll()
        {
            var Friend = await _friendRepository.GetAllWithUser();
            Friend =Friend.Where(friend => friend.FromId == userViewModel.Id || friend.ToId ==userViewModel.Id).ToList();
            var filtredFriend = _mapper.Map<List<FriendViewModel>>(Friend);
            return filtredFriend.ToList() ;
        }
    }   

}

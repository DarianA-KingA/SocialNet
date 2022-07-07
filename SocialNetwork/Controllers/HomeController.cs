using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNet.Core.Application.Interfaces.Service;
using SocialNet.Core.Application.ViewModels.Commentary;
using SocialNet.Core.Application.ViewModels.Friend;
using SocialNet.Core.Application.ViewModels.Publication;
using SocialNet.Core.Application.ViewModels.User;
using SocialNetwork.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPublicationService _publicationService;
        private readonly IComentaryService _comentaryService;
        private readonly IFriendService _friendService;
        private readonly IUserService _userService;
        public HomeController(IPublicationService publicationService, IComentaryService comentaryService, IFriendService friendService, IUserService userService)
        {
            _publicationService = publicationService;
            _comentaryService = comentaryService;
            _friendService = friendService;
            _userService = userService;
        }

        public async Task<IActionResult>  Index()
        {
            var users = await _userService.GetAllViewModel();
            var friend =  await _friendService.GetAll();
            List<int> idFriend = new List<int>();
            foreach (var id in friend)
            {
                idFriend.Add(id.FromId);
                idFriend.Add(id.ToId);
            }
            var publication = await _publicationService.GetAllAsyncByFriend(idFriend);
            var comentaries = await _comentaryService.GetAllViewModel();
            List<ComentaryViewModel> coment = new List<ComentaryViewModel>();
            foreach (var item in comentaries)
            {
                ComentaryViewModel obj = new ComentaryViewModel();
                obj.Id = item.Id;
                obj.PublicationId = item.PublicationId;
                obj.UserId = item.UserId;
                obj.UserComentary = item.UserComentary;
                obj.UserOwner = await _userService.GetByIdSaveViewModel(item.UserId);
                coment.Add(obj);
            }
            var filtredPublication = publication.Select(publication => new PublicationViewModel { 
                Id=publication.Id,
                UserId = publication.UserId,
                Body = publication.Body,
                ImageUrl = publication.ImageUrl,
                ComentaryPublication = coment.Where(comentary=>comentary.PublicationId == publication.Id).ToList()

            }).ToList();
            List<UserViewModel> userFriends = new List<UserViewModel>();
            foreach (var item in friend)
            {
                var user = users.Where(user => user.Id == item.FromId || user.Id == item.ToId);
                foreach (var obj in user)
                {
                    userFriends.Add(obj);
                }
            }
            ViewBag.Users = userFriends;
            return View(filtredPublication);
        }

    }
}

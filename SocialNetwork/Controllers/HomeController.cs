using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNet.Core.Application.Helpers;
using SocialNet.Core.Application.Interfaces.Service;
using SocialNet.Core.Application.ViewModels.Commentary;
using SocialNet.Core.Application.ViewModels.Friend;
using SocialNet.Core.Application.ViewModels.Publication;
using SocialNet.Core.Application.ViewModels.User;
using SocialNetwork.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApp.StockApp.Middlewares;

namespace SocialNetwork.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPublicationService _publicationService;
        private readonly IComentaryService _comentaryService;
        private readonly IFriendService _friendService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel userViewModel;
        private readonly ValidateUserSession _validateUserSession;
        public HomeController(IPublicationService publicationService, IComentaryService comentaryService, IFriendService friendService, IUserService userService, IHttpContextAccessor httpContextAccessor, ValidateUserSession validateUserSession)
        {
            _publicationService = publicationService;
            _comentaryService = comentaryService;
            _friendService = friendService;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult>  Index()
        {
            SavePublicationViewModel vm = new();
            vm.Users = await _userService.GetAllViewModel(); //<--

            var friend =  await _friendService.GetAll();//get all friends
            List<int> idFriend = new List<int>();//instance for id in the friend list 
            foreach (var id in friend)//set all the id to explore and filter the list
            {
                idFriend.Add(id.FromId);
                idFriend.Add(id.ToId);
            }

            var publication = await _publicationService.GetAllAsyncByFriend(idFriend); // get all the publication fo your friends, without your own publications
            var comentaries = await _comentaryService.GetAllViewModel();//get all the comentary
            List<ComentaryViewModel> coment = new List<ComentaryViewModel>();//instace for list the comentary and filter them 
            foreach (var item in comentaries)// this loop is to fill up the the userOwner, it's and atribute used to set the userImage, the UserName, etc to the comentary
            {
                ComentaryViewModel obj = new ComentaryViewModel();
                obj.Id = item.Id;
                obj.PublicationId = item.PublicationId;
                obj.UserId = item.UserId;
                obj.UserComentary = item.UserComentary;
                obj.UserOwner = await _userService.GetByIdSaveViewModel(item.UserId);
                coment.Add(obj);
            }
            List<PublicationViewModel> filtredPublication = new();
            foreach (var item in publication)
            {
                PublicationViewModel obj = new();
                obj.Id = item.Id;
                obj.UserId = item.UserId;
                obj.Body = item.Body;
                obj.ImageUrl = item.ImageUrl;
                obj.ComentaryPublication = coment.Where(comentary => comentary.PublicationId == item.Id).ToList();
                obj.OwnerPublication = await _userService.GetByIdSaveViewModel(item.UserId);
                filtredPublication.Add(obj);
            }
            /*
            var filtredPublication = publication.Select(publication => new PublicationViewModel { //instance of a varible, is the same list but, here, we fill up the list of comentaries of the publication
                Id=publication.Id,
                UserId = publication.UserId,
                Body = publication.Body,
                ImageUrl = publication.ImageUrl,
                ComentaryPublication = coment.Where(comentary=>comentary.PublicationId == publication.Id).ToList(),
                OwnerPublication = await _userService.GetByIdSaveViewModel(publication.UserId)
            }).ToList();
            */
            List<UserViewModel> userFriends = new List<UserViewModel>();//list to get the users 
            foreach (var item in friend)//in this loop, i gget all the users id from the table friend
            {
                var user = vm.Users.Where(user => user.Id == item.FromId || user.Id == item.ToId);//filter the user with the id from friends
                foreach (var obj in user)
                {
                    userFriends.Add(obj);//add the user to the list of friends
                }
            }
            var friends = userFriends.Where(user => user.Id != userViewModel.Id).ToList();
            vm.Friends = friends;
            foreach (var item in vm.Friends)
            { 
                vm.Users = vm.Users.Where(user => user.Id != item.Id && user.Id != userViewModel.Id).ToList();
            }
            vm.Publications = filtredPublication.Where(publication => publication.UserId!= userViewModel.Id).ToList();
            
            return View(vm);
        }
        public async Task<IActionResult> AddPublication(SavePublicationViewModel vm)
        {
            vm.UserId = userViewModel.Id;
            SavePublicationViewModel publation = await _publicationService.Add(vm);
            if (publation.Id != 0 && publation != null)
            {
                publation.ImageUrl = UploadFile(vm.File, publation.Id);

                await _publicationService.Update(publation, publation.Id);
            }

            return RedirectToRoute(new { controller = "User", action = "Index" });
        }
        public async Task<IActionResult> UpdatePublication(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            SavePublicationViewModel vm = await _publicationService.GetByIdSaveViewModel(id);
            return View("SavePublication",vm);
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePublication(SavePublicationViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View("SavePublication", vm);
                
            }
            SavePublicationViewModel publication = await _publicationService.GetByIdSaveViewModel(vm.Id);
            vm.ImageUrl = UploadFile(vm.File, vm.Id, true, publication.ImageUrl);
            vm.UserId = userViewModel.Id;
            await _publicationService.Update(vm, vm.Id);
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
        public async Task<IActionResult> DeletePublication(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            await _publicationService.Delete(id);

            string basePath = $"/Images/Products/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new(path);

                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo folder in directory.GetDirectories())
                {
                    folder.Delete(true);
                }

                Directory.Delete(path);
            }

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }


        public IActionResult AddFriend(int id)
        {
            SaveFriendViewModel friend = new();
            friend.FromId = userViewModel.Id;
            friend.ToId = id;
            _friendService.Add(friend);
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }
        public async Task<IActionResult>  DeleteFriend(int id)
        {
            var friend =  await _friendService.GetAll();
            var relationShip = friend.Where(f =>f.FromId == userViewModel.Id && f.ToId == id).ToList();
            if (relationShip == null || relationShip.Count == 0)
            {
                relationShip = friend.Where(f =>f.FromId == id && f.ToId==userViewModel.Id).ToList();
            }
            int idFriendship=0;
            foreach (var item in relationShip)
            {
                idFriendship = item.Id;
            }
             await _friendService.Delete(idFriendship);
            return RedirectToRoute(new { controller = "User", action = "Index" });

        }
        public async Task<IActionResult> MyPublication()
        {
            SavePublicationViewModel vm = new();
            vm.Users = await _userService.GetAllViewModel(); //<--

            var friend = await _friendService.GetAll();//get all friends
            List<int> idFriend = new List<int>();//instance for id in the friend list 
            foreach (var id in friend)//set all the id to explore and filter the list
            {
                idFriend.Add(id.FromId);
                idFriend.Add(id.ToId);
            }

            var publication = await _publicationService.GetAllAsyncByFriend(idFriend); // get all the publication fo your friends, without your own publications
            var comentaries = await _comentaryService.GetAllViewModel();//get all the comentary
            List<ComentaryViewModel> coment = new List<ComentaryViewModel>();//instace for list the comentary and filter them 
            foreach (var item in comentaries)// this loop is to fill up the the userOwner, it's and atribute used to set the userImage, the UserName, etc to the comentary
            {
                ComentaryViewModel obj = new ComentaryViewModel();
                obj.Id = item.Id;
                obj.PublicationId = item.PublicationId;
                obj.UserId = item.UserId;
                obj.UserComentary = item.UserComentary;
                obj.UserOwner = await _userService.GetByIdSaveViewModel(item.UserId);
                coment.Add(obj);
            }
            List<PublicationViewModel> filtredPublication = new();
            foreach (var item in publication)
            {
                PublicationViewModel obj = new();
                obj.Id = item.Id;
                obj.UserId = item.UserId;
                obj.Body = item.Body;
                obj.ImageUrl = item.ImageUrl;
                obj.ComentaryPublication = coment.Where(comentary => comentary.PublicationId == item.Id).ToList();
                obj.OwnerPublication = await _userService.GetByIdSaveViewModel(item.UserId);
                filtredPublication.Add(obj);
            }
            /*
            var filtredPublication = publication.Select(publication => new PublicationViewModel { //instance of a varible, is the same list but, here, we fill up the list of comentaries of the publication
                Id=publication.Id,
                UserId = publication.UserId,
                Body = publication.Body,
                ImageUrl = publication.ImageUrl,
                ComentaryPublication = coment.Where(comentary=>comentary.PublicationId == publication.Id).ToList(),
                OwnerPublication = await _userService.GetByIdSaveViewModel(publication.UserId)
            }).ToList();
            */
            List<UserViewModel> userFriends = new List<UserViewModel>();//list to get the users 
            foreach (var item in friend)//in this loop, i gget all the users id from the table friend
            {
                var user = vm.Users.Where(user => user.Id == item.FromId || user.Id == item.ToId);//filter the user with the id from friends
                foreach (var obj in user)
                {
                    userFriends.Add(obj);//add the user to the list of friends
                }
            }
            var friends = userFriends.Where(user => user.Id != userViewModel.Id).ToList();
            vm.Friends = friends;
            foreach (var item in vm.Friends)
            {
                vm.Users = vm.Users.Where(user => user.Id != item.Id && user.Id != userViewModel.Id).ToList();
            }
            vm.Publications = filtredPublication.Where(publication => publication.UserId == userViewModel.Id).ToList();

            return View(vm);
        }

        private string UploadFile(IFormFile file, int id, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode)
            {
                if (file == null)
                {
                    return imagePath;
                }
            }
            if (file == null)
            {
                return null;
            }
            string basePath = $"/Images/Publication/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //create folder if not exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //get file extension
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldImagePart = imagePath.Split("/");
                string oldImagePath = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImagePath);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }
            return $"{basePath}/{fileName}";
        }


    }
}

using Microsoft.AspNetCore.Mvc;
using SocialNet.Core.Application.Interfaces.Service;
using SocialNet.Core.Application.ViewModels.User;
using System.Threading.Tasks;
using WebApp.StockApp.Middlewares;
using SocialNet.Core.Application.Helpers;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using SocialeNet.Core.Application.DTOs.Email;

namespace SocialNetwork.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ValidateUserSession _validateUserSession;
        private readonly IEmailService _emailService;


        public UserController(IUserService userService, ValidateUserSession validateUserSession, IEmailService emailService)
        {
            _userService = userService;
            _validateUserSession = validateUserSession;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            UserViewModel userVm = await _userService.Login(vm);
            if (userVm != null)
            {
                if (userVm.status)
                {
                    HttpContext.Session.Set<UserViewModel>("user", userVm);
                    return RedirectToRoute(new { controller = "Home", action = "Index" });
                }
                else
                {
                    ModelState.AddModelError("userValidation", "Debe activar el usuario");
                }
            }
            else
            {
                ModelState.AddModelError("userValidation", "Datos de acceso incorrecto");
            }

            return View(vm);
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public IActionResult Register()
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View(new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            var FindUser = await _userService.FindUserName(vm);
            if (FindUser == null)
            {
                SaveUserViewModel UserVm = await _userService.Add(vm);
                if (UserVm.Id != 0 && UserVm != null)
                {
                    UserVm.ImageUrl = UploadFile(vm.File, UserVm.Id);

                    await _userService.Update(UserVm, UserVm.Id);
                }

                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            else
            { 
                ModelState.AddModelError("userValidation", "Ese usuario ya existe");
            }
            return View(vm);
        }
        public IActionResult UpdatePassword()
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View(new UpdatePasswordUserViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordUserViewModel vm)
        {
            SaveUserViewModel user = new();
            user.UserName = vm.UserName;
            var FindUser = await _userService.FindUserName(user);
            if (FindUser == null)
            {
                ModelState.AddModelError("userValidation", "Ese usuario no existe");
                return View(vm);
            }
            else
            {
                user = await _userService.GetByIdSaveViewModel(FindUser.Id);
                user.Password = vm.Password;
                await _userService.Update(user, user.Id);

                await _emailService.SendAsync(new EmailRequest
                {
                    To = user.Email,
                    From = _emailService._mailSettings.EmailFrom,
                    Body = $"Usuario {user.UserName} su nueva password es:{user.Password}",
                    Subject = "Password reset"
                });

                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

        }
        public IActionResult ActivateUser()
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View(new ConfirmUserViewModel());
        }
        [HttpPost]

        public async Task<IActionResult> ActivateUser(ConfirmUserViewModel cuv)
        {
            if (!ModelState.IsValid)
            {
                return View(cuv);
            }
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            var confirmation = await _userService.ActivateUSer(cuv);
            if (confirmation == null)
            {
                ModelState.AddModelError("userValidation", "Datos erroneos");
                return View(cuv);
            }
            else
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

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
            string basePath = $"/Images/Users/{id}";
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

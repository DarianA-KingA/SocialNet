using AutoMapper;
using SocialeNet.Core.Application.DTOs.Email;
using SocialeNet.Core.Domain.Entities;
using SocialNet.Core.Application.Interfaces.Repository;
using SocialNet.Core.Application.Interfaces.Service;
using SocialNet.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNet.Core.Application.Services
{
    public class UserService : GenericService<SaveUserViewModel, UserViewModel, Users>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IEmailService emailService, IMapper mapper) : base(userRepository, mapper)
        {
            _userRepository = userRepository;
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task<UserViewModel> Login(LoginViewModel vm)
        {
            Users user = await _userRepository.LoginAsync(vm);

            if (user == null)
            {
                return null;
            }

            UserViewModel userVm = _mapper.Map<UserViewModel>(user);
            return userVm;
        }
        public async Task<UserViewModel> ActivateUSer(ConfirmUserViewModel vm)
        {
            Users user = await _userRepository.ActivateUserAsync(vm);

            if (user == null)
            {
                return null;
            }
            user.status = true;
            SaveUserViewModel savedUser = _mapper.Map<SaveUserViewModel>(user);
            await base.Update(savedUser, savedUser.Id);
            UserViewModel userVm = _mapper.Map<UserViewModel>(user);
            return userVm;
        }
        public async Task<Users> FindUserName(SaveUserViewModel vm)
        {
            Users User = await _userRepository.FindUserNameAsync(vm);
            return User;
        }
        public override async Task<SaveUserViewModel> Add(SaveUserViewModel vm)
        {
            Random random = new Random();
            string cadena = "";
            int randomNumber;
            for (int i = 0; i < 4; i++)
            {
                randomNumber = random.Next(9);
                cadena += randomNumber.ToString();
            }
            vm.ConfirmationCode = cadena;
            SaveUserViewModel userVm = await base.Add(vm);

            await _emailService.SendAsync(new EmailRequest
            {
                To = userVm.Email,
                From = _emailService._mailSettings.EmailFrom,
                Body = $"Se ha creado el usuario {userVm.UserName} y su codigo de confirmacion es:{userVm.ConfirmationCode}",
                Subject = "Creacion de usuario"
            });

            return userVm;
        }

        public async Task<List<UserViewModel>> GetAllViewModelWithInclude()
        {
            var userList = await _userRepository.GetAllWithIncludeAsync(new List<string> { "Products" });

            return userList.Select(user => new UserViewModel
            {
                Name = user.Name,
                LastName = user.LastName,
                UserName = user.UserName,
                Id = user.Id,
                Email = user.Email,
                Pasword = user.Password,
                Phone = user.Phone,
                ImageUrl = user.ImageUrl
            }).ToList();
        }

    }


    
}

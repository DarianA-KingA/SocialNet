using AutoMapper;
using SocialeNet.Core.Domain.Entities;
using SocialNet.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNet.Core.Application.Mappings
{
    class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<User, UserViewModel>()
                .ReverseMap()
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedDate, opt => opt.Ignore());

            CreateMap<User, SaveUserViewModel>()
                .ForMember(x => x.ConfirmPassword, opt => opt.Ignore())
                .ForMember(x => x.File, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedDate, opt => opt.Ignore());
        }
    }
}

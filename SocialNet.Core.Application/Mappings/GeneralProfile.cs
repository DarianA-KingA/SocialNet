using AutoMapper;
using SocialeNet.Core.Domain.Entities;
using SocialNet.Core.Application.ViewModels.Commentary;
using SocialNet.Core.Application.ViewModels.Friend;
using SocialNet.Core.Application.ViewModels.Publication;
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
            CreateMap<Users, UserViewModel>()
                .ReverseMap()
                .ForMember(x => x.status, opt => opt.Ignore())
                .ForMember(x => x.ConfirmationCode, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedDate, opt => opt.Ignore());

            CreateMap<Users, SaveUserViewModel>()
                .ForMember(x => x.ConfirmPassword, opt => opt.Ignore())
                .ForMember(x => x.File, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedDate, opt => opt.Ignore());

            CreateMap<Comentary, ComentaryViewModel>()
                .ReverseMap()
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedDate, opt => opt.Ignore());

            CreateMap<Comentary, SaveComentaryViewModel>()
                .ReverseMap()
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedDate, opt => opt.Ignore());

            CreateMap<Publications, PublicationViewModel>()
                .ReverseMap()
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedDate, opt => opt.Ignore());

            CreateMap<Publications, SavePublicationViewModel>()
                .ForMember(x => x.UserComentary, opt => opt.Ignore())
                .ForMember(x => x.PublicationId, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedDate, opt => opt.Ignore());

            CreateMap<Friends, FriendViewModel>()
                .ReverseMap()
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedDate, opt => opt.Ignore());

            CreateMap<Friends, SaveFriendViewModel>()
                .ReverseMap()
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedDate, opt => opt.Ignore());
        }
    }
}

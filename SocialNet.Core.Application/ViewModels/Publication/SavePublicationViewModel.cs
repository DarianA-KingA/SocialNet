using Microsoft.AspNetCore.Http;
using SocialNet.Core.Application.ViewModels.Commentary;
using SocialNet.Core.Application.ViewModels.Friend;
using SocialNet.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNet.Core.Application.ViewModels.Publication
{
    public class SavePublicationViewModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        [Required(ErrorMessage = "Debe colocar alguna descripcion")]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
        [DataType(DataType.Text)]
        public string ImageUrl { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile File { get; set; }

        #region entities
        public List<PublicationViewModel> Publications { get; set; }
        public List<UserViewModel> Friends { get; set; }
        public List<UserViewModel> Users { get; set; }
        #endregion
        #region "Save entities"
        public SaveComentaryViewModel SaveComentary { get; set; }

        public SaveFriendViewModel saveFriend { get; set; }
        #endregion

    }
}

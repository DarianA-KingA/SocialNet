﻿using SocialeNet.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNet.Core.Application.ViewModels.User
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Pasword { get; set; }

        //navigation property
        public ICollection<Publications> Publications { get; set; }
        public ICollection<Comentary> Comentaries { get; set; }

        public ICollection<Friend> Friends { get; set; }
    }
}

using SocialeNet.Core.Domain.Entities;
using SocialNet.Core.Application.ViewModels.Publication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNet.Core.Application.Interfaces.Service
{
    public interface IPublicationService : IGenericService<SavePublicationViewModel, PublicationViewModel, Publications>
    {
    }
}

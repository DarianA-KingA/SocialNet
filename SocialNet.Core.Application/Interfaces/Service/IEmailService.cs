using SocialeNet.Core.Application.DTOs.Email;
using SocialeNet.Core.Domain.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialNet.Core.Application.Interfaces.Service
{
    public interface IEmailService
    {
        public MailSettings _mailSettings { get; }
        Task SendAsync(EmailRequest request);
    }
}

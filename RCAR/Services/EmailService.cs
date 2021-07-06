using RCAR.Domain.Interfaces;
using RCAR.Extension;
using RCAR.Helper.Constans;
using RCAR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Services
{
    public class EmailService : IEmailService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> SendConfirmEmailAsync(string callBackUrl, string email)
        {
            return await EmailSender.MailSender(email, EmailConstans.SUBJECT_CONFIRM_EMAIL, EmailConstans.BODY_CONFIRM_EMAIL + "<a href =\"" + callBackUrl + "\">POTWIERDZENIE KONTA</a>");
        }

        public async Task<bool> SendResetPasswordEmailAsync(string callBackUrl, string email)
        {
            return await EmailSender.MailSender(email, EmailConstans.SUBJECT_RESET_PASSWORD_EMAIL, EmailConstans.BODY_RESET_PASSWORD_EMAIL + "<a href =\"" + callBackUrl + "\">ZMIANA HASŁA</a>");
        }
    }
}

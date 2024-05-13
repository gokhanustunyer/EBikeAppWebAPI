using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBikeAppWebAPI.business.Abstract.Service
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string email, string subject, string htmlContent);
        Task<bool> SendEmailAsHtml(string email, string subject, string htmlContent);
    }
}

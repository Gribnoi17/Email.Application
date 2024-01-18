using Email.Application.Api.Contracts.Emails.Controllers;
using Email.Application.Api.Contracts.Emails.Requests;
using Email.Application.Api.Contracts.Emails.Responses;
using Email.Application.AppServices.Contracts.Emails.Handlers;
using Email.Application.Host.Infrastructure.Filters;
using Email.Application.Host.Infrastructure.MapService;
using Microsoft.AspNetCore.Mvc;

namespace Email.Application.Host.Emails.Controllers
{
    /// <inheritdoc cref="IEmailController"/>
    [ApiController]
    [TypeFilter(typeof(ExceptionFilter))]
    [Route("api/mails")]
    public class EmailController : Controller, IEmailController
    {
        private readonly ISendEmailHandler _sendEmailHandler;
        private readonly IGetEmailsHandler _getEmailsHandler;

        public EmailController(ISendEmailHandler sendEmailHandler, IGetEmailsHandler getEmailsHandler)
        {
            _sendEmailHandler = sendEmailHandler;
            _getEmailsHandler = getEmailsHandler;
        }

        [HttpPost]
        [Route("send")]
        public async Task SendEmail(EmailRequest request, CancellationToken token)
        {
            var emailInternalRequest = MappingService.MapToEmailInternalRequest(request);

            await _sendEmailHandler.Handle(emailInternalRequest, token);
        }

        [HttpGet]
        [Route("get")]
        public async Task<EmailResponse[]> GetEmails(CancellationToken token)
        {
            var emailMessages = await _getEmailsHandler.Handle(token);

            var result = MappingService.MapToEmailMessages(emailMessages);

            return result.ToArray();
        }
    }
}
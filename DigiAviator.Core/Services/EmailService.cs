using DigiAviator.Core.Contracts;
using DigiAviator.Core.Models;
using System.Net;
using System.Net.Mail;

namespace DigiAviator.Core.Services
{
    public class EmailService : IEmailService
	{
		private readonly IValidationService _validationService;

		public EmailService(IValidationService validationService)
		{
			_validationService = validationService;

		}

		public async Task SendEmail(EmailSubmitViewModel model)
		{
			var (isValid, validationError) = _validationService.ValidateModel(model);

			if (!isValid)
			{
				throw new ArgumentException("Invalid data submitted.");
			}

            try
            {
				using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
				{
					smtpClient.Credentials = new NetworkCredential("djowlie@gmail.com", "@NicolaiION20");
					smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
					smtpClient.EnableSsl = true;

					using (MailMessage mail = new MailMessage())
					{
						mail.From = new MailAddress("djowlie@gmail.com", "Digital Aviator Support");
						mail.To.Add(new MailAddress("djowlie@gmail.com"));
						mail.CC.Add(new MailAddress(model.Email, model.Name));
						mail.Subject = model.Subject;
						mail.Body = model.Body;

						await smtpClient.SendMailAsync(mail);
					}
				}
			}
			catch (Exception)
            {
                throw new Exception("Could not send email.");
			}
			
		}
	}
}

using DigiAviator.Core.Contracts;
using DigiAviator.Core.Models;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;

namespace DigiAviator.Core.Services
{
	public class EmailService : IEmailService
	{
		public async Task SendEmail(EmailSubmitViewModel model)
		{

			using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
			{
				smtpClient.Credentials = new NetworkCredential("nottoday", "pal");
				smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
				smtpClient.EnableSsl = true;

				using (MailMessage mail = new MailMessage())
				{
					mail.From = new MailAddress("nottoday", "Digital Aviator Support");
					mail.To.Add(new MailAddress("nottoday"));
					mail.CC.Add(new MailAddress(model.Email, model.Name));
					mail.Subject = model.Subject;
					mail.Body = model.Body;

					await smtpClient.SendMailAsync(mail);
				}
			}
		}
	}
}

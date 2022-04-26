using DigiAviator.Core.Models;

namespace DigiAviator.Core.Contracts
{
	public interface IEmailService
	{
		Task SendEmail(EmailSubmitViewModel model);
	}
}

using DigiAviator.Core.Models;

namespace DigiAviator.Core.Contracts
{
	public interface IEmailService
	{
		Task<bool> SendEmail(EmailSubmitViewModel model);
	}
}

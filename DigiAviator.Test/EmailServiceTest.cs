using DigiAviator.Core.Contracts;
using DigiAviator.Core.Models;
using DigiAviator.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace DigiAviator.Test
{
    public class EmailServiceTest
    {
        private ServiceProvider _serviceProvider;

        [SetUp]
        public async Task Setup()
        {
            var serviceCollection = new ServiceCollection();

            _serviceProvider = serviceCollection
                .AddSingleton<IEmailService, EmailService>()
                .AddSingleton<IValidationService, ValidationService>()
                .BuildServiceProvider();
        }

        [Test]
        public async Task InvalidEmailModelMustThrow()
        {
            var service = _serviceProvider.GetService<IEmailService>();

            EmailSubmitViewModel email = new EmailSubmitViewModel { 
                Name = "A",
                Email = "NotAnEmail",
                Subject = "No",
                Body = "Invalid"
            };

            Assert.CatchAsync<ArgumentException>(async () => await service.SendEmail(email), "Invalid data submitted.");
        }

        [Test]
        public async Task ValidEmailModelMustNotThrow()
        {
            var service = _serviceProvider.GetService<IEmailService>();

            EmailSubmitViewModel email = new EmailSubmitViewModel
            {
                Name = "Alexander Savov",
                Email = "savov.alxndr@gmail.com",
                Subject = "Unit testing",
                Body = "This is an automated test for a successful email submission. Please disregard."
            };

            Assert.DoesNotThrowAsync(async () => await service.SendEmail(email));
        }
    }
}

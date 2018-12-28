using System;

using Microsoft.Extensions.Configuration;

using Specter.Api.Services.UrlService;

namespace Specter.Api.Services
{
    public interface IUrlCreatorService
    {
        IUserUrlCreatorService User { get; }
    }

    public class UrlCreatorService : IUrlCreatorService
    {
        private readonly IConfiguration _configuration;

        public IUserUrlCreatorService User => new UserUrlCreatorService(_configuration);

        public UrlCreatorService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    }

}
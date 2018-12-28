using System;

using Microsoft.Extensions.Configuration;

namespace Specter.Api.Services.UrlService
{
    
    public interface IUserUrlCreatorService
    {
        string CreateResetPasswordLink(string token);
        string ActivateUserLink(string token);
    }

    public class UserUrlCreatorService : IUserUrlCreatorService
    {
        private readonly string _appEndpoint;

        public UserUrlCreatorService(IConfiguration configuration)
        {
            _appEndpoint = configuration.GetValue<string>("AppEndPoint");
        }

        public string CreateResetPasswordLink(string token)
        {
            return CreateLink("users/reset", token);
        }

        public string ActivateUserLink(string token)
        {
            return CreateLink("users/activate", token);
        }

        private string CreateLink(string path, string token)
        {
            var builder = new UriBuilder(_appEndpoint);
            
            builder.Path = path;

            builder.Query = "token=" + token;

            return builder.Uri.ToString();
        }
    }
}
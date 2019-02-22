using System;

using Microsoft.AspNetCore.Hosting;

using Specter.Api.Extensions;

namespace Specter.Api.Services
{
    public interface ISecretInterpreter
    {
        string GetKey(string key);
    }

    public class SecretInterpreter : ISecretInterpreter
    {
        private readonly IHostingEnvironment _environment;

        protected virtual string StartEnvToken => "ENV:";

        protected virtual EnvironmentVariableTarget Target => _environment.IsDevelopment()
                                                                ? EnvironmentVariableTarget.Machine
                                                                : EnvironmentVariableTarget.Process;

        public SecretInterpreter(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public virtual string GetKey(string key)
        {
            if(string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            if(key.StartsWith(StartEnvToken))
                return Environment.GetEnvironmentVariable(key.TrimStart(StartEnvToken), Target);

            return key;
        }
    }
}
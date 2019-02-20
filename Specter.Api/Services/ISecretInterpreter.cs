using System;
using Specter.Api.Extensions;

namespace Specter.Api.Services
{
    public interface ISecretInterpreter
    {
        string GetKey(string key);
    }

    public class SecretInterpreter : ISecretInterpreter
    {
        protected virtual string StartEnvToken => "ENV:";

        public virtual string GetKey(string key)
        {
            if(string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            if(key.StartsWith(StartEnvToken))
                return Environment.GetEnvironmentVariable(key.TrimStart(StartEnvToken), EnvironmentVariableTarget.Machine);

            return key;
        }
    }
}
using System;
using System.Collections.Generic;

namespace Specter.Api.Services.Email
{
    public abstract class BaseEmailTemplate : IEmailTemplate
    {
        public abstract string Subject { get; }
        public string Body => BuildBody();

        public IEnumerable<IEmailAttachament> Attachaments => null;

        protected BaseEmailTemplate()
        {            
        }

        protected abstract string BuildBody();
    }
}
using System.Collections.Generic;

namespace Specter.Api.Services.Email
{
    public abstract class BaseEmailTemplate : IEmailTemplate
    {
        public abstract string Subject { get; }
        public virtual string Body => BuildBody();

        public virtual IEnumerable<IEmailAttachament> Attachaments => null;

        protected BaseEmailTemplate()
        {            
        }

        protected abstract string BuildBody();

        public virtual string PlainText() => Body;

        public virtual string HtmlText() => Body;
    }
}
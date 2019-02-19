using System.Collections.Generic;

namespace Specter.Api.Services
{
    public interface IEmailTemplate
    {
        string Subject { get; }

        string Body { get; }

        IEnumerable<IEmailAttachament> Attachaments { get; }

        string PlainText();
        string HtmlText();
    }
}

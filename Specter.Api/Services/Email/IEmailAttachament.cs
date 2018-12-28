namespace Specter.Api.Services
{
    public interface IEmailAttachament
    {
        string Name { get; }
        byte[] Content { get; }
    }
}

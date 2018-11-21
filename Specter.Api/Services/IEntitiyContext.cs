using System.Collections.Generic;

using Specter.Api.Entities;

namespace Specter.Api.Services
{
    public interface IEntitiyContext
    {
        IEnumerable<User> Users { get; }
        
    }
}
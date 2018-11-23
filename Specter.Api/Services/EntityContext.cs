using System;
using System.Collections.Generic;

using Newtonsoft.Json;

using Specter.Api.Entities;

namespace Specter.Api.Services
{
    public class EntityContext : IEntitiyContext
    {
        public IEnumerable<User> Users => null;
    }
}
using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Identity;

namespace Specter.Api.Data.Entities
{
    public interface IRemovable
    {
        bool Removed { get; set; }
    }
}
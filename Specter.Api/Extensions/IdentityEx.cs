using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace Specter.Api.Extensions
{
    public static class IdentityEx
    {
        public static string CreateErrorMessage(this IdentityResult result)
        {
            if(result.Succeeded)
                return null;

            return string.Join(";", result.Errors.Select(e => '[' + e.Code + ':' + e.Description + ']'));
        }
    }
}
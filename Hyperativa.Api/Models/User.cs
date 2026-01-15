using Microsoft.AspNetCore.Identity;

namespace Hyperativa.Api.Models
{
    public class User : IdentityUser<Guid>
    {
        public required string Name { get; set; }
    }
}

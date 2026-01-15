namespace Hyperativa.Api.Models
{
    public interface ICurrentUser
    {
        Guid? UserId { get; }
    }
}

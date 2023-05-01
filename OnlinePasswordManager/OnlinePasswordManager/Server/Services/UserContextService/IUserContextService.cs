using System.Security.Claims;

namespace OnlinePasswordManager.Server.Services.UserContextService
{
    public interface IUserContextService
    {
        int GetUserId { get; }
        ClaimsPrincipal User { get; }
    }
}
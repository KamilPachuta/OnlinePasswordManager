using Microsoft.AspNetCore.Authorization;
using OnlinePasswordManager.Server.Data.Entities;
using System.Security.Claims;

namespace OnlinePasswordManager.Server.Authorization
{
    public class ResourcePasswordRequirementHandler : AuthorizationHandler<ResourcePasswordRequirement, Password>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
            ResourcePasswordRequirement requirement, Password password)
        {
            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            if(userId == password.UserId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using OnlinePasswordManager.Server.Data.Entities;
using System.Security.Claims;

namespace OnlinePasswordManager.Server.Authorization
{
    public class ResourceNoteRequirementHandler : AuthorizationHandler<ResourceNoteRequirement, Note>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
            ResourceNoteRequirement requirement, Note note)
        {
            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            if (userId == note.UserId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}

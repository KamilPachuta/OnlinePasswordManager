using Microsoft.AspNetCore.Authorization;
using OnlinePasswordManager.Server.Data.Entities;
using System.Security.Claims;

namespace OnlinePasswordManager.Server.Authorization
{
    public class ResourceCategoryRequirementHandler : AuthorizationHandler<ResourceCategoryRequirement, Category>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
            ResourceCategoryRequirement requirement, Category category)
        {
            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            if (userId == category.UserId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}

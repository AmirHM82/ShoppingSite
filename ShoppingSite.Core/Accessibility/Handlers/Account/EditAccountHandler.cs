using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using ShoppingSite.Core.Accessibility.Requirements.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.Core.Accessibility.Handlers.Account
{
    public class EditAccountHandler : AuthorizationHandler<ManageAccountRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ManageAccountRequirement requirement)
        {
            var authFilterContext = (HttpContext)context.Resource;
            if (authFilterContext is null)
                return Task.CompletedTask;

            var loggedInUserId = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var userIdBeingEdited = authFilterContext.Request.Query["userId"];

            if (loggedInUserId is not null)
                if (context.User.HasClaim("Edit User", "True") || loggedInUserId == userIdBeingEdited)
                    context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}

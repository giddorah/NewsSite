using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NewsSite.Data
{
    public class MinimumAgeHandler : AuthorizationHandler<AgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AgeRequirement requirement)
        {
            var dateOfBirth = Convert.ToDateTime(
            context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth));


            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MusicMicroservice.Application.Authorization.Requirements;

namespace MusicMicroservice.Application.Authorization.Handlers
{
    public class AgeHandler: AuthorizationHandler<AgeRequirements>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AgeRequirements requirement)
        {
            var dateOfBirthClaim = context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth);
            if (dateOfBirthClaim is null)
            {
                context.Fail(new AuthorizationFailureReason(this, "Birthday claim not found!"));
                return Task.CompletedTask;
            }

            if (!DateTime.TryParse(dateOfBirthClaim.Value, out DateTime dateOfBirth))
            {
                
                context.Fail(new AuthorizationFailureReason(this, "Birthday claim is not valid date!"));
                return Task.CompletedTask;
            }

            int userAge = DateTime.Today.Year - dateOfBirth.Year;
            if(dateOfBirth > DateTime.Today.AddYears(-userAge))
            {
                userAge--;
            }

            if(userAge >= requirement.MinAge)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail(new AuthorizationFailureReason(this, "User does not meet the minimum age requirement!"));
            }

            return Task.CompletedTask;
        }
    }
}
using CookieIdentity.AppCode;

using Microsoft.AspNetCore.Authorization;

namespace CookieIdentity.Authorization
{
    public class HRManagerProbationRequirementHandler : AuthorizationHandler<HRManagerProbationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HRManagerProbationRequirement requirement)
        {
            if (!context.User.HasClaim(x => x.Type == Constant.EMPLOYMENT_DATE))
            {
                return Task.CompletedTask;
            }
            else
            {
                var hasEmploymentDate = DateTime.TryParse(context.User.FindFirst(x => x.Type == Constant.EMPLOYMENT_DATE)?.Value, out var employmentDate);
                if (hasEmploymentDate)
                {
                    var period = DateTime.Now - employmentDate;
                    if (period.Days >= requirement.ProbationMonths * 30)
                    {
                        context.Succeed(requirement);
                    }
                }
                return Task.CompletedTask;
            }
        }
    }
}

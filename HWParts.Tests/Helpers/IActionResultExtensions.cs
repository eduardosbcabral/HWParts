using Microsoft.AspNetCore.Mvc;

namespace HWParts.Tests.Helpers
{
    public static class IActionResultExtensions
    {
        public static OkObjectResult GetOk(this IActionResult actionResult)
        {
            return actionResult as OkObjectResult;
        }

        public static T GetValue<T>(this IActionResult actionResult)
        {
            return (T)actionResult.GetOk().Value;
        }
    }
}

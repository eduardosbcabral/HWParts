using Microsoft.AspNetCore.Mvc;

namespace HWParts.Tests.Helpers
{
    public static class IActionResultExtensions
    {
        public static ObjectResult GetObjectResult(this IActionResult actionResult)
        {
            return actionResult as ObjectResult;
        }

        public static T GetValue<T>(this IActionResult actionResult)
        {
            return (T)actionResult.GetObjectResult().Value;
        }
    }
}

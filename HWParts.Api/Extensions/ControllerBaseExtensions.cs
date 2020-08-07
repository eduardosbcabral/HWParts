using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HWParts.Api.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static StatusCodeResult CreatedEntity(this ControllerBase controllerBase, object value = null)
        {
            return new StatusCodeResult((int) HttpStatusCode.Created);
        }
    }
}

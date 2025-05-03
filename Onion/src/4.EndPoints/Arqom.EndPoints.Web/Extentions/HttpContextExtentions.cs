using Microsoft.AspNetCore.Http;
using Arqom.Core.Contracts.ApplicationServices.Events;
using Arqom.Utilities;

namespace Arqom.EndPoints.Web.Extensions
{
    public static class HttpContextExtensions
    {
        public static ICommandDispatcher CommandDispatcher(this HttpContext httpContext) =>
            (ICommandDispatcher)httpContext.RequestServices.GetService(typeof(ICommandDispatcher));

        public static IQueryDispatcher QueryDispatcher(this HttpContext httpContext) =>
            (IQueryDispatcher)httpContext.RequestServices.GetService(typeof(IQueryDispatcher));
        public static IEventDispatcher EventDispatcher(this HttpContext httpContext) =>
            (IEventDispatcher)httpContext.RequestServices.GetService(typeof(IEventDispatcher));
        public static ArqomServices ArqomApplicationContext(this HttpContext httpContext) =>
            (ArqomServices)httpContext.RequestServices.GetService(typeof(ArqomServices));
    }
}

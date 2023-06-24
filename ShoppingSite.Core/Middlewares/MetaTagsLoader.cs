using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ShoppingSite.Core.Interfaces;
using ShoppingSite.Core.ViewModels.Configuration;
using System.Threading.Tasks;

namespace ShoppingSite.Core.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MetaTagsLoader : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var viewPageService = context.RequestServices.GetRequiredService<IViewPage>();
            context.Items["MetaTags"] = ((await viewPageService.FindAsync(context.Request.Path.Value))?.Tags).Adapt<ICollection<MetaTagViewModel>>(); //Ok now wtf, u gotta try using events in baseModerator
            await next(context);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MetaTagsLoader>();
        }
    }
}

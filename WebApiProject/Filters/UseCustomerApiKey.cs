using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiProject.Filters
{
    public class UseCustomerApiKey
    {
        public class UseApiKeyAttribute : Attribute, IAsyncActionFilter
        {
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
                var apiKey = configuration.GetValue<string>("CustomerApiKey");

                if (!context.HttpContext.Request.Headers.TryGetValue("code", out var code))
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                if (!apiKey.Equals(code))
                {
                    context.Result = new UnauthorizedResult();
                }

                await next();
            }
        }
    }
}

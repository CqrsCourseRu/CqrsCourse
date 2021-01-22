using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Layers.WebApi
{
    public class CheckOrderFilterAttribute: ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var dbContext = context.HttpContext.RequestServices.GetRequiredService<IDbContext>();
            var cus = context.HttpContext.RequestServices.GetRequiredService<ICurrentUserService>();
            var id = (int)context.ActionArguments["id"];

            var count = await dbContext.Orders.CountAsync(
                x => x.UserEmail == cus.Email && x.Id == id);
            if (count != 1) //throw new Exception("Order not found");
            {
                context.Result = new NotFoundResult();
                return;
            }

            await base.OnActionExecutionAsync(context, next);
        }
    }
}

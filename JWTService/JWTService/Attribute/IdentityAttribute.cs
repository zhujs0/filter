using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTService.Attribute
{
    public class IdentityAttribute: ActionFilterAttribute
    {
        public string AuthId { get; set; }
        public IdentityAttribute()
        {

        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string userId = context.HttpContext.User.Identity.Name;
            //if (string.IsNullOrWhiteSpace(AuthId))
            //{
            //    context.Result = new StatusCodeResult(405);
            //    return;
            //}
            if (context.HttpContext.User.Identity.IsAuthenticated == false)
            {
                context.Result = new StatusCodeResult(405);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}

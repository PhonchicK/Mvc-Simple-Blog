using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcSimpleBlog.WebUI.Filters
{
    public class Auth : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            bool skipAuthorization = context.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true)
                                 || context.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true);

            if (skipAuthorization)
            {
                return;
            }

            if (context.HttpContext.Session["user"] == null)
            {
                context.Result = new RedirectResult(FormsAuthentication.LoginUrl);
                return;
            }
        }
    }
}
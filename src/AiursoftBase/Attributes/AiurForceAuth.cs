using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using AiursoftBase.Exceptions;

namespace AiursoftBase.Attributes
{
    public class AiurForceAuth : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var _iController = context.Controller as Controller;
            if (!_iController.User.Identity.IsAuthenticated)
            {
                throw new NotAiurSignedInException(_iController);
            }
        }
    }
}

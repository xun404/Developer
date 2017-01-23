using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using AiursoftBase.Exceptions;

namespace AiursoftBase.Attributes
{
    /// <summary>
    /// Request the signed in token or throw a NotAiurSignedInException 
    /// </summary>
    public class AiurForceAuth : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var _controller = context.Controller as Controller;
            var _queryReturnUrl = _controller.HttpContext.Request.Query["returnurl"];

            string _stateUrl = string.Empty;

            if (!string.IsNullOrEmpty(_queryReturnUrl))
                _stateUrl = _queryReturnUrl;
            else
                _stateUrl = $"{_controller.Request.Path}";

            if (!_controller.User.Identity.IsAuthenticated)
            {
                throw new NotAiurSignedInException(_controller, _stateUrl);
            }
        }
    }
}

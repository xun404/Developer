using AiursoftBase.Exceptions;
using AiursoftBase.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AiursoftBase.Attributes
{
    /// <summary>
    /// This will stop current action with any Aiursoft exceptions.
    /// </summary>
    public class AiurForceAuthExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
            switch (context.Exception.GetType().Name)
            {
                case nameof(NotAiurSignedInException):
                    var _iController = (context.Exception as NotAiurSignedInException).controller;
                    var request = _iController.HttpContext.Request;
                    var hostpath = $"{request.Scheme}://{request.Host}{request.Path}";
                    var authpath = $"{request.Scheme}://{request.Host}/Auth/AuthResult";
                    var url = OAuthService.GenerateAuthUrl(authpath, hostpath);
                    _iController.HttpContext.Response.Redirect(url);
                    break;
                default:
                    break;
            }
            context.ExceptionHandled = true;
        }
    }
}

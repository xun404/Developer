using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AiursoftBase.Exceptions
{
    public class NotAiurSignedInException : Exception
    {
        public Controller controller { get; }
        public string SignInRedirectPath { get; }
        public NotAiurSignedInException(Controller controller,string SuccessfulRedirectPath)
        {
            this.controller = controller;
            this.SignInRedirectPath = SuccessfulRedirectPath;
        }
    }
}

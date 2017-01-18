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
        public NotAiurSignedInException(Controller controller)
        {
            this.controller = controller;
        }
    }
}

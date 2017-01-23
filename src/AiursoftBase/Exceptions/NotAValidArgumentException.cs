using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AiursoftBase.Exceptions
{
    public class NotAValidAiurArgumentException : Exception
    {
        public IEnumerable<string> ArgumentName => _Arguments;
        private List<string> _Arguments { get; set; }
        public NotAValidAiurArgumentException(params string[] arguments)
        {
            foreach (var arg in arguments)
            {
                this._Arguments.Add(arg);
            }
        }
    }
}

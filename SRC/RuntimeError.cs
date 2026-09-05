using System;
using System.Collections.Generic;
using System.Text;

namespace Kabuk
{
    internal class RuntimeError : Exception
    {
        readonly public Token token; // The token associated with the runtime error

        public RuntimeError(Token token, string message) : base(message)
        {
            
            this.token = token;
            // Initialize the runtime error with the associated token and error message
        }


    }
}

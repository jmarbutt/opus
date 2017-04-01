using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Opus.Controls
{
    public class ControlValidationHelper
    {
        private string _message;

        public ControlValidationHelper(string message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            _message = message;
            ThrowValidationError = true;
        }

        public bool ThrowValidationError { get; set; }

        public object ValidationError
        {
            get { return null; }
            [DebuggerNonUserCode]
            set
            {
                
                if (ThrowValidationError)
                {
                    throw new ValidationException(_message);
                }
            }
        }
    }
}
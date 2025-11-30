using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace WizardAndWitch.WizardAndWitch

{
    // Custom exception for invalid options / inputs
    public class InvalidOptionException : Exception
    {
        public InvalidOptionException(string message) : base(message) { }
    }
}

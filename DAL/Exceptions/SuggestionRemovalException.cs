using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exceptions
{
    public class SuggestionRemovalException : Exception
    {
        public SuggestionRemovalException(string message) : base(message)
        {
        }
    }
}

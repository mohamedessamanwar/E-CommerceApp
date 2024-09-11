using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessAccessLayer.Exception
{
    public class CustomValidationException  : System.Exception
    {
        public IDictionary<string, List<string>> Errors { get; }

        public CustomValidationException(IDictionary<string, List<string>> errors , string massage):base(massage)
        {
            Errors = errors;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquirrelsBox.Generic.Extensions
{
    public static class ErrorMessagesExtensions
    {
        public static List<string> GetErrorMessages(List<KeyValuePair<string, object>> errorState)
        {
            var errors = new List<string>();
            foreach (var item in errorState)
            {   
                if (item.Value is List<string> errorList)
                {
                    errors.AddRange(errorList);
                }
            }
            return errors;
        }
    }
}

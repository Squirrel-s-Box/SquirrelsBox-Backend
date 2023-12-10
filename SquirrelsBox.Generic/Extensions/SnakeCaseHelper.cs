using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SquirrelsBox.Generic.Extensions
{
    public static class SnakeCaseHelper
    {
        public static string ToSnakeCase(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var builder = new StringBuilder();
            var previousChar = char.MinValue;
            foreach (var currentChar in input)
            {
                if (char.IsLetterOrDigit(currentChar))
                {
                    if (char.IsUpper(currentChar))
                    {
                        if (previousChar != char.MinValue && !char.IsUpper(previousChar) && !char.IsDigit(previousChar))
                            builder.Append('_');
                        builder.Append(char.ToLower(currentChar));
                    }
                    else
                    {
                        builder.Append(currentChar);
                    }
                }
                else
                {
                    builder.Append('_');
                }

                previousChar = currentChar;
            }

            return builder.ToString();
        }
    }

}

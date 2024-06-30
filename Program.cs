
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Interpolation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var values = new Dictionary<string, string>
            {
                { "name", "Jim" }
            };

            string input = "Hello [name] [[author]]";

            string result = Interpolate(input, values);

            Console.WriteLine(result);
            Console.ReadLine();
        }

        public static string Interpolate(string input, Dictionary<string, string> values)
        {
            string leftPlaceholder = Guid.NewGuid().ToString();
            string rightPlaceholder = Guid.NewGuid().ToString();
            input = input.Replace("[[", leftPlaceholder).Replace("]]", rightPlaceholder);

            // Use regex to find tokens
            string pattern = @"\[(.*?)\]";
            var regex = new Regex(pattern);
            var matches = regex.Matches(input);

            // Replace tokens with dictionary values
            foreach (Match match in matches)
            {
                string key = match.Groups[1].Value;
                if (values.ContainsKey(key))
                {
                    input = input.Replace(match.Value, values[key]);
                }
            }

            input = input.Replace(leftPlaceholder, "[").Replace(rightPlaceholder, "]");

            return input;
        }
    }
}

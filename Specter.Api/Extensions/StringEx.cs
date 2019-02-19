using System;
using System.Text.RegularExpressions;

namespace Specter.Api.Extensions 
{
    public static class StringEx
    {
        private static readonly Regex _numberCheckRegex;

        static StringEx()
        {
            _numberCheckRegex = new Regex("^[0-9]+$");
        }

        public static string GetFirstUntil(this string source, bool includingFind, params string[] find)
        {
            return null;
        }

        public static string[] SplitFirst(this string source, char chr)
        {
            if(string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));

            return source.Split(chr, 2, StringSplitOptions.None);
        }

        public static bool IsNumber(this string source)
        {
            return _numberCheckRegex.IsMatch(source ?? string.Empty);
        }

        public static string TrimStart(this string target, string trimChars)
        {
            return target.TrimStart(trimChars.ToCharArray());
        }

        public static string TrimEnd(this string target, string trimChars)
        {
            return target.TrimEnd(trimChars.ToCharArray());
        }
    }
}
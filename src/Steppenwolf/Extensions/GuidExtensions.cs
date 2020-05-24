using System;

namespace Steppenwolf.Extensions
{
    public static class GuidExtensions
    {
        public static string ToLowerString(this Guid guid)
        {
            return guid.ToString().ToLower().Replace("-", string.Empty);
        }
    }
}
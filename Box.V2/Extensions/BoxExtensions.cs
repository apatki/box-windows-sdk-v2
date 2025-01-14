using System;

namespace Box.V2.Extensions
{
    internal static class BoxExtensions
    {
        /// <summary>
        /// Checks if the object is null 
        /// </summary>
        /// <typeparam name="T">Type of the object being checked</typeparam>
        /// <param name="param"></param>
        /// <param name="name"></param>
        internal static T ThrowIfNull<T>(this T param, string name) where T : class
        {
            return param ?? throw new ArgumentNullException(name);
        }

        /// <summary>
        /// Checks if the object is null 
        /// </summary>
        /// <typeparam name="T">Type of the object being checked</typeparam>
        /// <param name="param"></param>
        /// <param name="name"></param>
        internal static T ThrowIfNull<T>(this T? param, string name) where T : struct
        {
            return param ?? throw new ArgumentNullException(name);
        }

        /// <summary>
        /// Checks if a string is null or whitespace
        /// </summary>
        /// <param name="value"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static string ThrowIfNullOrWhiteSpace(this string value, string name)
        {
            return string.IsNullOrWhiteSpace(value) ? throw new ArgumentException("Required field cannot be null or whitespace", name) : value;
        }
    }
}

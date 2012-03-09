/*
 * Author:  Marc Levine
 * Project: SRR
 * Date:    From March 2011
 * 
 * Description
 * Contains language extensions for base data types
 * 
*/
using System;
using System.Linq;

namespace Disney.iDash.Shared
{
	public static class LanguageExtensions
	{

		public static string Left(this string expr, int length)
		{
			if (length > 0)
			{
				length = Math.Min(length, expr.Length);
				return expr.PadRight(length).Substring(0, length);
			}
			else
				return string.Empty;
		}

		public static string Right(this string expr, int length)
		{
			if (length > 0)
			{
				length = Math.Min(length, expr.Length);
				return expr.Substring(expr.Length - length, length);
			}
			else
				return string.Empty;
		}

		/// <summary>
		/// Convert a string 
		/// from: once upon a time 
		/// to: Once Upon A Time.
		/// </summary>
		/// <param name="expr"></param>
		/// <returns></returns>
        public static string Capitalise(this string expr)
        {
            var result = expr.ToLower().ToCharArray();
            var alpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var toUpper = true;

            for (var i = 0; i < result.Length; i++)
            {
                if (toUpper)
                {
                    result[i] = Convert.ToChar(result[i].ToString().ToUpper());
                    toUpper = false;
                }
                    toUpper = (!alpha.Contains(result[i]));
            }

            return string.Join(string.Empty, result);
        }

    }
}

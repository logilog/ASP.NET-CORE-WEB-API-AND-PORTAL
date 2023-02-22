using System;
using System.Collections.Generic;

namespace Helpers.Extensions
{
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Verilen exception'ın detay bilgisini alır.
        /// </summary>
        /// <param name="ExObj"></param>
        /// <returns></returns>
        public static string GetErrorDetail(this Exception ExObj)
        {
            string ErrorResult = ExObj.ToString();
            try
            {
                var messages = new List<string>();
                while (ExObj != null)
                {
                    messages.Add(ExObj.Message);
                    ExObj = ExObj.InnerException;
                }
                ErrorResult = string.Join(" - ", messages);
            }
            catch (Exception)
            {

            }
            return ErrorResult;
        }
    }
}

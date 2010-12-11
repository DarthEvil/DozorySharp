using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace DozorySharp
{
    /// <summary>
    /// Описывает работы с логами
    /// </summary>
    public class Logs
    {
        private static string _logBaseUrl = "http://logs.dozory.ru/";

        /// <summary>
        /// Получить список логов в установленную дату
        /// </summary>
        /// <param name="date">Дата</param>
        /// <returns></returns>
        public static List<string> GetLogsByDay (DateTime date)
        {
            List<string> result = new List<string>();

            string url = _logBaseUrl + date.Date.Year + "-" + date.Date.Month.ToString().PadLeft(2, '0') + "-" + date.Date.Day.ToString().PadLeft(2, '0') + "/";
            
            WebClient webClient = new WebClient();
            string list = webClient.DownloadString(url);

            Regex re = new Regex(">(?<number>\\d+).xml</a>");
            MatchCollection mc = re.Matches(list);
            
            foreach (Match item in mc)
            {
                result.Add(item.Groups[1].Value);
            }

            return result;
        }
    }
}

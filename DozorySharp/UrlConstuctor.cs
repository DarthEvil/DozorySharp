using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DozorySharp
{
    /// <summary>
    /// Конструктор строк запросов к API
    /// </summary>
    public class UrlConstuctor
    {
        private const string MainUrl = "http://api.dozory.ru/query/";

        /// <summary>
        /// Создание url для доступа к API профилей персонажей
        /// </summary>
        /// <param name="list">Список ИД персонажей</param>
        /// <returns>Строка запроса к сервису API</returns>
        public static string GetProfilesInfoUrl(List<int> list)
        {
            string result = MainUrl + "?rm=person_info";

            foreach (int item in list)
            {
                result += "&person_id=" + item;
            }

            return result;
        }

        /// <summary>
        /// Создание url для доступа к API статуса персонажа в организации
        /// </summary>
        /// <param name="org">ID организации</param>
        /// <param name="password">Пароль организации</param>
        /// <param name="list">Список персонажей</param>
        /// <param name="date">Текущая дата мск</param>
        /// <returns></returns>
        public static string GetOrgMembersStatusUrl(int org, string password, List<int> list, DateTime date)
        {
            string result = MainUrl + "?rm=org_member_status";

            foreach (int item in list)
            {
                result += "&person_id=" + item;
            }

            string dt = date.Day.ToString().PadLeft(2, '0') + "." + date.Month.ToString().PadLeft(2, '0') + "." +
                        date.Year;

            result += "&org_id=" + org;
            result += "&date=" + dt;
            result += "&sign=" + Md5(org + password + dt);

            return result;
        }

        /// <summary>
        /// Создание url для доступа к API склада
        /// </summary>
        /// <param name="auth">Данные авторизации организации</param>
        /// <param name="date">Дата</param>
        /// <param name="storageType">Тип склада</param>
        /// <returns></returns>
        public static string GetStorageUrl(OrgAuth auth, DateTime date, StorageType storageType)
        {
            string result;
            string type;
            string dt = date.Day.ToString().PadLeft(2, '0') + "." + date.Month.ToString().PadLeft(2, '0') + "." +
                        date.Year;

            if (storageType == StorageType.Main)
            {
                type = "storage";
            }
            else if (storageType == StorageType.Second)
            {
                type = "aux_storage";
            }
            else if (storageType == StorageType.Mods)
            {
                type = "mods_storage";
            }
            else if (storageType == StorageType.Prof)
            {
                type = "prof_storage";
            }
            else if (storageType == StorageType.Lib)
            {
                type = "lib_storage";
            }
            else
            {
                throw new SetParameterError("Задан некорректный тип склада!");
            }

            result = MainUrl + "?rm=org_storage_info";
            result += "&org_id=" + auth.Id;
            result += "&date=" + dt;
            result += "&type=" + type;
            result += "&sign=" + Md5(auth.Id + auth.Password + dt + type);
            return result;
        }

        /// <summary>
        /// Создание url для доступа к API казны
        /// </summary>
        /// <param name="auth">Данные авторизации организации</param>
        /// <param name="date">Дата</param>
        /// <param name="treasureType">Тип казны</param>
        /// <returns></returns>
        public static string GetTreasureUrl(OrgAuth auth, DateTime date, TreasureType treasureType)
        {
            string result;
            string type;
            string dt = date.Day.ToString().PadLeft(2, '0') + "." + date.Month.ToString().PadLeft(2, '0') + "." +
                        date.Year;
            if (treasureType == TreasureType.Money)
            {
                type = "money";
            }
            else if (treasureType == TreasureType.Taler)
            {
                type = "talers";
            }
            else if (treasureType == TreasureType.Exp)
            {
                type = "exp";
            }
            else
            {
                throw new SetParameterError("Задан некорректный тип склада!");
            }

            result = MainUrl + "?rm=org_treasury_info";
            result += "&org_id=" + auth.Id;
            result += "&date=" + dt;
            result += "&type=" + type;
            result += "&sign=" + Md5(auth.Id + auth.Password + dt + type);
            return result;
        }

        /// <summary>
        /// Создание url для доступа к API информации об организации
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string OrgPersons(int id)
        {
            return MainUrl + "?rm=org_members" + "&org_id=" + id;
        }

        /// <summary>
        /// Получение MD5 хеша строки
        /// </summary>
        /// <param name="input">Входная строка</param>
        /// <returns></returns>
        private static string Md5(string input)
        {
            var x = new MD5CryptoServiceProvider();
            byte[] bs = Encoding.UTF8.GetBytes(input);
            bs = x.ComputeHash(bs);
            var s = new StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            return s.ToString();
        }
    }
}
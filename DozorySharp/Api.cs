using System;
using System.Collections.Generic;
using System.Xml;

namespace DozorySharp
{
    /// <summary>
    /// Реализует доступ к официальному API проекта dozory.ru
    /// </summary>
    public class DozoryApi
    {
        private const int MaxPersonsInQuery = 50;

        /// <summary>
        /// Получение информации по списку персонажей
        /// </summary>
        /// <param name="list">Список ID персонажей</param>
        /// <returns></returns>
        public static Dictionary<int, PersonInfo> GetPersonsInfo(List<int> list)
        {
            var result = new Dictionary<int, PersonInfo>();
            result.Clear();

            //Остаток после полных партий
            int mod = list.Count%MaxPersonsInQuery;
            //Полных партий
            int full = list.Count/MaxPersonsInQuery;
            //оставшиеся - тоже партия
            if (mod != 0) full++;

            var doc = new XmlDocument();

            for (int i = 0; i < full; i++)
            {
                int ii = 0;
                var arguments = new List<int>();

                //Пока не набираем полную партию или не кончается список пишем id в запрос
                while ((ii != MaxPersonsInQuery) && (ii != list.Count))
                {
                    arguments.Add(list[ii]);
                    ii++;
                }

                //Получаем строку запроса
                string query = UrlConstuctor.GetProfilesInfoUrl(arguments);

                //Загружаем данные о персонажах
                doc.Load(query);
                XmlNodeList persons = doc.GetElementsByTagName("person");

                //Перебмраем пришедшие данные
                foreach (XmlNode person in persons)
                {
                    var info = new PersonInfo
                        (
                        Int32.Parse(person.Attributes["person_id"].Value),
                        person.Attributes["nick"].Value,
                        Int32.Parse(person.Attributes["magic_level"].Value),
                        person.Attributes["magic_align"].Value,
                        Int32.Parse(person.Attributes["org_id"].Value),
                        Int32.Parse(person.Attributes["max_stamina"].Value),
                        Int32.Parse(person.Attributes["max_energy"].Value),
                        Int32.Parse(person.Attributes["class_type_id"].Value),
                        person.Attributes["sex"].Value,
                        Int32.Parse(person.Attributes["tendency"].Value),
                        Int32.Parse(person.Attributes["reg_date"].Value),
                        Int32.Parse(person.Attributes["init_date"].Value),
                        Int32.Parse(person.Attributes["last_login"].Value),
                        Int32.Parse(person.Attributes["online_time"].Value),
                        Int32.Parse(person.Attributes["cnt_wins"].Value),
                        Int32.Parse(person.Attributes["cnt_lose"].Value),
                        person.Attributes["act_status"].Value,
                        Int32.Parse(person.Attributes["online"].Value)
                        );
                    result.Add(info.ID, info);
                }
            }
            return result;
        }

        /// <summary>
        /// Проверка онлайн-статуса членов организации
        /// </summary>
        /// <param name="auth">Авторизация организации</param>
        /// <param name="list">Спмсок ID персонажей</param>
        /// <param name="date">Текущая дата по мск</param>
        /// <returns></returns>
        public static Dictionary<int, PersonStatus> GetOrgMembersStatus(OrgAuth auth, List<int> list, DateTime date)
        {
            var result = new Dictionary<int, PersonStatus>();
            result.Clear();

            //Остаток после полных партий
            int mod = list.Count%MaxPersonsInQuery;
            //Полных партий
            int full = list.Count/MaxPersonsInQuery;
            //оставшиеся - тоже партия
            if (mod != 0) full++;

            var doc = new XmlDocument();

            for (int i = 0; i < full; i++)
            {
                int ii = 0;
                var arguments = new List<int>();

                //Пока не набираем полную партию или не кончается список пишем id в запрос
                while ((ii != MaxPersonsInQuery) || (ii != list.Count))
                {
                    arguments.Add(list[ii]);
                    ii++;
                }

                //Получаем строку запроса
                string query = UrlConstuctor.GetOrgMembersStatusUrl(auth.Id, auth.Password, arguments, date);

                //Загружаем данные о персонажах
                doc.Load(query);

                //Проверяем на наличие ошибки
                XmlNodeList errors = doc.GetElementsByTagName("error");

                if (errors.Count == 0)
                {
                    XmlNodeList persons = doc.GetElementsByTagName("person");

                    //Перебмраем пришедшие данные
                    foreach (XmlNode person in persons)
                    {
                        int id = Int32.Parse(person.Attributes["id"].Value);

                        PersonStatus status = PersonStatus.Online;

                        if (person.Attributes["status"].Value == "offline")
                        {
                            status = PersonStatus.Offline;
                        }

                        result.Add(id, status);
                    }
                }
                else
                {
                    //Если ошибка
                    throw new ApiAccessError(errors[0].InnerText);
                }
            }

            return result;
        }

        /// <summary>
        /// Получаем состав организации
        /// </summary>
        /// <param name="orgId">ID организации</param>
        /// <returns>Данные об организации</returns>
        public static OrgInfo GetOrgMembers(int orgId)
        {
            OrgInfo result = null;
            var doc = new XmlDocument();

            string query = UrlConstuctor.OrgPersons(orgId);
            doc.Load(query);

            XmlNodeList errors = doc.GetElementsByTagName("error");

            if (errors.Count == 0)
            {
                XmlNodeList orgs = doc.GetElementsByTagName("organization");

                if (orgs.Count == 1)
                {
                    XmlNode org = orgs[0];

                    int id = Int32.Parse(org.Attributes["id"].Value);
                    string name = org.Attributes["name"].Value;

                    result = new OrgInfo(id, name);
                    if (org.ChildNodes.Item(0) != null)
                    {
                        XmlNodeList members = org.ChildNodes.Item(0).ChildNodes;
                        foreach (XmlNode member in members)
                        {
                            int pid = Int32.Parse(member.Attributes["id"].Value);
                            string position = member.FirstChild.InnerText;
                            result.Main.Add(pid, position);
                        }
                    }

                    if (org.ChildNodes.Item(1) != null)
                    {
                        XmlNodeList members = org.ChildNodes.Item(1).ChildNodes;
                        foreach (XmlNode member in members)
                        {
                            int pid = Int32.Parse(member.Attributes["id"].Value);
                            string position = member.FirstChild.InnerText;
                            result.Reserve.Add(pid, position);
                        }
                    }
                }
            }
            else
            {
                //Если ошибка
                throw new ApiAccessError(errors[0].InnerText);
            }

            return result;
        }

        /// <summary>
        /// Загрузка списка складских операций за указанную дату
        /// </summary>
        /// <param name="auth">Данные авторизации организации</param>
        /// <param name="date">Дата</param>
        /// <param name="type">Тип склада</param>
        /// <returns></returns>
        public static List<StorageOperation> GetStorageDay(OrgAuth auth, DateTime date, StorageType type)
        {
            var result = new List<StorageOperation>();

            string query = UrlConstuctor.GetStorageUrl(auth, date, type);

            var doc = new XmlDocument();
            doc.Load(query);

            XmlNodeList errors = doc.GetElementsByTagName("error");

            if (errors.Count == 0)
            {
                XmlNodeList list = doc.GetElementsByTagName("action");

                foreach (XmlNode element in list)
                {
                    DateTime dateTime = DateTime.Parse(element.Attributes[0].Value);
                    string person = element.ChildNodes[0].InnerText;
                    int personId = Int32.Parse(element.ChildNodes[1].InnerText);
                    string item = element.ChildNodes[2].InnerText;
                    int instanceId = Int32.Parse(element.ChildNodes[3].InnerText);
                    string typeAction = element.ChildNodes[4].InnerText;

                    var operation = new StorageOperation(dateTime, person, personId, item, instanceId, typeAction);
                    result.Add(operation);
                }
            }
            else
            {
                //Если ошибка
                throw new ApiAccessError(errors[0].InnerText);
            }
            return result;
        }

        /// <summary>
        /// Загрузка списка финансовых операций за указанную дату
        /// </summary>
        /// <param name="auth">Данные авторизации организации</param>
        /// <param name="date">Дата</param>
        /// <param name="type">Тип склада</param>
        /// <returns></returns>       
        public static List<TreasureOperation> GetTreasureDay(OrgAuth auth, DateTime date, TreasureType type)
        {
            var result = new List<TreasureOperation>();
            result.Clear();

            string url = UrlConstuctor.GetTreasureUrl(auth, date, type);

            var doc = new XmlDocument();
            XmlNodeList list;

            doc.Load(url);

            list = doc.GetElementsByTagName("error");

            if (list.Count == 0)
            {
                list = doc.GetElementsByTagName("action");
                foreach (XmlNode item in list)
                {
                    DateTime dateTime = DateTime.Parse(item.Attributes[0].Value);
                    string nick = item.ChildNodes[0].InnerText;
                    string direction = item.ChildNodes[1].InnerText;
                    float value = float.Parse(item.ChildNodes[2].InnerText);

                    var operation = new TreasureOperation(dateTime, nick, direction, value);
                    result.Add(operation);
                }
            }
            else
            {
                //Ошибка доступа!
                throw new ApiAccessError(list[0].InnerText);
            }

            return result;
        }
    }
}
using System;

namespace DozorySharp
{
    /// <summary>
    /// Складская операция
    /// </summary>
    public class StorageOperation
    {
        private readonly DateTime _date;
        private readonly int _instance_id;
        private readonly string _item;
        private readonly string _person;
        private readonly int _personId;
        private readonly string _type_action;

        /// <summary>
        /// Создатель
        /// </summary>
        /// <param name="date">Дата</param>
        /// <param name="person">Имя персонажа</param>
        /// <param name="personId">ID персонажа</param>
        /// <param name="item">Название предмета</param>
        /// <param name="instanceId">ID предмета</param>
        /// <param name="typeAction">Тип операции</param>
        public StorageOperation(DateTime date, string person, int personId, string item, int instanceId,
                                string typeAction)
        {
            _date = date;
            _type_action = typeAction;
            _instance_id = instanceId;
            _item = item;
            _personId = personId;
            _person = person;
        }

        /// <summary>
        /// Дата и время операции
        /// </summary>
        public DateTime Date
        {
            get { return _date; }
        }

        /// <summary>
        /// Имя персонажа
        /// </summary>
        public string PersonNick
        {
            get { return _person; }
        }

        /// <summary>
        /// ID персонажа
        /// </summary>
        public int PersonID
        {
            get { return _personId; }
        }

        /// <summary>
        /// Название предмета
        /// </summary>
        public string ItemName
        {
            get { return _item; }
        }

        /// <summary>
        /// ID предмета
        /// </summary>
        public int ItemID
        {
            get { return _instance_id; }
        }

        /// <summary>
        /// Тип операции
        /// </summary>
        public string TypeAction
        {
            get { return _type_action; }
        }
    }
}
using System;

namespace DozorySharp
{
    /// <summary>
    /// Финансовая операция
    /// </summary>
    public class TreasureOperation
    {
        private readonly DateTime _date;
        private readonly string _direction;
        private readonly string _nick;
        private readonly float _value;

        /// <summary>
        /// Создатель
        /// </summary>
        /// <param name="date">Дата</param>
        /// <param name="nick">Имя персонажа</param>
        /// <param name="direction">Тип операции</param>
        /// <param name="value">Сумма</param>
        public TreasureOperation(DateTime date, string nick, string direction, float value)
        {
            _date = date;
            _value = value;
            _direction = direction;
            _nick = nick;
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
            get { return _nick; }
        }

        /// <summary>
        /// Тип операции
        /// </summary>
        public string TypeAction
        {
            get { return _direction; }
        }

        /// <summary>
        /// Сумма
        /// </summary>
        public float Value
        {
            get { return _value; }
        }
    }
}
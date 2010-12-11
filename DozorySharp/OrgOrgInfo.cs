using System.Collections.Generic;

namespace DozorySharp
{
    /// <summary>
    /// Описание организации
    /// </summary>
    public class OrgInfo
    {
        private readonly int _id;
        private readonly Dictionary<int, string> _main;
        private readonly string _name;
        private readonly Dictionary<int, string> _reserve;

        /// <summary>
        /// Создатель
        /// </summary>
        /// <param name="id">ID организации</param>
        /// <param name="name">Название организации</param>
        public OrgInfo(int id, string name)
        {
            _id = id;
            _name = name;
            _main = new Dictionary<int, string>();
            _reserve = new Dictionary<int, string>();
        }

        /// <summary>
        /// ID организации
        /// </summary>
        public int Id
        {
            get { return _id; }
        }

        /// <summary>
        /// Название организации
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Основной состав
        /// </summary>
        public Dictionary<int, string> Main
        {
            get { return _main; }
        }

        /// <summary>
        /// Резерв
        /// </summary>
        public Dictionary<int, string> Reserve
        {
            get { return _reserve; }
        }
    }
}
using System;

namespace DozorySharp
{
    /// <summary>
    /// Описание персонажа
    /// </summary>
    public class PersonInfo
    {
        private readonly string _act_status;
        private readonly int _class_type_id;
        private readonly int _cnt_lose;
        private readonly int _cnt_wins;
        private readonly int _init_date;
        private readonly int _last_login;
        private readonly string _magic_align;
        private readonly int _magic_level;
        private readonly int _max_energy;
        private readonly int _max_stamina;
        private readonly string _nick;
        private readonly double _online_time;
        private readonly int _org_id;
        private readonly int _person_id;
        private readonly int _reg_date;
        private readonly string _sex;
        private readonly int _tendency;
        private readonly int _online;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="personId">ИД персонажа</param>
        /// <param name="nick">Имя</param>
        /// <param name="magicLevel">Уровень</param>
        /// <param name="magicAlign">Сторона</param>
        /// <param name="orgId">Организация</param>
        /// <param name="maxStamina">Здоровье</param>
        /// <param name="maxEnergy">Энергия</param>
        /// <param name="classTypeId">Класс</param>
        /// <param name="sex">Пол</param>
        /// <param name="tendency">Склонность</param>
        /// <param name="regDate">Дата регистрация</param>
        /// <param name="initDate">Дата инициации</param>
        /// <param name="lastLogin">Последний вход</param>
        /// <param name="onlineTime">Время в игре</param>
        /// <param name="cntWins">Побед</param>
        /// <param name="cntLose">Поражений</param>
        /// <param name="actStatus">Состояние</param>
        /// /// <param name="online">Online\Offline</param>
        public PersonInfo(int personId, string nick, int magicLevel, string magicAlign, int orgId, int maxStamina,
                          int maxEnergy, int classTypeId, string sex, int tendency, int regDate, int initDate,
                          int lastLogin, double onlineTime, int cntWins, int cntLose, string actStatus, int online)
        {
            _person_id = personId;
            _online = online;
            _act_status = actStatus;
            _cnt_lose = cntLose;
            _cnt_wins = cntWins;
            _online_time = onlineTime;
            _last_login = lastLogin;
            _init_date = initDate;
            _reg_date = regDate;
            _tendency = tendency;
            _sex = sex;
            _class_type_id = classTypeId;
            _max_energy = maxEnergy;
            _max_stamina = maxStamina;
            _org_id = orgId;
            _magic_align = magicAlign;
            _magic_level = magicLevel;
            _nick = nick;
            _online = online;
        }

        /// <summary>
        /// ID персонажа
        /// </summary>
        public int ID
        {
            get { return _person_id; }
        }

        /// <summary>
        /// Никнейм
        /// </summary>
        public string Nick
        {
            get { return _nick; }
        }

        /// <summary>
        /// Магический уровень 
        /// (8 - человек, 0 - вне категорий)
        /// </summary>
        public int Level
        {
            get { return _magic_level; }
        }

        /// <summary>
        /// Сторона Силы.
        /// В случае ошибки возвращет null
        /// </summary>
        public PersonAlign? Align
        {
            get
            {
                PersonAlign result;
                switch (_magic_align)
                {
                    case "Dark":
                        result = PersonAlign.Dark;
                        break;
                    case "Light":
                        result = PersonAlign.Light;
                        break;
                    case "Unknown":
                        result = PersonAlign.Unknown;
                        break;
                    default:
                        return null;
                }
                return result;
            }
        }

        /// <summary>
        /// ID орагиазации
        /// </summary>
        public int OrgID
        {
            get { return _org_id; }
        }

        /// <summary>
        /// Здоровье
        /// </summary>
        public int Stamina
        {
            get { return _max_stamina; }
        }

        /// <summary>
        /// Энергия
        /// </summary>
        public int Energy
        {
            get { return _max_energy; }
        }

        /// <summary>
        /// Класс персонажа.
        /// В случае ошибки возвращет null
        /// </summary>
        public PersonClass? Class
        {
            get
            {
                PersonClass result;
                switch (_class_type_id)
                {
                    case 1:
                        result = PersonClass.Mage;
                        break;
                    case 2:
                        result = PersonClass.Witch;
                        break;
                    case 3:
                        result = PersonClass.Werewolf;
                        break;
                    case 4:
                        result = PersonClass.Vampire;
                        break;
                    case 5:
                        result = PersonClass.Incub;
                        break;
                    case 6:
                        result = PersonClass.Devon;
                        break;
                    default:
                        return null;
                }
                return result;
            }
        }

        /// <summary>
        /// Пол персонажа
        /// В случае ошибки возвращет null
        /// </summary>
        public PersonSex? Sex
        {
            get
            {
                PersonSex result;
                switch (_sex)
                {
                    case "M":
                        result = PersonSex.Male;
                        break;
                    case "F":
                        result = PersonSex.Female;
                        break;
                    default:
                        return null;
                }
                return result;
            }
        }

        /// <summary>
        /// Склонность персонажа
        /// В случае ошибки возвращет null
        /// </summary>
        public PersonTendency? Tendency
        {
            get
            {
                PersonTendency result;
                switch (_tendency)
                {
                    case 1:
                        result = PersonTendency.Adept;
                        break;
                    case 0:
                        result = PersonTendency.Free;
                        break;
                    case -1:
                        result = PersonTendency.Outcast;
                        break;
                    default:
                        return null;
                }
                return result;
            }
        }

        /// <summary>
        /// Дата регистрации
        /// </summary>
        public DateTime Register
        {
            get { return DateTime.SpecifyKind(new DateTime(1970, 1, 1, 0, 0, 0), DateTimeKind.Utc).AddSeconds(_reg_date); }
        }

        /// <summary>
        /// Дата инициации
        /// </summary>
        public DateTime Initiation
        {
            get { return DateTime.SpecifyKind(new DateTime(1970, 1, 1, 0, 0, 0), DateTimeKind.Utc).AddSeconds(_init_date); }
        }

        /// <summary>
        /// Последний вход
        /// </summary>
        public DateTime LastLogin
        {
            get { return DateTime.SpecifyKind(new DateTime(1970, 1, 1, 0, 0, 0), DateTimeKind.Utc).AddSeconds(_last_login); }
        }

        /// <summary>
        /// Время в игре
        /// </summary>
        public TimeSpan OnlineTime
        {
            get { return TimeSpan.Parse(_online_time.ToString()); }
        }

        /// <summary>
        /// Побед
        /// </summary>
        public int Wins
        {
            get { return _cnt_wins; }
        }

        /// <summary>
        /// Поражений
        /// </summary>
        public int Loses
        {
            get { return _cnt_lose; }
        }

        /// <summary>
        /// Состояние персонажа
        /// </summary>
        public string ActiveStatus
        {
            get { return _act_status; }
        }

        /// <summary>
        /// Online/Offline персонажа
        /// </summary>
        public PersonStatus OnlineStatus
        {
            get
            {
                PersonStatus result = PersonStatus.Offline;
                if (_online == 1)
                {
                    result = PersonStatus.Online;
                }
                return result;
            }
        }
    }
}
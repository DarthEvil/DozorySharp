namespace DozorySharp
{
    /// <summary>
    /// Авторизация организации
    /// </summary>
    public class OrgAuth
    {
        private readonly int _orgId;
        private readonly string _orgPassword;

        /// <summary>
        /// Создатель
        /// </summary>
        /// <param name="id">ID организации</param>
        /// <param name="password">Пароль организации</param>
        public OrgAuth(int id, string password)
        {
            _orgId = id;
            _orgPassword = password;
        }

        /// <summary>
        /// ID организации
        /// </summary>
        public int Id
        {
            get { return _orgId; }
        }

        /// <summary>
        /// Пароль организации
        /// </summary>
        public string Password
        {
            get { return _orgPassword; }
        }
    }
}
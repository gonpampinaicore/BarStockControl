using BarStockControl.Models;

namespace BarStockControl.Core
{
    public class SessionContext
    {
        private static SessionContext _instance;

        public static SessionContext Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SessionContext();
                return _instance;
            }
        }

        public User LoggedUser { get; private set; }

        public void SetUser(User user)
        {
            LoggedUser = user;
        }

        public void Clear()
        {
            LoggedUser = null;
        }

        public bool IsLoggedIn => LoggedUser != null;

        private SessionContext() { }
    }
}

using BarStockControl.Models;

namespace BarStockControl.Core
{
    public class SessionContext
    {
        private static readonly SessionContext _instance = new SessionContext();
        public static SessionContext Instance => _instance;
        public User? LoggedUser { get; private set; } = null;

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

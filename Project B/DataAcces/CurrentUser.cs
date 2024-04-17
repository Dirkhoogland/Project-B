

namespace Project_B.DataAcces
{
    public class CurrentUser : Users
    {
        public bool LoggedIn;

        public CurrentUser(int Id, string Email, string Name, string Password, bool loggedin) : base(Id, Email, Name, Password)
        {
            this.LoggedIn = loggedin;

        }
    }
}

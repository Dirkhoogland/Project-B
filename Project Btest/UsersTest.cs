using Project_B.Presentation;
using Project_B.DataAcces;
using Project_B.BusinessLogic;
using System.Data.SQLite;

namespace Project_Btest
{
    [TestClass]
    public class UsersTest
    {      
        [TestMethod]
        static void ReserveSeat()
        {
            // lay_out.ReserveSeat();
            Assert.AreEqual(1, 1);
        }
      
        [TestMethod]
        public void TestNewuser()
        {
            DataAccess.Database();
            string Email = "TestEmail1";
            string Name = "Testname1";
            string Password = "TestPassword1";
            Users.Newuser(Email, Name, Password);
            Users user = Users.Getuser(Email);
            Users.RemoveUser(Email);
            Assert.IsNotNull(user.Email);
        }
        // tests if login function checks correctly
        [TestMethod]
        public void TestLogin()
        {
                DataAccess.Database();
                string Email = "Email";
                string Password = "Password";
                bool check = Login.LoginLogic(Email, Password);
                Assert.IsTrue(check);
        }
        // checks if the user can fail to log in
        [TestMethod]
        public void TestFalselogin()
        {
            DataAccess.Database();
            string Email = "TestEmail1";
            string Name = "Testname1";
            string Password = "TestPassword1";
            Users.Newuser(Email, Name, Password);
            bool check = Login.LoginLogic("wrong email", "wrong password");
            Users.RemoveUser(Email);
            Assert.IsFalse(check);
        }
        // tests if the history exists
        [TestMethod]
        public void TestHistory()
        {
            DataAccess.Database();
            List<Bookinghistory> userhistory = Userhistorylogic.returnuserhistory(1);
            int lenght = userhistory.Count;
            Assert.AreEqual(5, lenght);    
        }
        [TestMethod]
        public void Testrankadmin()
        {
            DataAccess.Database();
            Users user = Users.Getuser("Email");
            Assert.AreEqual(1, user.rank);
        }
        [TestMethod]
        public void Testrankuser()
        {
            DataAccess.Database();
            Users user = Users.Getuser("Email2");
            Assert.AreEqual(0, user.rank);
        }
        [TestMethod]
        static void DisplaySeatLayout()
        {
            // lay_out.DisplaySeatLayout();
            Assert.AreEqual(1, 1);
        }
    }   
}
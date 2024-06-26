using Project_B.Presentation;
using Project_B.DataAcces;
using Project_B.BusinessLogic;
using System.Data.SQLite;

namespace Project_Btest
{
    [TestClass]
    public class UsersTest
    {
        //admin functions
        [TestMethod]
        public void GetuserbyIdTest()
        {
            DataAccess.Database();
            string email = "testmail";
            string name = "Dirk";
            string password = "leeg";

            Users.Newuser(email, name, password);
            Users user = Users.GetuserbyId(1);
            Assert.AreEqual("Dirk", user.Name);
        }
        [TestMethod]
        public void Testrankadmin()
        {
            DataAccess.Database();
            string email = "Email";
            string name = "Admintest";
            string password = "leeg";

            Users.Newuser(email, name, password, 1);
            Users user = Users.Getuser("Email");
            Assert.AreEqual(1, user.rank);
        }
        [TestMethod]
        public void Testrankuser()
        {
            DataAccess.Database();
            string email = "Email2";
            string name = "Testuser";
            string password = "leeg";

            Users.Newuser(email, name, password);
            Users user = Users.Getuser("Email2");
            Assert.AreEqual(0, user.rank);
        }
        // user functions
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
                string email = "Email45";
                string name = "Testlogin";
                string password = "Password";

                Users.Newuser(email, name, password);
                string Email = "Email45";
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
            int flightid = 1;
            int user = 1;
            string seat = "1 - A";
            string seatclass = "Economy";
            string notes = "-";
            Flight.CreateFlightAirbus330();
            Flight.reserveseat(flightid, user, seat, seatclass, notes);
            List<Bookinghistory> userhistory = Userhistorylogic.returnuserhistory(1);
            int lenght = userhistory.Count;
            Assert.AreEqual(1, lenght);    
        }

    }   
}
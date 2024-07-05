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
        public void GetuserbyEmailTest()
        {   // arrange
            DataAccess.Database();
            string email = "testmail";
            string name = "Dirk";
            string password = "leeg";
            // act
            Users.Newuser(email, name, password);
            Users user = Users.Getuser(email);
            Users.RemoveUser(email);
            // assert
            Assert.AreEqual("Dirk", user.Name);
        }
        [TestMethod]
        public void Testrankadmin()
        {   // arrange
            DataAccess.Database();
            string email = "Email";
            string name = "Admintest";
            string password = "leeg";
            // act
            Users.Newuser(email, name, password, 1);
            Users user = Users.Getuser("Email");
            Users.RemoveUser(email);
            // assert
            Assert.AreEqual(1, user.rank);
        }
        [TestMethod]
        public void Testrankuser()
        {   // arrange
            DataAccess.Database();
            string email = "Email2";
            string name = "Testuser";
            string password = "leeg";
            // act
            Users.Newuser(email, name, password);
            Users user = Users.Getuser("Email2");
            Users.RemoveUser(email);
            // assert
            Assert.AreEqual(0, user.rank);
        }
        // user functions
        [TestMethod]
        public void TestNewuser()
        {   // arrange
            DataAccess.Database();
            string Email = "TestEmail1";
            string Name = "Testname1";
            string Password = "TestPassword1";
            // act
            Users.Newuser(Email, Name, Password);
            Users user = Users.Getuser(Email);
            Users.RemoveUser(Email);
            // assert
            Assert.IsNotNull(user.Email);
        }
        // tests if login function checks correctly
        [TestMethod]
        public void TestLogin()
        {       // arrange
                DataAccess.Database();
                string email = "Email45";
                string name = "Testlogin";
                string password = "Password";
                // act
                Users.Newuser(email, name, password);
                string Email = "Email45";
                string Password = "Password";
                bool check = Login.LoginLogic(Email, Password);
                Users.RemoveUser(Email);
                // assert
                Assert.IsTrue(check);
        }
        // checks if the user can fail to log in
        [TestMethod]
        public void TestFalselogin()
        {   // arrange
            DataAccess.Database();
            string Email = "TestEmail1";
            string Name = "Testname1";
            string Password = "TestPassword1";
            // act
            Users.Newuser(Email, Name, Password);
            bool check = Login.LoginLogic("wrong email", "wrong password");
            Users.RemoveUser(Email);
            // assert
            Assert.IsFalse(check);
        }

        

    }   
}
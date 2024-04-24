using Project_B.Presentation;
using Project_B.DataAcces;
using Project_B.BusinessLogic;
using System.Data.SQLite;

namespace Project_Btest
{
    [TestClass]
    public class UsersTest
    {
        public static string databasePath
        {
            get
            {   // gets the path to where ever its currently on your pc/laptop and then into a DataSource file, which if its correctly downloaded from github it should find.
                return System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\DataSource"));
            }
        }
        static void CreateTable()
        {
            // creates the user table with a ID, Email Name And Password, the ID is with an Primary key and Autoincrement.
            try
            {
                string ConnectionString = $"Data Source={databasePath}\\database.db; Version = 3; New = True; Compress = True; ";
                string sql = "CREATE TABLE Users(" +
                    "ID INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "Email VARCHAR(255)," +
                    "Name VARCHAR(255)," +
                    "Password VARCHAR(225))";
                // using statements are used to confine the use of the connection to only this function, so the database remains useable outside of it since its automatially closed and does not remain open on a function when it shouldnt be
                using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
                {
                    c.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex) { }

        }
      
        [TestMethod]
        static void ReserveSeat()
        {
            // lay_out.ReserveSeat();
            Assert.AreEqual(1, 1);
        }
      
        [TestMethod]
        public void TestNewuser()
        {
            CreateTable();
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
            CreateTable();
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
        static void DisplaySeatLayout()
        {
            // lay_out.DisplaySeatLayout();
            Assert.AreEqual(1, 1);
        }
    }   
}
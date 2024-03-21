using Project_B.DataAcces;

namespace Project_Btest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestNewuser()
        { 
            string Email = "TestEmail1";
            string Name = "Testname1";
            string Password = "TestPassword1";
            Users.Newuser(Email, Name, Password);
            Users user = Users.Getuser(Email);
            Users.RemoveUser(Email);
            Assert.IsNotNull(user);
        }
    }
}
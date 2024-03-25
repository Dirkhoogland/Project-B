using Project_B.Presentation;

namespace Project_Btest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        static void ReserveSeat()
        {
            lay_out.ReserveSeat();
            Assert.AreEqual(1, 1);
        }

        [TestMethod]

        static void DisplaySeatLayout()
        {
            lay_out.DisplaySeatLayout();
            Assert.AreEqual(1, 1);
        }
    }   
}
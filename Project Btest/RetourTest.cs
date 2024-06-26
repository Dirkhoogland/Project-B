using System.Security.Cryptography;
using Project_B.BusinessLogic;
using Project_B.DataAcces;
using Project_B.Presentation;

namespace Project_Btest
{
    [TestClass]
    public class RetourTest
    {
        [TestMethod]
        public void TestRetourReservation()
        {
            DataAccess.Database();
            string seat = "14 - A";
            string seatClass = "Economy";
            int flightId = 20;
            int userId = 0;
            bool retourStatus = true;
            string extraNotes = "test";
            Flight.reserveseat(flightId, userId, seat, seatClass,extraNotes);
        }
    }
}
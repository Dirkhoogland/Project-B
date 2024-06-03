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
            string email = "test@mail";
            string name = "test";
            int seat = 14;
            string seatClass = "Economy";
            int flightId = 20;
            int userId = 0;
            string gate = "11";
            DateTime depatureTime = new DateTime(2021, 12, 12, 12, 12, 12);
            string destination = "Rome";
            bool retourStatus = true;
            string origin = "Amsterdam";
            string extraNotes = "test";

            DataAccess.ReserveSeat(email, name, seat, seatClass, flightId, userId, gate, depatureTime, destination, retourStatus, origin, extraNotes);
        }

        public void TestRetourWithBaggage()
        {

        }

        public void TestRetourCancellation()
        {
            
        }
    }
}
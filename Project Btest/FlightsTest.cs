using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Project_B.DataAcces;
using Project_Btest;
namespace Project_B.DataAcces.Flights
{
    [TestClass]
    public class FlightsTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            var flights = new List<Flight>
            {
                new Flight { FlightNumber = "KL1234", Destination = "Amsterdam", Origin = "London", DepartureTime = DateTime.Now, Status = "On time" },
                new Flight { FlightNumber = "KL1235", Destination = "Paris", Origin = "London", DepartureTime = DateTime.Now, Status = "On time" },
                new Flight { FlightNumber = "KL1236", Destination = "Berlin", Origin = "London", DepartureTime = DateTime.Now, Status = "On time" }
            };
            // Act
            var result = flights;
            // Assert
            Assert.AreEqual(3, result.Count);
        }
    }
}
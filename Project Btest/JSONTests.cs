using Project_B.DataAcces;

namespace Project_Btest
{
    [TestClass]
    public class JSONconversionTests
    {
        [TestMethod]
        public void JSONconversionTest()
        {
            string relativeDatabasePath = Path.Combine("..", "..", "..", "Project B", "DataSource");
            string absoluteDatabasePath = Path.GetFullPath(relativeDatabasePath);
            string databaseFileName = "database.db";
            string databaseFilePath = Path.Combine(absoluteDatabasePath, databaseFileName);

            // Ensure the database file exists
            Assert.IsTrue(File.Exists(databaseFilePath), $"Database file not found at {databaseFilePath}");

            string ConnectionString = $"Data Source={databaseFilePath}; Version = 3;";
            List<JSONconversion> tickets = JSONconversion.Convert_to_Json(ConnectionString);
            Assert.IsNotNull(tickets);
        }
    }
}
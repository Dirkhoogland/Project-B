using Project_B.DataAcces;

namespace Project_Btest
{
    [TestClass]
    public class JSONconversionTests
    {
        [TestMethod]

        // Check if the created JSON file doesn't return empty
        public void JSONfileTest()
        {
            string relativeDatabasePath = Path.Combine("..", "..", "..", "..", "Project B", "DataSource");
            string absoluteDatabasePath = Path.GetFullPath(relativeDatabasePath);
            string databaseFileName = "database.db";
            string databaseFilePath = Path.Combine(absoluteDatabasePath, databaseFileName);

            // Ensure the database file exists
            Assert.IsTrue(File.Exists(databaseFilePath), $"Database file not found at {databaseFilePath}");

            string ConnectionString = $"Data Source={databaseFilePath}; Version = 3;";
            List<JSONconversion> tickets = JSONconversion.Convert_to_Json(ConnectionString);
            Assert.IsNotNull(tickets);
        }

        [TestMethod]

        //Check if JSON file is located in the "Documents" folder.
        public void JSONlocationTest()
        {
            string relativeJsonPath = Path.Combine("..", "..", "..", "..", "..", "..", "Documents");
            string absoluteJsonPath = Path.GetFullPath(relativeJsonPath);
            string JsonFileName = "NewSouthTicketData.json";
            string JsonFilePath = Path.Combine(absoluteJsonPath, JsonFileName);

            Assert.IsTrue(File.Exists(JsonFilePath), $"JSON file not found at {JsonFilePath}");
        }
    }
}
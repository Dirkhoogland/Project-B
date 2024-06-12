using System.Data;
using System.Data.SQLite;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Project_B.DataAcces
{
    public class JSONconversionUsers
    {
        public long ID { get; private set; }
        public string Email { get; private set; }
        public string Name { get; private set; }
        public string Password { get; private set; }
        public int Rank { get; private set; }
        public int FlyPoints { get; private set; }

        public JSONconversionUsers(long id, string email, string name, string password, int rank, int flyPoints)
        {
            ID = id;
            Email = email;
            Name = name;
            Password = password;
            Rank = rank;
            FlyPoints = flyPoints;
        }

        public static List<JSONconversionUsers> Convert_to_Json_Users(string ConnectionString)
        {
            List<JSONconversionUsers> users = new List<JSONconversionUsers>();
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM Users";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            long id = rdr.GetInt64(0);
                            string email = rdr.GetString(1);
                            string name = rdr.GetString(2);
                            string password = rdr.GetString(3);
                            int rank = rdr.GetInt32(4);
                            int flyPoints = rdr.GetInt32(5);

                            JSONconversionUsers user = new JSONconversionUsers(id, email, name, password, rank, flyPoints);
                            users.Add(user);
                        }
                    }
                }
                Console.WriteLine("Data retrieved.");
                return users;
            }
        }

        public static void Create_Json_users()
        {
            string ConnectionString = $"Data Source={DataAccess.databasePath}\\database.db; Version = 3; New = True; Compress = True; DateTimeFormat=Custom;DateTimeStringFormat=dd-MM-yyyy HH:mm:ss";
            List<JSONconversionUsers> users = Convert_to_Json_Users(ConnectionString);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(users, options);
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = Path.Combine(documentsPath, "NewSouthUserData.json");
            File.WriteAllText(filePath, json);
        }
    }
}
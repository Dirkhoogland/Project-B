using System.Data.SQLite;
using System.Security.Cryptography;
using Project_B.DataAcces;

class TicketManager{
    public static void AddTicket(string email, string name, DateTime time){
        try{
            string ConnectionString = $"Data Source={DataAccess.databasePath}\\database.db; Version = 3; New = True; Compress = True; ";
            string sql = "INSERT INTO Tickets (Email, Name, PurchaseTime) VALUES (@Email, @Name, @PurchaseTime)";

            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString)){
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(sql, conn)){
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@PurchaseTime", time);
                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex){
            Console.WriteLine("ERROR: Could not add ticket.");
        }
    }

    public static void CancelTicket(){

    }
}
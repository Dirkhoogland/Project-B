using System.Data.SQLite;
using System.Security.Cryptography;
using Project_B.DataAcces;

class TicketManager{
    public static void AddTicket(string email, DateTime time){
        try{
            string ConnectionString = $"Data Source={DataAccess.databasePath}\\database.db; Version = 3; New = True; Compress = True; ";
            string sql = "INSERT INTO Tickets (Email, PurchaseTime) VALUES (@Email, @PurchaseTime)";

            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString)){
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(sql, conn)){
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@PurchaseTime", time);
                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex){
            Console.WriteLine("ERROR: Could not add ticket.");
        }
    }
}

class CancelTicket{

    private string SqlSelect = "SELECT FlightID, PurchaseTime FROM Tickets WHERE Email = @user_mail LIMIT 1";
    private string SqlDelete = "DELETE FROM Tickets WHERE Email = @user_mail";
    private int TicketID;
    private DateTime PurchaseTime;
    private DateTime CompareTime;

    public CancelTicket(){
        CompareTime = PurchaseTime.AddHours(3);
    }

    public bool CancelTicketAccess(string user_mail){
        string ConnectionString = $"Data Source={DataAccess.databasePath}\\database.db; Version = 3; New = True; Compress = True; ";
        // LIMIT 1 als tijdelijke 'fix' voor verwijderen van meerdere tickets. Hieraan nog werken

        using (SQLiteConnection conn = new SQLiteConnection(ConnectionString)){
            conn.Open();
            using (SQLiteCommand command = new SQLiteCommand(SqlSelect, conn)){
                command.Parameters.AddWithValue("@user_mail", user_mail);
                using (SQLiteDataReader reader = command.ExecuteReader()){
                    if (reader.Read()){
                        int TicketID = reader.GetInt32(0);
                        DateTime PurchaseTime = reader.GetDateTime(reader.GetOrdinal("PurchaseTime"));
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public void CancelTicketLogic(string UserMail){
        string ConnectionString = $"Data Source={DataAccess.databasePath}\\database.db; Version = 3; New = True; Compress = True; ";
        using (SQLiteConnection conn = new SQLiteConnection(ConnectionString)){
            conn.Open();
            using (SQLiteCommand command = new SQLiteCommand(SqlDelete, conn)){
                command.Parameters.AddWithValue("@user_mail", UserMail);
            }
        
        using (SQLiteCommand command_del = new SQLiteCommand(SqlDelete, conn)){
            command_del.Parameters.AddWithValue("@user_mail", UserMail);

            int row_removed = command_del.ExecuteNonQuery();

            if (CompareTime > DateTime.Now){
                Console.WriteLine("Your purchase has been cancelled and refunded. More info is sent to your E-Mail address.");
                AutoMail.SendMail(UserMail, "New South: Ticket Cancellation", Email.EmailBody(true, TicketID, PurchaseTime));
                }

            else{
                Console.WriteLine("Your purchase has been cancelled. More info is sent to your E-Mail address.");
                AutoMail.SendMail(UserMail, "New South: Ticket Cancellation", Email.EmailBody(false, TicketID, PurchaseTime));
                }
            }
        }
    }

    public void CancelTicketUserAccess(){
        Console.WriteLine("Cancelling a ticket more than 3 hours after transaction will NOT result in a refund.");
        Console.WriteLine("Enter the E-Mail used in purchase:");
        string user_mail = Console.ReadLine();
        if(CancelTicketAccess(user_mail)){
            CancelTicketAccess(user_mail);
            CancelTicketLogic(user_mail);
        }
        else{
            Console.WriteLine("Could not find a ticket.");
        }
    }


class Email{
    public static string EmailBody(bool refund, long flight_id, DateTime purchase_time){
        if (refund == true){
            string body_true = $@"Dear [Customer Name],

We hope this email finds you well.

We wanted to inform you that your ticket purchase has been successfully cancelled and refunded. Below are the details of your cancellation:

Ticket ID: {flight_id}
Purchase Time: {purchase_time}
Cancellation Time: {DateTime.Now}
Refund Amount: [TBA]

If you have any questions or concerns regarding your cancellation and refund, please don't hesitate to contact us at newsouthairlines@gmail.com.

Thank you for choosing us, and we look forward to serving you in the future.

Best regards,
New South";
            return body_true;
        }

        if (refund == false){
            string body_false = $@"Dear [Customer Name],

We hope this email finds you well.

We wanted to inform you that your ticket purchase has been successfully cancelled and refunded. Below are the details of your cancellation:

Ticket ID: {flight_id}
Purchase Time: {purchase_time}
Cancellation Time: {DateTime.Now}
Refund Amount: Inelligible

If you have any questions or concerns regarding your cancellation and refund, please don't hesitate to contact us at newsouthairlines@gmail.com.

Thank you for choosing us, and we look forward to serving you in the future.

Best regards,
New South";
            return body_false;
            }

        return "Invalid parameters.";
        }
    }
}
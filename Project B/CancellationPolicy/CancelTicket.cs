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

    public static void CancelTicket(){
        Console.WriteLine("Cancelling a ticket more than 3 hours after transaction will NOT result in a refund.");
        Console.WriteLine("Enter the E-Mail used in purchase:");
        string user_mail = Console.ReadLine();

        string ConnectionString = $"Data Source={DataAccess.databasePath}\\database.db; Version = 3; New = True; Compress = True; ";
        // LIMIT 1 als tijdelijke 'fix' voor verwijderen van meerdere tickets. Hieraan nog werken
        string sql_select = "SELECT PurchaseTime FROM Tickets WHERE Email = @user_mail LIMIT 1";
        string sql_delete = "DELETE FROM Tickets WHERE Email = @user_mail";

        using (SQLiteConnection conn = new SQLiteConnection(ConnectionString)){
            conn.Open();
            using (SQLiteCommand command = new SQLiteCommand(sql_select, conn)){
                command.Parameters.AddWithValue("@user_mail", user_mail);
                using (SQLiteDataReader reader = command.ExecuteReader()){
                    if (reader.Read()){
                        long ticket_id = reader.GetInt64(0);
                        DateTime purchase_time = reader.GetDateTime(reader.GetOrdinal("PurchaseTime"));

                        Console.WriteLine("A ticket has been found.");

                        using (SQLiteCommand command_del = new SQLiteCommand(sql_delete, conn)){
                            command_del.Parameters.AddWithValue("@user_mail", user_mail);

                            int row_removed = command_del.ExecuteNonQuery();

                            DateTime compare_time = purchase_time.AddHours(3);

                            if (compare_time > DateTime.Now){
                                Console.WriteLine("Your purchase has been cancelled and refunded. More info is sent to your E-Mail address.");
                                AutoMail.SendMail(user_mail, "New South: Ticket Cancellation", EmailBody(true, ticket_id, purchase_time));
                            }

                            else{
                                Console.WriteLine("Your purchase has been cancelled. More info is sent to your E-Mail address.");
                                AutoMail.SendMail(user_mail, "New South: Ticket Cancellation", EmailBody(false, ticket_id, purchase_time));
                            }
                        }
                    }
                    else{
                        Console.WriteLine("No ticket has been found.");
                    }
                } 
            }
        }
    }


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
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
        string sql_select = "SELECT PurchaseTime FROM Tickets WHERE Email = @user_mail LIMIT 1";
        string sql_delete = "DELETE FROM Tickets WHERE Email = @user_mail";

        using (SQLiteConnection conn = new SQLiteConnection(ConnectionString)){
            conn.Open();
            using (SQLiteCommand command = new SQLiteCommand(sql_select, conn)){
                command.Parameters.AddWithValue("@user_mail", user_mail);
                object time_result = command.ExecuteScalar();

                if (time_result != null){
                    DateTime purchase_time = Convert.ToDateTime(time_result);
                    Console.WriteLine("A ticket has been found.");

                    using (SQLiteCommand command_del = new SQLiteCommand(sql_delete, conn)){
                        command_del.Parameters.AddWithValue("@user_mail", user_mail);

                    int row_removed = command_del.ExecuteNonQuery();

                    DateTime compare_time = purchase_time.AddHours(3);

                    if (compare_time > DateTime.Now){
                        Console.WriteLine("Your purchase has been cancelled and refunded. More info is sent to your E-Mail address.");
                        AutoMail.SendMail(user_mail, "New South: Ticket Cancellation", EmailBody(true));
                    }

                    else{
                        Console.WriteLine("Your purchase has been cancelled. More info is sent to your E-Mail address.");
                        AutoMail.SendMail(user_mail, "New South: Ticket Cancellation", EmailBody(false));
                    }
                    }
                }

                else{
                    Console.WriteLine("No ticket has been found.");
                }
            }
        }
    }

    public static string EmailBody(bool refund){
        if (refund == true){
            string body_true = @"Dear [Customer Name],

We hope this email finds you well.

We wanted to inform you that your ticket purchase has been successfully cancelled and refunded. Below are the details of your cancellation:

Ticket ID: [Ticket ID]
Purchase Time: [Purchase Time]
Cancellation Time: [Cancellation Time]
Refund Amount: [Refund Amount]

If you have any questions or concerns regarding your cancellation and refund, please don't hesitate to contact us at [Your Contact Information].

Thank you for choosing us, and we look forward to serving you in the future.

Best regards,
New South";
            return body_true;
        }

        string body_false = @"Dear [Customer Name],

We hope this email finds you well.

We wanted to inform you that your ticket purchase has been successfully cancelled and refunded. Below are the details of your cancellation:

Ticket ID: [Ticket ID]
Purchase Time: [Purchase Time]
Cancellation Time: [Cancellation Time]
Refund Amount: [Refund Amount]

If you have any questions or concerns regarding your cancellation and refund, please don't hesitate to contact us at [Your Contact Information].

Thank you for choosing us, and we look forward to serving you in the future.

Best regards,
New South";
        return body_false;
        }
    }
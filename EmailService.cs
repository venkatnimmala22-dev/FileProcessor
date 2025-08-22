using System;
using System.Data.SqlClient;
using System.Net.Mail;

namespace FileProcessor
{
    public class EmailService
    {
        public static void SendStatusEmail(bool success)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MailDb"].ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM Maillist", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(reader["ToEmail"].ToString());
                    mail.CC.Add(reader["CcEmail"].ToString());
                    mail.Subject = "Status for the file processing";
                    mail.IsBodyHtml = true;
                    string status = success ? "<b style='color:green'>successfully</b>" : "<b style='color:red'>failed</b>";
                    mail.Body = $"Hi, your file has been processed {status}";

                    SmtpClient client = new SmtpClient(reader["SmtpServer"].ToString(), Convert.ToInt32(reader["Port"]));
                    client.Credentials = new System.Net.NetworkCredential(reader["Username"].ToString(), reader["Password"].ToString());
                    client.Send(mail);
                }
            }
        }
    }
}


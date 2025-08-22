using System;
using System.Data.SqlClient;

namespace FileProcessor
{
    public class DatabaseLogger
    {
        public static void LogException(Exception ex)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["LogDb"].ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Logs (Timestamp, Message, StackTrace) VALUES (@Timestamp, @Message, @StackTrace)", conn);
                cmd.Parameters.AddWithValue("@Timestamp", DateTime.Now);
                cmd.Parameters.AddWithValue("@Message", ex.Message);
                cmd.Parameters.AddWithValue("@StackTrace", ex.StackTrace);
                cmd.ExecuteNonQuery();
            }
        }
    }
}


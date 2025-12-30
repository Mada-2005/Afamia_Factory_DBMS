using Afamia_UI.Models.Entities;
using Microsoft.Data.SqlClient;

namespace Afamia_UI.Models.Queries
{
    public class AdminAccountService
    {
        private readonly DB db;

        public AdminAccountService(DB db)
        {
            this.db = db;
        }

        // Check if username already exists
        public bool UsernameExists(string username)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql = "SELECT COUNT(*) FROM UserAccount WHERE Username = @Username";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@Username", username);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // Create a new admin account
        public bool CreateAdminAccount(string username, string password)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    // Role 2 = Admin according to the roles array in Login.cs
                    string sql = "INSERT INTO UserAccount (Username, Password, Role) VALUES (@Username, @Password, 2)";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // Get all admin accounts
        public List<UserAccount> GetAllAdmins()
        {
            List<UserAccount> admins = new List<UserAccount>();
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql = "SELECT Username, Password, Role FROM UserAccount WHERE Role = 2 ORDER BY Username";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        admins.Add(new UserAccount(
                            0,
                            reader.GetString(0),
                            reader.GetString(1),
                            Role.Admin
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return admins;
        }

        // Delete an admin account
        public bool DeleteAdminAccount(string username)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql = "DELETE FROM UserAccount WHERE Username = @Username AND Role = 2";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@Username", username);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // Get total admin count
        public int GetTotalAdmins()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql = "SELECT COUNT(*) FROM UserAccount WHERE Role = 2";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    return (int)cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
    }
}

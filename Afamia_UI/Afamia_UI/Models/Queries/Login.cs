using Microsoft.Data.SqlClient;

namespace Afamia_UI.Models.Queries
{
    public class Login
    {
        private readonly DB db;
        private readonly string[] roles = {"", "Employee", "Admin", "CEO" };

        public Login(DB db)
        {
            this.db = db;
        }

        public string VerifyCredentials(string username, string password)
        {
             string userRole = null;
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sqlCommand = "Select Role from UserAccount where Username = @Username and Password = @Password";
                    SqlCommand cmd = new SqlCommand(sqlCommand, con);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                        userRole = roles[Convert.ToInt32(result)];
                    return userRole;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
    }
}

using Afamia_UI.Models.Entities;
using Microsoft.Data.SqlClient;

namespace Afamia_UI.Models.Queries
{
    public class ProductionLineServices
    {
        private readonly DB db;
        public ProductionLineServices(DB db)
        {
            this.db = db;
        }

        public List<ProductionLine> GetProductionLinesByRoomId(int roomId)
        {
            List<ProductionLine> productionLines = new List<ProductionLine>();
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sqlCommand = "Select * from Production_Pipeline where Room_ID = @Room_ID";
                    SqlCommand cmd = new SqlCommand(sqlCommand, con);
                    cmd.Parameters.AddWithValue("@Room_ID", roomId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ProductionLine line = new ProductionLine()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Room_ID = Convert.ToInt32(reader["Room_ID"]),
                            Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? "" : reader.GetString(reader.GetOrdinal("Name"))
                        };
                        productionLines.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return productionLines;
        }

        public ProductionLine GetProductionLineById(int id)
        {
            ProductionLine productionLine = new ProductionLine();
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql = "Select * from Production_Pipeline where Id = @id";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    ProductionLine line = new ProductionLine
                    {
                        Id = reader.GetInt32(0),
                        Room_ID = reader.GetInt32(1),
                        Name = reader.IsDBNull(2) ? "" : reader.GetString(2)
                    };
                    return line;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public void AddProductionLine(ProductionLine productionLine)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql = "Insert into Production_Pipeline (ID, Room_ID, Name) values (@ID, @Room_ID, @Name)";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@ID", productionLine.Id);
                    cmd.Parameters.AddWithValue("@Room_ID", productionLine.Room_ID);
                    cmd.Parameters.AddWithValue("@Name", productionLine.Name);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void EditProductionLine(ProductionLine productionLine)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql = "Update Production_Pipeline Set Room_ID = @Room_ID, Name = @Name Where Id = @Id";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@Room_ID", productionLine.Room_ID);
                    cmd.Parameters.AddWithValue("@Name", productionLine.Name);
                    cmd.Parameters.AddWithValue("@Id", productionLine.Id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql = "Delete From Production_Pipeline where ID = @ID";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@ID", id);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

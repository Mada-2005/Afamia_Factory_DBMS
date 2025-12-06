using Afamia_UI.Models.Entities;
using Microsoft.Data.SqlClient;

namespace Afamia_UI.Models.Queries
{
    public class ProductionRoomServices
    {
        private readonly DB db;
        public ProductionRoomServices(DB db)
        {
            this.db = db;
        }
        public List<ProductionRoom> GetAllProductionRooms()
        {
            List<ProductionRoom> productionRooms = new List<ProductionRoom>();
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sqlCommand = "Select * from ProductionRoom";
                    SqlCommand cmd = new SqlCommand(sqlCommand, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ProductionRoom room = new Entities.ProductionRoom()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Place = reader["Place"].ToString()
                        };
                        productionRooms.Add(room);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return productionRooms;
        }

        public int GetNumOfProductionRooms()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql = "\r\nSelect count(ID) \r\nfrom Production_Room\r\n";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    int n = Convert.ToInt32(cmd.ExecuteScalar());
                    return n;
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

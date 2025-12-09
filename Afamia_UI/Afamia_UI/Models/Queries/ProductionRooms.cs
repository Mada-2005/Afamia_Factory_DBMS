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
                    string sqlCommand = "Select * from Production_Room";
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

        public void AddProductionRoom(ProductionRoom productionRoom)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql = "Insert into Production_Room (ID, Place) values (@ID, @Place)";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@ID", productionRoom.Id);
                    cmd.Parameters.AddWithValue("@Place", productionRoom.Place);
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
                    string sql = "Delete From Production_Room where ID = @ID";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@ID",id);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public ProductionRoom GetProductionRoomById(int id)
        {
            ProductionRoom productionRoom = new ProductionRoom();
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql = "Select * from Production_Room where Id = @id";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    ProductionRoom room = new ProductionRoom
                    {
                        Id = reader.GetInt32(0),
                        Place = reader.GetString(1)
                    };
                    return room;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public void EditProductionRoom(ProductionRoom productionRoom)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql = "Update Production_Room Set Place = @Place Where Id = @Id";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@Place", productionRoom.Place);
                    cmd.Parameters.AddWithValue("@Id", productionRoom.Id);
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

using Afamia_UI.Models.Entities;
using Microsoft.Data.SqlClient;

namespace Afamia_UI.Models.Queries
{
    public class ProductServices
    {
        private readonly DB db;
        public ProductServices(DB db)
        {
            this.db = db;
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sqlCommand = "Select * from Product";
                    SqlCommand cmd = new SqlCommand(sqlCommand, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Product product = new Product()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? "" : reader.GetString(reader.GetOrdinal("Name")),
                            ProductionDate = Convert.ToDateTime(reader["Production_Date"]),
                            ExpirationDate = Convert.ToDateTime(reader["Expiration_Date"]),
                            type = Convert.ToInt32(reader["Type"]),
                            Batch_Id = Convert.ToInt32(reader["Batch_Id"]),
                            Production_Line = Convert.ToInt32(reader["Product_Line"]),
                            Customer_Id = reader.IsDBNull(reader.GetOrdinal("customer_id")) ? 0 : Convert.ToInt32(reader["customer_id"]),
                            Start_time = reader.IsDBNull(reader.GetOrdinal("Start_time")) ? null : (TimeSpan?)reader.GetTimeSpan(reader.GetOrdinal("Start_time")),
                            End_time = reader.IsDBNull(reader.GetOrdinal("End_time")) ? null : (TimeSpan?)reader.GetTimeSpan(reader.GetOrdinal("End_time"))
                        };
                        products.Add(product);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return products;
        }

        public Product GetProductById(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql = "Select * from Product where Id = @id";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Product product = new Product
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? "" : reader.GetString(reader.GetOrdinal("Name")),
                            ProductionDate = Convert.ToDateTime(reader["Production_Date"]),
                            ExpirationDate = Convert.ToDateTime(reader["Expiration_Date"]),
                            type = Convert.ToInt32(reader["Type"]),
                            Batch_Id = Convert.ToInt32(reader["Batch_Id"]),
                            Production_Line = Convert.ToInt32(reader["Product_Line"]),
                            Customer_Id = reader.IsDBNull(reader.GetOrdinal("customer_id")) ? 0 : Convert.ToInt32(reader["customer_id"]),
                            Start_time = reader.IsDBNull(reader.GetOrdinal("Start_time")) ? null : (TimeSpan?)reader.GetTimeSpan(reader.GetOrdinal("Start_time")),
                            End_time = reader.IsDBNull(reader.GetOrdinal("End_time")) ? null : (TimeSpan?)reader.GetTimeSpan(reader.GetOrdinal("End_time"))
                        };
                        return product;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public void AddProduct(Product product)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql = "Insert into Product ( Name, Production_Date, Expiration_Date, Type, Batch_Id, Product_Line, customer_id, Start_time, End_time) " +
                                 "values ( @Name, @Production_Date, @Expiration_Date, @Type, @Batch_Id, @Product_Line, @customer_id, @Start_time, @End_time)";
                    SqlCommand cmd = new SqlCommand(sql, con);
           
                    cmd.Parameters.AddWithValue("@Name", product.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Production_Date", product.ProductionDate);
                    cmd.Parameters.AddWithValue("@Expiration_Date", product.ExpirationDate);
                    cmd.Parameters.AddWithValue("@Type", product.type);
                    cmd.Parameters.AddWithValue("@Batch_Id", product.Batch_Id);
                    cmd.Parameters.AddWithValue("@Product_Line", product.Production_Line);
                    cmd.Parameters.AddWithValue("@customer_id", product.Customer_Id == 0 ? (object)DBNull.Value : product.Customer_Id);
                    cmd.Parameters.AddWithValue("@Start_time", product.Start_time ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@End_time", product.End_time ?? (object)DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void EditProduct(Product product)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql = "Update Product Set Name = @Name, Production_Date = @Production_Date, Expiration_Date = @Expiration_Date, " +
                                 "Type = @Type, Batch_Id = @Batch_Id, Product_Line = @Product_Line, customer_id = @customer_id, Start_time = @Start_time, End_time = @End_time " +
                                 "Where Id = @Id";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@Name", product.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Production_Date", product.ProductionDate);
                    cmd.Parameters.AddWithValue("@Expiration_Date", product.ExpirationDate);
                    cmd.Parameters.AddWithValue("@Type", product.type);
                    cmd.Parameters.AddWithValue("@Batch_Id", product.Batch_Id);
                    cmd.Parameters.AddWithValue("@Product_Line", product.Production_Line);
                    cmd.Parameters.AddWithValue("@customer_id", product.Customer_Id == 0 ? (object)DBNull.Value : product.Customer_Id);
                    cmd.Parameters.AddWithValue("@Start_time", product.Start_time ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@End_time", product.End_time ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Id", product.Id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql = "Delete From Product where ID = @ID";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        // Get all product types for dropdown
        public List<Products_Type> GetAllProductTypes()
        {
            List<Products_Type> productTypes = new List<Products_Type>();
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sqlCommand = "Select * from Products_Types";
                    SqlCommand cmd = new SqlCommand(sqlCommand, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Products_Type productType = new Products_Type()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? "" : reader.GetString(reader.GetOrdinal("Name")),
                            weight = reader.IsDBNull(reader.GetOrdinal("weight")) ? 0 : (float)Convert.ToDouble(reader["weight"])
                        };
                        productTypes.Add(productType);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return productTypes;
        }

        // Get product type by ID
        public Products_Type GetProductTypeById(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql = "Select * from Products_Types where Id = @id";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Products_Type productType = new Products_Type
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? "" : reader.GetString(reader.GetOrdinal("Name")),
                            weight = reader.IsDBNull(reader.GetOrdinal("weight")) ? 0 : (float)Convert.ToDouble(reader["weight"])
                        };
                        return productType;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}

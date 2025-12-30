using Afamia_UI.Models.Entities;
using Microsoft.Data.SqlClient;

namespace Afamia_UI.Models.Queries
{
    public class CustomerServices
    {
        private readonly DB db;
        public CustomerServices(DB db)
        {
            this.db = db;
        }

        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();

            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();

                    string sql = @"
                        Select Customer.*, PhoneData.Phone_Numbers
                        From Customer
                        Left join(
                            Select
                            Customer_ID, STRING_AGG(Phone, ', ') As Phone_Numbers
                            From customer_phone_numbers
                            Group By Customer_ID
                        ) As PhoneData on Customer_ID = Id";

                    SqlCommand command = new SqlCommand(sql, con);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Customer customer = new Customer
                        {
                            Id = reader.GetInt32(0),
                            FName = reader.GetString(1),
                            LName = reader.GetString(2),
                            Name = reader.IsDBNull(3) ? null : reader.GetString(3)
                        };
                        if (!reader.IsDBNull(4))
                        {
                            customer.Phone.AddRange(reader.GetString(4).Split(", "));
                        }

                        customers.Add(customer);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return customers;
        }

        public Customer GetCustomerById(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql = @"
                        Select Customer.*, PhoneData.Phone_Numbers
                        From Customer
                        Left join(
                            Select Customer_ID, STRING_AGG(Phone, ', ') As Phone_Numbers
                            From customer_phone_numbers
                            Group By Customer_ID
                        )
                        As PhoneData on Customer_ID = Id
                        where Id = @id";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Customer customer = new Customer
                        {
                            Id = reader.GetInt32(0),
                            FName = reader.GetString(1),
                            LName = reader.GetString(2),
                            Name = reader.IsDBNull(3) ? null : reader.GetString(3)
                        };
                        if (!reader.IsDBNull(4))
                        {
                            customer.Phone.AddRange(reader.GetString(4).Split(", "));
                        }

                        return customer;
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

        public void AddCustomer(Customer customer)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string InsertQuery = "INSERT INTO Customer (Id, FName, LName, Name) VALUES (@Id, @FName, @LName, @Name)";
                    SqlCommand cmd = new SqlCommand(InsertQuery, con);
                    cmd.Parameters.AddWithValue("@Id", customer.Id);
                    cmd.Parameters.AddWithValue("@FName", customer.FName);
                    cmd.Parameters.AddWithValue("@LName", customer.LName);
                    cmd.Parameters.AddWithValue("@Name", customer.Name ?? (object)DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            try
            {
                using (SqlConnection con2 = new SqlConnection(db.GetConnectionString()))
                {
                    con2.Open();
                    string insertQuery = "INSERT INTO customer_phone_numbers (Customer_ID, Phone) VALUES (@Id, @Phone)";
                    foreach (string phone in customer.Phone)
                    {
                        if (!string.IsNullOrEmpty(phone))
                        {
                            SqlCommand insertCmd = new SqlCommand(insertQuery, con2);
                            insertCmd.Parameters.AddWithValue("@Id", customer.Id);
                            insertCmd.Parameters.AddWithValue("@Phone", phone);
                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void EditCustomer(Customer customer)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql1 = "Update Customer Set FName = @FName, LName = @LName, Name = @Name Where Id = @Id";
                    SqlCommand cmd = new SqlCommand(sql1, con);
                    cmd.Parameters.AddWithValue("@FName", customer.FName);
                    cmd.Parameters.AddWithValue("@LName", customer.LName);
                    cmd.Parameters.AddWithValue("@Name", customer.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Id", customer.Id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    using (SqlTransaction transaction = con.BeginTransaction())
                    {
                        try
                        {
                            string deleteQuery = "DELETE FROM customer_phone_numbers WHERE Customer_ID = @Id";
                            SqlCommand deleteCmd = new SqlCommand(deleteQuery, con, transaction);
                            deleteCmd.Parameters.AddWithValue("@Id", customer.Id);
                            deleteCmd.ExecuteNonQuery();

                            string insertQuery = "INSERT INTO customer_phone_numbers (Customer_ID, Phone) VALUES (@Id, @Phone)";
                            foreach (string phone in customer.Phone)
                            {
                                if (!string.IsNullOrEmpty(phone))
                                {
                                    SqlCommand insertCmd = new SqlCommand(insertQuery, con, transaction);
                                    insertCmd.Parameters.AddWithValue("@Id", customer.Id);
                                    insertCmd.Parameters.AddWithValue("@Phone", phone);
                                    insertCmd.ExecuteNonQuery();
                                }
                            }
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            Console.WriteLine(ex.Message);
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public bool DeleteCustomer(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql = "DELETE FROM Customer WHERE Id = @id";
                    SqlCommand command = new SqlCommand(sql, con);
                    command.Parameters.AddWithValue("@id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public int GetTotalCustomers()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql = "Select count(Id) from Customer";
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

        public bool IsPhoneTaken(string phone)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql = "SELECT TOP 1 * FROM customer_phone_numbers WHERE Phone = @phone";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@phone", phone);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    int count = 0;
                    while (rdr.Read())
                    {
                        count++;
                    }
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}

using Afamia_UI.Models.Entities;
using Microsoft.Data.SqlClient;
using System.Transactions;

namespace Afamia_UI.Models.Queries
{
    public class EmployeesServices
    {
        private readonly DB db;
        public EmployeesServices(DB db)
        {
            this.db = db;
        }
        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();

                    string sql = "Select Employee.*, PhoneData.Phone_Numbers\r\nFrom Employee\r\nLeft join(\r\n\tSelect \r\n\tEmployee_ID, STRING_AGG(Phone, ', ') As Phone_Numbers\r\n\tFrom Employee_phone_numbers\r\n\tGroup By Employee_ID\r\n\t) As PhoneData on Employee_ID = Id\r\n";
                    SqlCommand command = new SqlCommand(sql, con);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Employee emp = new Employee
                        {
                            Id = reader.GetInt32(0),
                            FName = reader.GetString(1),
                            LName = reader.GetString(2),
                            role = reader.GetString(3),
                            Works_in_Factory = reader.GetBoolean(4),
                        };
                        if (!reader.IsDBNull(5))
                        {
                            emp.Phone.AddRange(reader.GetString(5).Split(", "));
                        }

                        employees.Add(emp);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;  // Re-throw to let caller handle
            }

            return employees;
        }

        public bool DeleteEmployee(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql = "DELETE FROM Employee WHERE Id = @id";
                    SqlCommand command = new SqlCommand(sql, con);
                    command.Parameters.AddWithValue("@id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                // Connection automatically closed here, even if exception occurs
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public Employee GetEmployeeById(int id)
        {
            Employee employee = new Employee();
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql = "Select Employee.*, PhoneData.Phone_Numbers\r\nFrom Employee \r\nLeft join(\r\n\tSelect Employee_ID, STRING_AGG(Phone, ', ') As Phone_Numbers\r\n\tFrom Employee_phone_numbers\r\n\tGroup By Employee_ID\r\n\t)\r\nAs PhoneData on Employee_ID = Id\r\nwhere Id = @id\r\n";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    Employee emp = new Employee
                    {
                        Id = reader.GetInt32(0),
                        FName = reader.GetString(1),
                        LName = reader.GetString(2),
                        role = reader.GetString(3),
                        Works_in_Factory = reader.GetBoolean(4),
                    };
                    if (!reader.IsDBNull(5))
                    {
                        emp.Phone.AddRange(reader.GetString(5).Split(", "));
                    }

                    return emp;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        public List<Employee> GetEmployeeByName(string FName)
        {
            List<Employee> employees = new List<Employee>();
           
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql = "Select Employee.*, PhoneData.Phone_Numbers\r\nFrom Employee \r\nLeft join(\r\n\tSelect Employee_ID, STRING_AGG(Phone, ', ') As Phone_Numbers\r\n\tFrom Employee_phone_numbers\r\n\tGroup By Employee_ID\r\n\t)\r\nAs PhoneData on Employee_ID = Id\r\nwhere FName = @FName\r\n";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@FName", FName);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Employee emp = new Employee
                        {
                            Id = reader.GetInt32(0),
                            FName = reader.GetString(1),
                            LName = reader.GetString(2),
                            role = reader.GetString(3),
                            Works_in_Factory = reader.GetBoolean(4),
                        };
                        if (!reader.IsDBNull(5))
                        {
                            emp.Phone.AddRange(reader.GetString(5).Split(", "));
                        }
                        employees.Add(emp);
                    }
                    return employees;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public List<Employee> GetEmployeeByName(string FName, string LName)
        {
            
            List<Employee> employees = new List<Employee>();
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql = "Select Employee.*, PhoneData.Phone_Numbers\r\nFrom Employee \r\nLeft join(\r\n\tSelect Employee_ID, STRING_AGG(Phone, ', ') As Phone_Numbers\r\n\tFrom Employee_phone_numbers\r\n\tGroup By Employee_ID\r\n\t)\r\nAs PhoneData on Employee_ID = Id\r\nwhere FName = @FName and LName = @LName\r\n";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@FName", FName);
                    cmd.Parameters.AddWithValue("@LName", LName);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Employee emp = new Employee
                        {
                            Id = reader.GetInt32(0),
                            FName = reader.GetString(1),
                            LName = reader.GetString(2),
                            role = reader.GetString(3),
                            Works_in_Factory = reader.GetBoolean(4),
                        };
                        if (!reader.IsDBNull(5))
                        {
                            emp.Phone.AddRange(reader.GetString(5).Split(", "));
                        }
                        employees.Add(emp);
                    }
                    return employees;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public void EditEmployee(Employee emp)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql1 = "Update Employee Set FName = @FName, LName = @LName, Role = @Role, Works_in_Factory = @Works_in_Factory Where Id = @Id";
                    SqlCommand cmd = new SqlCommand(sql1, con);
                    cmd.Parameters.AddWithValue("@FName", emp.FName);
                    cmd.Parameters.AddWithValue("@LName", emp.LName);
                    cmd.Parameters.AddWithValue("@Role", emp.role);
                    cmd.Parameters.AddWithValue("@Works_in_Factory", emp.Works_in_Factory);
                    cmd.Parameters.AddWithValue("@Id", emp.Id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                            string deleteQuery = "DELETE FROM Employee_phone_numbers WHERE Employee_ID = @Id";
                            SqlCommand deleteCmd = new SqlCommand(deleteQuery, con, transaction);
                            deleteCmd.Parameters.AddWithValue("@Id", emp.Id);
                            deleteCmd.ExecuteNonQuery();

                            string insertQuery = "INSERT INTO Employee_phone_numbers (Employee_ID, Phone) VALUES (@Id, @Phone)";
                            foreach (string phone in emp.Phone)
                            {
                                if (!string.IsNullOrEmpty(phone))
                                {
                                    SqlCommand insertCmd = new SqlCommand(insertQuery, con, transaction);

                                    insertCmd.Parameters.AddWithValue("@Id", emp.Id);
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

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        public void AddEmployee(Employee employee)
        {
            try
            {
                using(SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string InsertQuery = "INSERT INTO Employee (Id, FName, LName, Role, Works_in_Factory) VALUES (@Id, @FName, @LName, @Role, @Works_in_Factory)";
                    SqlCommand cmd = new SqlCommand(InsertQuery, con);
                    cmd.Parameters.AddWithValue("@Id", employee.Id);
                    cmd.Parameters.AddWithValue("@FName", employee.FName);
                    cmd.Parameters.AddWithValue("@LName", employee.LName);
                    cmd.Parameters.AddWithValue("@Role", employee.role);
                    cmd.Parameters.AddWithValue("@Works_in_Factory", employee.Works_in_Factory);
                    cmd.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                using (SqlConnection con2 = new SqlConnection(db.GetConnectionString()))
                {
                    con2.Open();
                    string insertQuery = "INSERT INTO Employee_phone_numbers (Employee_ID, Phone) VALUES (@Id, @Phone)";
                    foreach (string phone in employee.Phone)
                    {
                        Console.WriteLine(phone);
                        Console.WriteLine(employee.Id);
                        if (!string.IsNullOrEmpty(phone))
                        {
                            SqlCommand insertCmd = new SqlCommand(insertQuery, con2);

                            insertCmd.Parameters.AddWithValue("@Id", employee.Id);
                            insertCmd.Parameters.AddWithValue("@Phone", phone);
                            insertCmd.ExecuteNonQuery();

                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public int GetNumOfActiveEmployees()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql = "\r\nSelect count(ID) \r\nfrom Employee\r\nwhere Works_in_Factory = 1";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    int n = Convert.ToInt32(cmd.ExecuteScalar());
                    return n;
                }
            }
            catch(Exception ex)
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
                    string sql = "SELECT TOP 2 * FROM Employee_Phone_numbers WHERE Phone = @phone";
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
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

    }
}

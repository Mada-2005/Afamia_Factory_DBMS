using Afamia_UI.Models.Entities;
using Microsoft.Data.SqlClient;

namespace Afamia_UI.Models.Queries
{
    public class BatchInfo : Product
    {
        public string CustomerName { get; set; }
        public int TotalProductsInBatch { get; set; }
    }

    public class ProductTraceResult
    {
        public int BatchId { get; set; }
        public int TotalProductsInBatch { get; set; }
        public string CustomerName { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeRole { get; set; }
        public DateTime WorkDate { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int SectionID { get; set; }
        public string SectionName { get; set; }
        public int PipelineID { get; set; }
        public string PipelineName { get; set; }
        public int ProductionRoomID { get; set; }
        public string ProductionRoomPlace { get; set; }
    }

    public class TraceProductQueries
    {
        private readonly DB db;

        public TraceProductQueries(DB db)
        {
            this.db = db;
        }

        public BatchInfo GetBatchInfo(int batchId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    // Get batch info with customer details and total count
                    string sql = @"
                        SELECT TOP 1
                            p.*,
                            ISNULL(CONCAT(c.FName, ' ', c.LName), 'No Customer') AS CustomerName,
                            (SELECT COUNT(*) FROM Product WHERE Batch_Id = @BatchId) AS TotalProductsInBatch
                        FROM Product p
                        LEFT JOIN Customer c ON p.customer_id = c.Id
                        WHERE p.Batch_Id = @BatchId";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@BatchId", batchId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        BatchInfo batchInfo = new BatchInfo
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? "" : reader.GetString(reader.GetOrdinal("Name")),
                            ProductionDate = Convert.ToDateTime(reader["Production_Date"]),
                            ExpirationDate = Convert.ToDateTime(reader["Expiration_Date"]),
                            type = Convert.ToInt32(reader["Type"]),
                            Batch_Id = Convert.ToInt32(reader["Batch_Id"]),
                            Production_Line = Convert.ToInt32(reader["Product_Line"]),
                            Customer_Id = reader.IsDBNull(reader.GetOrdinal("customer_id")) ? 0 : Convert.ToInt32(reader["customer_id"]),
                            CustomerName = reader.GetString(reader.GetOrdinal("CustomerName")),
                            TotalProductsInBatch = reader.GetInt32(reader.GetOrdinal("TotalProductsInBatch")),
                            Start_time = reader.IsDBNull(reader.GetOrdinal("Start_time")) ? null : (TimeSpan?)reader.GetTimeSpan(reader.GetOrdinal("Start_time")),
                            End_time = reader.IsDBNull(reader.GetOrdinal("End_time")) ? null : (TimeSpan?)reader.GetTimeSpan(reader.GetOrdinal("End_time"))
                        };
                        return batchInfo;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<ProductTraceResult> GetProductionWorkersForBatch(int batchId)
        {
            List<ProductTraceResult> results = new List<ProductTraceResult>();

            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();

                    // Complex query to get all workers who worked on this batch
                    // The query joins Product -> Customer -> Production_Pipeline -> Daily_Work_Schedule -> Employee
                    // Groups by Batch_Id and shows all employees who worked on the production date
                    // If start/end times exist, filters by 30 mins before start and 15 mins after end
                    string sqlCommand = @"
                        SELECT 
                            p.Batch_Id AS BatchId,
                            (SELECT COUNT(*) FROM Product WHERE Batch_Id = p.Batch_Id) AS TotalProductsInBatch,
                            ISNULL(CONCAT(c.FName, ' ', c.LName), 'No Customer') AS CustomerName,
                            e.Id AS EmployeeID,
                            CONCAT(e.FName, ' ', e.LName) AS EmployeeName,
                            e.Role AS EmployeeRole,
                            dws.Work_Date AS WorkDate,
                            dws.Start_Time AS StartTime,
                            dws.End_Time AS EndTime,
                            ps.ID AS SectionID,
                            ps.Name AS SectionName,
                            pl.ID AS PipelineID,
                            pl.Name AS PipelineName,
                            pr.ID AS ProductionRoomID,
                            pr.Place AS ProductionRoomPlace
                        FROM Product p
                        LEFT JOIN Customer c ON p.customer_id = c.Id
                        INNER JOIN Production_Pipeline pl ON p.Product_Line = pl.ID
                        INNER JOIN Pipeline_Section ps ON pl.ID = ps.Pipeline_ID
                        INNER JOIN Daily_Work_Schedule dws ON ps.ID = dws.SectionID AND ps.Pipeline_ID = dws.Pipeline_ID
                        INNER JOIN Employee e ON dws.EmployeeID = e.Id
                        INNER JOIN Production_Room pr ON pl.Room_ID = pr.ID
                        WHERE p.Batch_Id = @BatchId
                          AND CAST(dws.Work_Date AS DATE) = CAST(p.Production_Date AS DATE)
                          AND (
                              (p.Start_time IS NULL OR p.End_time IS NULL) OR
                              (dws.Start_Time <= DATEADD(MINUTE, 15, p.End_time)
                               AND dws.End_Time >= DATEADD(MINUTE, -30, p.Start_time))
                          )
                        ORDER BY dws.Work_Date, dws.Start_Time, e.FName";

                    SqlCommand cmd = new SqlCommand(sqlCommand, con);
                    cmd.Parameters.AddWithValue("@BatchId", batchId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ProductTraceResult result = new ProductTraceResult
                        {
                            BatchId = reader.GetInt32(reader.GetOrdinal("BatchId")),
                            TotalProductsInBatch = reader.GetInt32(reader.GetOrdinal("TotalProductsInBatch")),
                            CustomerName = reader.IsDBNull(reader.GetOrdinal("CustomerName")) ? "No Customer" : reader.GetString(reader.GetOrdinal("CustomerName")),
                            EmployeeID = reader.GetInt32(reader.GetOrdinal("EmployeeID")),
                            EmployeeName = reader.GetString(reader.GetOrdinal("EmployeeName")),
                            EmployeeRole = reader.IsDBNull(reader.GetOrdinal("EmployeeRole")) ? "" : reader.GetString(reader.GetOrdinal("EmployeeRole")),
                            WorkDate = reader.GetDateTime(reader.GetOrdinal("WorkDate")),
                            StartTime = reader.IsDBNull(reader.GetOrdinal("StartTime")) ? null : (TimeSpan?)reader.GetTimeSpan(reader.GetOrdinal("StartTime")),
                            EndTime = reader.IsDBNull(reader.GetOrdinal("EndTime")) ? null : (TimeSpan?)reader.GetTimeSpan(reader.GetOrdinal("EndTime")),
                            SectionID = reader.GetInt32(reader.GetOrdinal("SectionID")),
                            SectionName = reader.IsDBNull(reader.GetOrdinal("SectionName")) ? "" : reader.GetString(reader.GetOrdinal("SectionName")),
                            PipelineID = reader.GetInt32(reader.GetOrdinal("PipelineID")),
                            PipelineName = reader.IsDBNull(reader.GetOrdinal("PipelineName")) ? "" : reader.GetString(reader.GetOrdinal("PipelineName")),
                            ProductionRoomID = reader.GetInt32(reader.GetOrdinal("ProductionRoomID")),
                            ProductionRoomPlace = reader.IsDBNull(reader.GetOrdinal("ProductionRoomPlace")) ? "" : reader.GetString(reader.GetOrdinal("ProductionRoomPlace"))
                        };

                        results.Add(result);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return results;
        }
    }
}

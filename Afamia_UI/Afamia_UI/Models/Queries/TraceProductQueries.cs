using Microsoft.Data.SqlClient;

namespace Afamia_UI.Models.Queries
{
    public class ProductTraceResult
    {
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

        public List<ProductTraceResult> GetProductionWorkersForProduct(int productId)
        {
            List<ProductTraceResult> results = new List<ProductTraceResult>();

            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();

                    // Complex query to get all workers who worked on this product
                    // The query joins Product -> Production_Pipeline -> Daily_Work_Schedule -> Employee
                    // Filters by:
                    // 1. Employee start time doesn't exceed product end time by more than 30 mins
                    // 2. Employee end time isn't less than product start time by more than 30 mins
                    // 3. Work date matches production date
                    string sqlCommand = @"
                        SELECT DISTINCT
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
                            pr.place AS ProductionRoomPlace
                        FROM Product p
                        INNER JOIN Production_Pipeline pl ON p.Product_Line = pl.ID
                        INNER JOIN Pipeline_Section ps ON pl.ID = ps.Pipeline_ID
                        INNER JOIN Daily_Work_Schedule dws ON ps.ID = dws.SectionID AND ps.Pipeline_ID = dws.Pipeline_ID
                        INNER JOIN Employee e ON dws.EmployeeID = e.Id
                        INNER JOIN Production_Room pr ON pl.Room_ID = pr.ID
                        WHERE p.Id = @ProductId
                          AND p.Start_time IS NOT NULL
                          AND p.End_time IS NOT NULL
                          AND dws.Start_Time <= DATEADD(MINUTE, 30, p.End_time)
                          AND dws.End_Time >= DATEADD(MINUTE, -30, p.Start_time)
                          AND CAST(dws.Work_Date AS DATE) = CAST(p.Production_Date AS DATE)
                        ORDER BY dws.Work_Date, dws.Start_Time, e.FName";

                    SqlCommand cmd = new SqlCommand(sqlCommand, con);
                    cmd.Parameters.AddWithValue("@ProductId", productId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ProductTraceResult result = new ProductTraceResult
                        {
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

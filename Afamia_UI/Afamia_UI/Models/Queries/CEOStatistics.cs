using Afamia_UI.Models.Entities;
using Microsoft.Data.SqlClient;

namespace Afamia_UI.Models.Queries
{
    public class CEODashboardStats
    {
        public int TotalProductsThisMonth { get; set; }
        public int TotalBatchesThisMonth { get; set; }
        public int TotalCustomersThisMonth { get; set; }
        public int ActiveProductionLines { get; set; }
        public int TotalEmployees { get; set; }
        public decimal PercentageChangeProducts { get; set; }
        public decimal PercentageChangeBatches { get; set; }
        public List<TopProductionLine> TopProductionLines { get; set; } = new List<TopProductionLine>();
        public List<MonthlyProduction> Last6MonthsProduction { get; set; } = new List<MonthlyProduction>();
        public List<TopCustomer> TopCustomersThisMonth { get; set; } = new List<TopCustomer>();
        public List<ProductTypeDistribution> ProductTypeBreakdown { get; set; } = new List<ProductTypeDistribution>();
    }

    public class TopProductionLine
    {
        public string LineName { get; set; }
        public int ProductsProduced { get; set; }
        public int BatchesCompleted { get; set; }
    }

    public class MonthlyProduction
    {
        public string MonthName { get; set; }
        public int Year { get; set; }
        public int TotalProducts { get; set; }
        public int TotalBatches { get; set; }
    }

    public class TopCustomer
    {
        public string CustomerName { get; set; }
        public int OrderCount { get; set; }
        public int TotalProducts { get; set; }
    }

    public class ProductTypeDistribution
    {
        public string TypeName { get; set; }
        public int Count { get; set; }
        public decimal Percentage { get; set; }
    }

    public class CEOStatisticsService
    {
        private readonly DB db;

        public CEOStatisticsService(DB db)
        {
            this.db = db;
        }

        public CEODashboardStats GetDashboardStatistics()
        {
            CEODashboardStats stats = new CEODashboardStats();

            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();

                    // Get current month stats
                    string currentMonthQuery = @"
                        DECLARE @CurrentMonthStart DATE = DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1);
                        DECLARE @NextMonthStart DATE = DATEADD(MONTH, 1, @CurrentMonthStart);
                        DECLARE @LastMonthStart DATE = DATEADD(MONTH, -1, @CurrentMonthStart);

                        SELECT
                            COUNT(DISTINCT p.Id) AS TotalProductsThisMonth,
                            COUNT(DISTINCT p.Batch_Id) AS TotalBatchesThisMonth,
                            COUNT(DISTINCT p.customer_id) AS TotalCustomersThisMonth,
                            (SELECT COUNT(DISTINCT Id) FROM Production_Pipeline) AS ActiveProductionLines,
                            (SELECT COUNT(*) FROM Employee WHERE Works_in_Factory = 1) AS TotalEmployees,
                            (SELECT COUNT(DISTINCT Id) FROM Product WHERE Production_Date >= @LastMonthStart AND Production_Date < @CurrentMonthStart) AS ProductsLastMonth,
                            (SELECT COUNT(DISTINCT Batch_Id) FROM Product WHERE Production_Date >= @LastMonthStart AND Production_Date < @CurrentMonthStart) AS BatchesLastMonth
                        FROM Product p
                        WHERE p.Production_Date >= @CurrentMonthStart AND p.Production_Date < @NextMonthStart";

                    SqlCommand cmd = new SqlCommand(currentMonthQuery, con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        stats.TotalProductsThisMonth = reader.GetInt32(0);
                        stats.TotalBatchesThisMonth = reader.GetInt32(1);
                        stats.TotalCustomersThisMonth = reader.GetInt32(2);
                        stats.ActiveProductionLines = reader.GetInt32(3);
                        stats.TotalEmployees = reader.GetInt32(4);

                        int productsLastMonth = reader.GetInt32(5);
                        int batchesLastMonth = reader.GetInt32(6);

                        // Calculate percentage change
                        stats.PercentageChangeProducts = productsLastMonth > 0
                            ? ((decimal)(stats.TotalProductsThisMonth - productsLastMonth) / productsLastMonth) * 100
                            : 0;
                        stats.PercentageChangeBatches = batchesLastMonth > 0
                            ? ((decimal)(stats.TotalBatchesThisMonth - batchesLastMonth) / batchesLastMonth) * 100
                            : 0;
                    }
                    reader.Close();

                    // Get top production lines this month
                    string topLinesQuery = @"
                        DECLARE @CurrentMonthStart DATE = DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1);
                        DECLARE @NextMonthStart DATE = DATEADD(MONTH, 1, @CurrentMonthStart);

                        SELECT TOP 5
                            pl.Name AS LineName,
                            COUNT(p.Id) AS ProductsProduced,
                            COUNT(DISTINCT p.Batch_Id) AS BatchesCompleted
                        FROM Product p
                        INNER JOIN Production_Pipeline pl ON p.Product_Line = pl.ID
                        WHERE p.Production_Date >= @CurrentMonthStart AND p.Production_Date < @NextMonthStart
                        GROUP BY pl.Name
                        ORDER BY COUNT(p.Id) DESC";

                    cmd = new SqlCommand(topLinesQuery, con);
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        stats.TopProductionLines.Add(new TopProductionLine
                        {
                            LineName = reader.GetString(0),
                            ProductsProduced = reader.GetInt32(1),
                            BatchesCompleted = reader.GetInt32(2)
                        });
                    }
                    reader.Close();

                    // Get last 6 months production
                    string sixMonthsQuery = @"
                        SELECT
                            DATENAME(MONTH, Production_Date) AS MonthName,
                            YEAR(Production_Date) AS Year,
                            COUNT(Id) AS TotalProducts,
                            COUNT(DISTINCT Batch_Id) AS TotalBatches
                        FROM Product
                        WHERE Production_Date >= DATEADD(MONTH, -6, GETDATE())
                        GROUP BY YEAR(Production_Date), MONTH(Production_Date), DATENAME(MONTH, Production_Date)
                        ORDER BY YEAR(Production_Date), MONTH(Production_Date)";

                    cmd = new SqlCommand(sixMonthsQuery, con);
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        stats.Last6MonthsProduction.Add(new MonthlyProduction
                        {
                            MonthName = reader.GetString(0),
                            Year = reader.GetInt32(1),
                            TotalProducts = reader.GetInt32(2),
                            TotalBatches = reader.GetInt32(3)
                        });
                    }
                    reader.Close();

                    // Get top customers this month
                    string topCustomersQuery = @"
                        DECLARE @CurrentMonthStart DATE = DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1);
                        DECLARE @NextMonthStart DATE = DATEADD(MONTH, 1, @CurrentMonthStart);

                        SELECT TOP 5
                            ISNULL(CONCAT(c.FName, ' ', c.LName), 'No Customer') AS CustomerName,
                            COUNT(DISTINCT p.Batch_Id) AS OrderCount,
                            COUNT(p.Id) AS TotalProducts
                        FROM Product p
                        LEFT JOIN Customer c ON p.customer_id = c.Id
                        WHERE p.Production_Date >= @CurrentMonthStart AND p.Production_Date < @NextMonthStart
                        GROUP BY CONCAT(c.FName, ' ', c.LName)
                        ORDER BY COUNT(p.Id) DESC";

                    cmd = new SqlCommand(topCustomersQuery, con);
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        stats.TopCustomersThisMonth.Add(new TopCustomer
                        {
                            CustomerName = reader.GetString(0),
                            OrderCount = reader.GetInt32(1),
                            TotalProducts = reader.GetInt32(2)
                        });
                    }
                    reader.Close();

                    // Get product type distribution this month
                    string typeDistributionQuery = @"
                        DECLARE @CurrentMonthStart DATE = DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1);
                        DECLARE @NextMonthStart DATE = DATEADD(MONTH, 1, @CurrentMonthStart);
                        DECLARE @TotalProducts INT = (SELECT COUNT(*) FROM Product WHERE Production_Date >= @CurrentMonthStart AND Production_Date < @NextMonthStart);

                        SELECT
                            pt.Name AS TypeName,
                            COUNT(p.Id) AS Count,
                            CASE WHEN @TotalProducts > 0 THEN (CAST(COUNT(p.Id) AS DECIMAL) / @TotalProducts) * 100 ELSE 0 END AS Percentage
                        FROM Product p
                        INNER JOIN Products_Types pt ON p.Type = pt.Id
                        WHERE p.Production_Date >= @CurrentMonthStart AND p.Production_Date < @NextMonthStart
                        GROUP BY pt.Name
                        ORDER BY COUNT(p.Id) DESC";

                    cmd = new SqlCommand(typeDistributionQuery, con);
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        stats.ProductTypeBreakdown.Add(new ProductTypeDistribution
                        {
                            TypeName = reader.GetString(0),
                            Count = reader.GetInt32(1),
                            Percentage = reader.GetDecimal(2)
                        });
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return stats;
        }
    }
}

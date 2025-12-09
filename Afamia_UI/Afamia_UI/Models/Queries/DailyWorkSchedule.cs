using Afamia_UI.Models.Entities;
using Microsoft.Data.SqlClient;

namespace Afamia_UI.Models.Queries
{
    public class DailyWorkScheduleServices
    {
        private readonly DB db;

        public DailyWorkScheduleServices(DB db)
        {
            this.db = db;
        }

        // Get all schedules for a specific date and pipeline
        public List<DailyWorkSchedule> GetSchedulesByDateAndPipeline(DateTime date, int pipelineId)
        {
            List<DailyWorkSchedule> schedules = new List<DailyWorkSchedule>();

            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql = @"
                        SELECT
                            dws.Schedule_ID,
                            dws.EmployeeID,
                            dws.Work_Date,
                            dws.Start_Time,
                            dws.End_Time,
                            dws.SectionID,
                            dws.Pipeline_ID,
                            (e.FName + ' ' + e.LName) AS EmployeeName,
                            ps.Name AS SectionName,
                            pl.Name AS PipelineName
                        FROM Daily_Work_Schedule dws
                        INNER JOIN Employee e ON dws.EmployeeID = e.Id
                        INNER JOIN Pipeline_Section ps ON dws.SectionID = ps.ID AND dws.Pipeline_ID = ps.Pipeline_ID
                        INNER JOIN Production_Pipeline pl ON dws.Pipeline_ID = pl.ID
                        WHERE CAST(dws.Work_Date AS DATE) = @Date AND dws.Pipeline_ID = @PipelineId
                        ORDER BY dws.Start_Time, e.FName";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@Date", date.Date);
                    cmd.Parameters.AddWithValue("@PipelineId", pipelineId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        DailyWorkSchedule schedule = new DailyWorkSchedule
                        {
                            Schedule_ID = reader.GetInt32(0),
                            EmployeeID = reader.GetInt32(1),
                            Work_Date = reader.GetDateTime(2),
                            Start_Time = reader.IsDBNull(3) ? null : reader.GetTimeSpan(3),
                            End_Time = reader.IsDBNull(4) ? null : reader.GetTimeSpan(4),
                            SectionID = reader.GetInt32(5),
                            Pipeline_ID = reader.GetInt32(6),
                            EmployeeName = reader.GetString(7),
                            SectionName = reader.GetString(8),
                            PipelineName = reader.GetString(9)
                        };
                        schedules.Add(schedule);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return schedules;
        }

        // Get schedule by ID
        public DailyWorkSchedule GetScheduleById(int scheduleId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql = @"
                        SELECT
                            dws.Schedule_ID,
                            dws.EmployeeID,
                            dws.Work_Date,
                            dws.Start_Time,
                            dws.End_Time,
                            dws.SectionID,
                            dws.Pipeline_ID,
                            (e.FName + ' ' + e.LName) AS EmployeeName,
                            ps.Name AS SectionName,
                            pl.Name AS PipelineName
                        FROM Daily_Work_Schedule dws
                        INNER JOIN Employee e ON dws.EmployeeID = e.Id
                        INNER JOIN Pipeline_Section ps ON dws.SectionID = ps.ID AND dws.Pipeline_ID = ps.Pipeline_ID
                        INNER JOIN Production_Pipeline pl ON dws.Pipeline_ID = pl.ID
                        WHERE dws.Schedule_ID = @ScheduleId";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@ScheduleId", scheduleId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        return new DailyWorkSchedule
                        {
                            Schedule_ID = reader.GetInt32(0),
                            EmployeeID = reader.GetInt32(1),
                            Work_Date = reader.GetDateTime(2),
                            Start_Time = reader.IsDBNull(3) ? null : reader.GetTimeSpan(3),
                            End_Time = reader.IsDBNull(4) ? null : reader.GetTimeSpan(4),
                            SectionID = reader.GetInt32(5),
                            Pipeline_ID = reader.GetInt32(6),
                            EmployeeName = reader.GetString(7),
                            SectionName = reader.GetString(8),
                            PipelineName = reader.GetString(9)
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        // Add new schedule (assign employee to section)
        public void AddSchedule(DailyWorkSchedule schedule)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql = @"
                        INSERT INTO Daily_Work_Schedule (EmployeeID, Work_Date, Start_Time, End_Time, SectionID, Pipeline_ID)
                        VALUES (@EmployeeID, @Work_Date, @Start_Time, @End_Time, @SectionID, @Pipeline_ID)";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@EmployeeID", schedule.EmployeeID);
                    cmd.Parameters.AddWithValue("@Work_Date", schedule.Work_Date.Date);
                    cmd.Parameters.AddWithValue("@Start_Time", (object)schedule.Start_Time ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@End_Time", (object)schedule.End_Time ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@SectionID", schedule.SectionID);
                    cmd.Parameters.AddWithValue("@Pipeline_ID", schedule.Pipeline_ID);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        // Update schedule (edit start/end times or section)
        public void UpdateSchedule(DailyWorkSchedule schedule)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql = @"
                        UPDATE Daily_Work_Schedule
                        SET EmployeeID = @EmployeeID,
                            Work_Date = @Work_Date,
                            Start_Time = @Start_Time,
                            End_Time = @End_Time,
                            SectionID = @SectionID,
                            Pipeline_ID = @Pipeline_ID
                        WHERE Schedule_ID = @Schedule_ID";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@Schedule_ID", schedule.Schedule_ID);
                    cmd.Parameters.AddWithValue("@EmployeeID", schedule.EmployeeID);
                    cmd.Parameters.AddWithValue("@Work_Date", schedule.Work_Date.Date);
                    cmd.Parameters.AddWithValue("@Start_Time", (object)schedule.Start_Time ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@End_Time", (object)schedule.End_Time ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@SectionID", schedule.SectionID);
                    cmd.Parameters.AddWithValue("@Pipeline_ID", schedule.Pipeline_ID);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        // Delete schedule
        public bool DeleteSchedule(int scheduleId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql = "DELETE FROM Daily_Work_Schedule WHERE Schedule_ID = @Schedule_ID";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@Schedule_ID", scheduleId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // Get available employees (those working in factory)
        public List<Employee> GetAvailableEmployees()
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql = "SELECT Id, FName, LName, Role FROM Employee WHERE Works_in_Factory = 1 ORDER BY FName, LName";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Employee emp = new Employee
                        {
                            Id = reader.GetInt32(0),
                            FName = reader.GetString(1),
                            LName = reader.GetString(2),
                            role = reader.GetString(3)
                        };
                        employees.Add(emp);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return employees;
        }

        // Get sections for a specific pipeline
        public List<PipelineSection> GetSectionsByPipeline(int pipelineId)
        {
            List<PipelineSection> sections = new List<PipelineSection>();

            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql = "SELECT ID, Pipeline_ID, Name FROM Pipeline_Section WHERE Pipeline_ID = @PipelineId ORDER BY ID";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@PipelineId", pipelineId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        PipelineSection section = new PipelineSection
                        {
                            ID = reader.GetInt32(0),
                            Pipeline_ID = reader.GetInt32(1),
                            Name = reader.GetString(2)
                        };
                        sections.Add(section);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return sections;
        }
    }
}

using Afamia_UI.Models.Entities;
using Microsoft.Data.SqlClient;

namespace Afamia_UI.Models.Queries
{
    public class PipelineSectionServices
    {
        private readonly DB db;
        public PipelineSectionServices(DB db)
        {
            this.db = db;
        }

        public List<PipelineSection> GetPipelineSectionsByPipelineId(int pipelineId)
        {
            List<PipelineSection> pipelineSections = new List<PipelineSection>();
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sqlCommand = "Select * from Pipeline_Section where Pipeline_ID = @Pipeline_ID";
                    SqlCommand cmd = new SqlCommand(sqlCommand, con);
                    cmd.Parameters.AddWithValue("@Pipeline_ID", pipelineId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        PipelineSection section = new PipelineSection()
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Pipeline_ID = Convert.ToInt32(reader["Pipeline_ID"]),
                            Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? "" : reader.GetString(reader.GetOrdinal("Name"))
                        };
                        pipelineSections.Add(section);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return pipelineSections;
        }

        public PipelineSection GetPipelineSectionById(int id, int pipelineId)
        {
            PipelineSection pipelineSection = new PipelineSection();
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql = "Select * from Pipeline_Section where ID = @id and Pipeline_ID = @Pipeline_ID";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@Pipeline_ID", pipelineId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    PipelineSection section = new PipelineSection
                    {
                        ID = reader.GetInt32(0),
                        Pipeline_ID = reader.GetInt32(1),
                        Name = reader.IsDBNull(2) ? "" : reader.GetString(2)
                    };
                    return section;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public void AddPipelineSection(PipelineSection pipelineSection)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();

                    string sql = "INSERT INTO Pipeline_Section (Pipeline_ID, Name) VALUES (@Pipeline_ID, @Name)";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@Pipeline_ID", pipelineSection.Pipeline_ID);
                    cmd.Parameters.AddWithValue("@Name", pipelineSection.Name);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void EditPipelineSection(PipelineSection pipelineSection, int oldId, int oldPipelineId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql = "Update Pipeline_Section Set ID = @NewID, Pipeline_ID = @NewPipeline_ID, Name = @Name Where ID = @OldID and Pipeline_ID = @OldPipeline_ID";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@NewID", pipelineSection.ID);
                    cmd.Parameters.AddWithValue("@NewPipeline_ID", pipelineSection.Pipeline_ID);
                    cmd.Parameters.AddWithValue("@Name", pipelineSection.Name);
                    cmd.Parameters.AddWithValue("@OldID", oldId);
                    cmd.Parameters.AddWithValue("@OldPipeline_ID", oldPipelineId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Delete(int id, int pipelineId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(db.GetConnectionString()))
                {
                    con.Open();
                    string sql = "Delete From Pipeline_Section where ID = @ID and Pipeline_ID = @Pipeline_ID";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@Pipeline_ID", pipelineId);

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

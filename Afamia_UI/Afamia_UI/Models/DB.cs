using Microsoft.Data.SqlClient;

namespace Afamia_UI.Models
{
    public class DB
    {
        private string ConnectionString = "Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog = AfamiaFactoryDB; Integrated Security = True; Trust Server Certificate = True;";
       
        public string GetConnectionString()
        {
            return ConnectionString;
        }
    }
}

namespace Afamia_UI.Models.Entities
{
    
    public class Employee
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string role { get; set; }
        public bool Works_in_Factory { get; set; }
        public List<string> Phone { get; set; } 

        public Employee() { 
            Phone = new List<string>();
        }

    }
}

namespace Afamia_UI.Models.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Name { get; set; }
        public List<string> Phone { get; set; }

        public Customer()
        {
            Phone = new List<string>();
        }
    }
}

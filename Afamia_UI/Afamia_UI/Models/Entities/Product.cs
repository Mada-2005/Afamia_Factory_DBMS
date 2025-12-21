namespace Afamia_UI.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime ProductionDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public int Production_Line { get; set; }

        public int Customer_Id { get; set; }
        public int type { get; set; }

        public int? weight { get; set; }
        public TimeSpan? Start_time { get; set; }
        public TimeSpan? End_time { get; set; }

        // Extra attribute to facilitate batch creation (not in DB)
        public int Quantity { get; set; }
    }
}

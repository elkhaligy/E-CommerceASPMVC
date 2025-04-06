namespace Project.Models
{
    public class AdminActionLog
    {
        public int Id { get; set; }
        public int AdminId { get; set; }
        public Admin Admin { get; set; }
        public string Action { get; set; } 
        public DateTime ActionTime { get; set; }
    }

}

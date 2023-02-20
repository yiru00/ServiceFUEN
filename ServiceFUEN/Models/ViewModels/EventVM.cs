namespace ServiceFUEN.Models.ViewModels
{
    public class EventVM
    {
        public int Id { get; set; }
        public string? EventName { get; set; }
        public string? Photo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

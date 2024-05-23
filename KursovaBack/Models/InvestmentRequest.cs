namespace project_back.Models
{
    public class InvestmentRequest
    {
        public Guid Id { get; set; }
        public Guid UserID { get; set; }
        public Guid ProjectId { get; set; }
        public string Message { get; set; }
        public string ContactData {  get; set; }
        public string Status { get; set; }
    }
}

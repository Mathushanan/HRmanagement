namespace HRManagement.Domain.Entities
{
    public class RequestAudit
    {
        public required int Id { get; set; }
        public required string RequestPath { get; set; }
        public required string RequestMethod { get; set; }
        public required string RequestBody { get; set; }
        public required string ResponseBody { get; set; }
        public required DateTime Timestamp { get; set; }
    }
}

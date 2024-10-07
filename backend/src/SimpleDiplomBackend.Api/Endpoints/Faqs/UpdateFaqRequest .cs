namespace SimpleDiplomBackend.Api.Endpoints.Faqs
{
    public class UpdateFaqRequest
    {
        public int Id { get; set; }
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
    }
}
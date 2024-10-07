namespace SimpleDiplomBackend.Api.Endpoints.Faqs
{
    public class CreateFaqRequest
    {
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
    }
}
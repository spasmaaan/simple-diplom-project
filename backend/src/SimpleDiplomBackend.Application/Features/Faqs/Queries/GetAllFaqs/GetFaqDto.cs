namespace SimpleDiplomBackend.Application.Features.Faqs.Queries.GetAllFaqs
{
    public class GetFaqDto
    {
        public int Id { get; set; }
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
    }
}
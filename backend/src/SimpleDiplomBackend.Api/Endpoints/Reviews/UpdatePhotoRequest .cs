namespace SimpleDiplomBackend.Api.Endpoints.Reviews
{
    public class UpdateReviewRequest
    {
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public short? Rating { get; set; }
    }
}
namespace SimpleDiplomBackend.Api.Endpoints.Reviews
{
    public class CreateReviewRequest
    {
        public string Message { get; set; } = string.Empty;
        public short? Rating { get; set; }
    }
}
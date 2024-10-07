namespace SimpleDiplomBackend.Domain.Entities
{
    public class ApplicationOption
    {
        public required string Id { get; set; }
        public string Value { get; set; } = string.Empty;
    }
}

namespace SimpleDiplomBackend.Api.Endpoints.Photos
{
    public class UpdatePhotoRequest
    {
        public int Id { get; set; }
        public string Image { get; set; } = string.Empty;
    }
}
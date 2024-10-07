namespace SimpleDiplomBackend.Api.Endpoints.Photos
{
    public class UpdatePhotoRequest
    {
        public int Id { get; set; }
        public byte[]? Image { get; set; }
    }
}
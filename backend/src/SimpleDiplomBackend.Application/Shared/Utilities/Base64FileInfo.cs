using System.Text.RegularExpressions;

namespace SimpleDiplomBackend.Application.Shared.Utilities
{
    public record class Base64FileInfo
    {
        private const int Base64TextPartsCount = 2;
        private const char Base64TextPartsSeparator = ',';
        private const string Base64TextMetadataPattern = "^data:(\\w+/\\w+);base64$";
        private const string DefaultMimeType = "application/octet-stream";


        public string Base64 { get; private set; }
        public string MimeType { get; private set; }
        public byte[] Data { get; private set; } = { };

        public Base64FileInfo(string base64)
        {
            Base64 = base64;

            string[]? dataParts = GetDataParts(Base64);
            MimeType = GetMimeType(dataParts);
            Data = GetData(dataParts);
        }

        private string[]? GetDataParts(string base64)
        {
            string[] dataParts = Base64.Split(Base64TextPartsSeparator);
            return dataParts.Count() == Base64TextPartsCount ? dataParts : null;
        }

        private byte[] GetData(string[]? dataParts)
        {
            return dataParts != null 
                ? Convert.FromBase64String(dataParts.Last())
                : Array.Empty<byte>();
        }

        private string GetMimeType(string[]? dataParts)
        {
            if (dataParts == null)
            {
                return DefaultMimeType;
            }
            string metadata = dataParts.First();
            Match match = new Regex(Base64TextMetadataPattern).Match(metadata);
            if (!match.Success)
            {
                return DefaultMimeType;
            }
            var mimeType = match.Groups[1];
            return mimeType.Success 
                ? mimeType.Value 
                : DefaultMimeType;
        }
    }
}

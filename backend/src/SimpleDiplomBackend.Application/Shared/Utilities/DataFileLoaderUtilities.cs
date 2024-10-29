using Azure.Core;
using Microsoft.AspNetCore.StaticFiles;
using System.Reflection;

namespace SimpleDiplomBackend.Application.Shared.Utilities
{
    public static class DataFileLoaderUtilities
    {
        private static readonly string ProjectPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;


        private static string GetFilePath(string filePath)
        {
            return Path.Combine(ProjectPath, filePath);
        }

        public static byte[] GetFileData(string filePath)
        {
            string fullPath = GetFilePath(filePath);
            return File.ReadAllBytes(fullPath);
        }

        public static string GetFileInBase64(string filePath)
        {
            byte[] fileData = GetFileData(filePath);
            return Convert.ToBase64String(fileData);
        }

        public static string GetFileMimeType(string fileName)
        {
            string contentType = "application/octet-stream";
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType!);
            return contentType;
        }
    }
}

using System;
using System.Text;
using WebApplication.Infrastructure.Interfaces;

namespace WebApplication.Infrastructure
{
    public class ImageService : IImageService
    {
        public string EncodeToBase64(string toEncode)
        {
            byte[] toEncodeAsBytes = Encoding.ASCII.GetBytes(toEncode);
            return Convert.ToBase64String(toEncodeAsBytes);
        }

        public string EncodeToBase64(byte[] toEncodeAsBytes)
        {
            return Convert.ToBase64String(toEncodeAsBytes);
        }

        public byte[] DecodeFromBase64(string encodedData)
        {
            return Convert.FromBase64String(encodedData);
        }
    }
}

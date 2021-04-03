namespace WebApplication.Infrastructure.Interfaces
{
    public interface IImageService
    {
        string EncodeToBase64(string toEncode);
        string EncodeToBase64(byte[] toEncodeAsBytes);
        byte[] DecodeFromBase64(string encodedData);
    }
}

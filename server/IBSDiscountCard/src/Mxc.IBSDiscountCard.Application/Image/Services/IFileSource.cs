namespace Mxc.IBSDiscountCard.Application.Image.Services
{
    public interface IFileSource
    {
        string ContentType { get; set; }
        byte[] Content { get; set; }
    }
}
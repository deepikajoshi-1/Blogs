namespace Bloggie.Web.Repositries
{
    public interface IImageRepository
    {
        Task<string> UploadAsync(IFormFile formFile);
    }
}

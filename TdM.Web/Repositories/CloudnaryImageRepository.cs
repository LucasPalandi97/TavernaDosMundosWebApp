using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace TdM.Web.Repositories;

public class CloudnaryImageRepository : IImageRepository
{
    private readonly IConfiguration configuration;
    private readonly Account account;
    public CloudnaryImageRepository(IConfiguration configuration)
    {
        //Configfure a Cloudinary Account gettin AppSettings Info
        this.configuration = configuration;
        account = new Account(
            configuration.GetSection("Cloudinary")["CloudName"],
            configuration.GetSection("Cloudinary")["ApiKey"],
            configuration.GetSection("Cloudinary")["ApiSecret"]);
    }
    public async Task<string> UploadAsync(IFormFile file)
    {
        var client = new Cloudinary(account);
        // Upload
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.FileName, file.OpenReadStream()),
            DisplayName = file.FileName
        };
        var uploadResult = await client.UploadAsync(uploadParams);

        if (uploadResult != null && uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return uploadResult.SecureUrl.ToString();
        }
#pragma warning disable CS8603 // Possible null reference return.
        return null;
#pragma warning restore CS8603 // Possible null reference return.
    }
}

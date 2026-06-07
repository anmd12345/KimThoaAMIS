using ManagementKimThoa.Constants;
using ManagementKimThoa.Models;
using ManagementKimThoa.Services.Interfaces;

namespace ManagementKimThoa.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;


        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string typeUpload)
        {
            if (file != null)
            {
                string folder = "";

                if (typeUpload == TypeUploadFileConstant.Avatar)
                {
                    folder = Path.Combine(
                    _env.WebRootPath,
                    "assets",
                    "uploads",
                    "avatars");
                }
                else if (typeUpload == TypeUploadFileConstant.FileScan)
                {
                    folder = Path.Combine(_env.ContentRootPath, "Privates", "Profiles");
                }else if(typeUpload == TypeUploadFileConstant.Product)
                {
                    folder = Path.Combine(_env.WebRootPath, "assets", "uploads", "products");
                }
                else if(typeUpload == TypeUploadFileConstant.Gift)
                {
                    folder = Path.Combine(_env.WebRootPath, "assets", "uploads", "gifts");
                }

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

                string filePath = Path.Combine(folder, fileName);

                await using var stream = new FileStream(filePath, FileMode.Create);

                await file.CopyToAsync(stream);

                return fileName;
            }

            return "";
        }
    }
}


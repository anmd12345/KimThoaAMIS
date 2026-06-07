using System;
namespace ManagementKimThoa.Services.Interfaces
{
	public interface IFileService
	{
		Task<string> UploadFileAsync(IFormFile file, string typeUpload);

        Task<string> UploadFileAsync(IFormFile file, string typeUpload, string userCode);
    }
}


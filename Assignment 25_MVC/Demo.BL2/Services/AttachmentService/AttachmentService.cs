using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL2.Services.AttachmentService
{
	public class AttachmentService : IAttachmentService
	{
		List<string> AllowedExtension = [".jpg",".jpeg",".png"];
		const int MaxAllowedSizeInBytes = 2 * 1024 * 1024; // 2 MB
		public bool DeleteAttachment(string attachmentPath)
		{
			if (File.Exists(attachmentPath))
			{
				File.Delete(attachmentPath);
				return true;
			}else
			{
				return false;
			}
		}

		public string? UploadAttachment(IFormFile File, string attachmentFolder)
		{
			// check extension and size
			if (!AllowedExtension.Contains(Path.GetExtension(File.FileName))) { return null; }
			if(File.Length > MaxAllowedSizeInBytes || File.Length==0) { return null; }
			// get path and save
			var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files",attachmentFolder);
			var FileName = $"{Guid.NewGuid()}_{File.FileName}";
			var filePath = Path.Combine(folderPath, FileName);
			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				File.CopyTo(stream);
			}
			return FileName;
		}
	}
}

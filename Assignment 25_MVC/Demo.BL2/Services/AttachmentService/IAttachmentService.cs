using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL2.Services.AttachmentService
{
	public interface IAttachmentService
	{
		public string? UploadAttachment(IFormFile File, string attachmentFolder);
		public bool DeleteAttachment(string attachmentPath);
	}
}

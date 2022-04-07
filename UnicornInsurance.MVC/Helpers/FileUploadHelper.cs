using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Contracts.Helpers;

namespace UnicornInsurance.MVC.Helpers
{
    public class FileUploadHelper : IFileUploadHelper
    {
        public void UploadImageFile(IFormFileCollection files, string uploads, string fileName, string extension)
        {
            using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
            {
                files[0].CopyTo(filesStreams);
            }
        }
    }
}

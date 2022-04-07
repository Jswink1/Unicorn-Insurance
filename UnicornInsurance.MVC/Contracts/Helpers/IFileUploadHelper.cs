using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnicornInsurance.MVC.Contracts.Helpers
{
    public interface IFileUploadHelper
    {
        void UploadImageFile(IFormFileCollection files, string uploads, string fileName, string extension);
    }
}

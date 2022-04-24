using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Models;

namespace UnicornInsurance.MVC.Contracts
{
    public interface IBlobService
    {
        Task UploadFileBlobAsync(FormFile file);
        Task DeleteBlobAsync(string blobName);

    }
}

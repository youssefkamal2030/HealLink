using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healLink.Application.Interfaces
{
    public interface IPhotoService
    {
        Task<string> SavePhotoAsync(IFormFile file, string folderName);
    }

}

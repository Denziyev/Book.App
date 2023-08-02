using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Service.Extentions
{
    public static class FileUpload
    {
        public static bool isImage(this IFormFile file)
        {
            return file.ContentType.Contains("image");
        }

        public static bool isSizeOk(this IFormFile file,int mb)
        {
            return file.Length / 1024 / 1024 <= mb;
        }

        public static string CreateImage(this IFormFile file, string root, string path)
        {
            string FileName = Guid.NewGuid().ToString() + file.FileName;
            string FullPath = Path.Combine(root, path, FileName);

            using (FileStream strem = new FileStream(FullPath, FileMode.Create))
            {
                file.CopyTo(strem);
            }
            return FileName;
        }
    }
}

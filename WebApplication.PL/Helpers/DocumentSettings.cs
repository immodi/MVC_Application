using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace WebApplication.PL.Helpers
{
    public class DocumentSettings
    {
        public static string UploadImage(IFormFile file, string folderName)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);

            string fileName = String.Empty;

            if (file is null)
                fileName = $"{Guid.NewGuid()}{String.Empty}";
            else
            {
                fileName = $"{Guid.NewGuid()}{file.FileName}";
                string filePath = Path.Combine(folderPath, fileName);
                using (var fs = new FileStream(filePath, FileMode.Create))
                    file.CopyTo(fs);
            }

            return fileName;
        } 

        public static void DeleteImage(string fileName, string folderName)
        {
            if (fileName is not null && folderName is not null)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName, fileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                } 
            }
        }
    }
}

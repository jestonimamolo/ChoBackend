using System.Web;

namespace choapi.Helper
{
    public class UploadHelper
    {
        public const string _contentFilesPath = "Content\\Files";
        public static async Task<string> SaveFile(IFormFile file, int id)
        {
            try
            {
                string fileName = file.FileName.Replace(" ", "_");
                string ext = Path.GetExtension(fileName);

                string fileNameExt = $"{fileName}-{id}";

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), _contentFilesPath);

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                var targetPath = Path.Combine(Directory.GetCurrentDirectory(), _contentFilesPath, fileName);

                using (var stream = new FileStream(targetPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return targetPath;
            }
            catch(Exception ex) 
            {
                return $"Error: {ex.Message}";
            }
        }
    }

    
}

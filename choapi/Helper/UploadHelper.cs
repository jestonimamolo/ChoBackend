using System.Web;

namespace choapi.Helper
{
    public class UploadHelper
    {
        public const string _contentFilesPath = "Content/Files";
        public const string _contentDirectoryFilesPath = "Content\\Files";

        public static async Task<string> SaveFile(IFormFile? file, int id, string from)
        {
            try
            {
                if (file == null)
                {
                    return "";
                }

                string fileName = file.FileName.Replace(" ", "_");
                string ext = Path.GetExtension(fileName);

                string toSaveFileName = $"{from}-{id}-{fileName}";

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), _contentDirectoryFilesPath);

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                var targetPath = Path.Combine(Directory.GetCurrentDirectory(), _contentDirectoryFilesPath, toSaveFileName);

                using (var stream = new FileStream(targetPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return _contentFilesPath + "/" + toSaveFileName;
            }
            catch(Exception ex) 
            {
                return $"Error: {ex.Message}";
            }
        }

        public static string DeleteFile(string path)
        {
            try
            {
                string[] pathOfFile = path.Split("/");

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"{_contentDirectoryFilesPath}\\{pathOfFile[pathOfFile.Length - 1]}");

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                return "";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }

    
}

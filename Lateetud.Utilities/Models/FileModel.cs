
namespace Lateetud.Utilities.Models
{
    public class FileModel
    {
        public FileModel()
        {
            Status = PStatus.None;
            StatusText = "Nothing";
        }

        public string OriginalFileName { get; set; }
        public string UploadedFileName { get; set; }
        public string DirectoryPath { get; set; }
        public string FilePath { get; set; }
        public string FileContent { get; set; }
        public PStatus Status { get; set; }
        public string StatusText { get; set; }
    }
}

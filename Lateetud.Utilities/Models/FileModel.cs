
using System;
using System.Collections.Generic;

namespace Lateetud.Utilities.Models
{
    public class FileModel
    {
        public FileModel()
        {
            Status = PStatus.None;
            StatusText = "Nothing";
            UploadTime = "";
            ExecutionTime = "";
        }

        public string OriginalFileName { get; set; }
        public string UploadedFileName { get; set; }
        public string DirectoryPath { get; set; }
        public string FilePath { get; set; }
        public string FileContent { get; set; }
        public PStatus Status { get; set; }
        public string StatusText { get; set; }

        public TimeSpan UploadTimeSpan { get; set; }
        public string UploadTime { get; set; }
        public TimeSpan ExecutionTimeSpan { get; set; }
        public string ExecutionTime { get; set; }
        public TimeSpan TotalExecutionTimeSpan { get; set; }
        public string TotalExecutionTime { get; set; }
    }
    public class FileModelList
    {
        public FileModelList() { TotalProcessTime = ""; }
        public List<FileModel> FileModels { get; set; }
        public TimeSpan TotalProcessTimeSpan { get; set; }
        public string TotalProcessTime { get; set; }
    }

    
}

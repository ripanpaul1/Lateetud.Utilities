using Lateetud.Utilities.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace Lateetud.Utilities
{
    public class GeneralService
    {
        public List<FileModel> UploadFiles(HttpPostedFileBase[] files, string DestinationPath)
        {
            List<FileModel> fileModels = new List<FileModel>();
            if (files == null) return null;
            if (files.Length > 0)
            {
                string CurrDir = DefaultDirectoryName();
                CreateDirectory(Path.Combine(DestinationPath, CurrDir));
                foreach (var file in files)
                {
                    FileModel fileModel = new FileModel()
                    {
                        OriginalFileName = Path.GetFileName(file.FileName),
                        UploadedFileName = CurrDir + "-" + Path.GetFileName(file.FileName),
                        FilePath = Path.Combine(DestinationPath, CurrDir, CurrDir + "-" + Path.GetFileName(file.FileName)),
                        Status = PStatus.None
                    };
                    file.SaveAs(fileModel.FilePath);
                    fileModels.Add(fileModel);
                }
            }
            return fileModels;
        }
        public List<FileModel> UploadFiles(HttpFileCollectionBase files, string DestinationPath)
        {
            List<FileModel> fileModels = new List<FileModel>();
            if (files == null) return null;
            if (files.Count > 0)
            {
                string CurrDir = DefaultDirectoryName();
                CreateDirectory(Path.Combine(DestinationPath, CurrDir));
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];
                    FileModel fileModel = new FileModel()
                    {
                        OriginalFileName = Path.GetFileName(file.FileName),
                        UploadedFileName = CurrDir + "-" + Path.GetFileName(file.FileName),
                        DirectoryPath = Path.Combine(DestinationPath, CurrDir),
                        FilePath = Path.Combine(DestinationPath, CurrDir, CurrDir + "-" + Path.GetFileName(file.FileName)),
                        Status = PStatus.None
                    };
                    file.SaveAs(fileModel.FilePath);
                    fileModels.Add(fileModel);
                }   
            }
            return fileModels;
        }

        public string DefaultDirectoryName()
        {
            return DateTime.Now.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss");
        }

        public void CreateDirectory(string DirectoryPath)
        {
            if (!Directory.Exists(DirectoryPath)) Directory.CreateDirectory(DirectoryPath);
        }

        public void DeleteDirectory(string DirectoryPath)
        {
            if (!Directory.Exists(DirectoryPath)) Directory.Delete(DirectoryPath);
        }

        public string ReplaceXmlLimitations(string data)
        {
            data = data.Replace("&", "and");
            return data;
        }
    }
}

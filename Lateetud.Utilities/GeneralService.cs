using Lateetud.Utilities.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
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
                string TheDirectoryPath = Path.Combine(DestinationPath, CurrDir);
                CreateDirectory(TheDirectoryPath);
                foreach (var file in files)
                {
                    string TheFilePath = Path.Combine(TheDirectoryPath, Path.GetFileName(file.FileName));
                    if (Path.GetExtension(file.FileName).Contains(".rar"))
                    {
                        DateTime dtStart = DateTime.Now;
                        FileModel TheFileModelRar = new FileModel()
                        {
                            OriginalFileName = Path.GetFileName(file.FileName),
                            UploadedFileName = Path.GetFileName(file.FileName),
                            DirectoryPath = TheDirectoryPath,
                            FilePath = "",
                            Status = PStatus.Error,
                            StatusText = "Invalid Format"
                        };
                        DateTime dtEnd = DateTime.Now;
                        TheFileModelRar.UploadTimeSpan = dtEnd.Subtract(dtStart);
                        if (StaticUtilities.ProcessTimeFormat == PTime.Ticks)
                            TheFileModelRar.UploadTime = TheFileModelRar.UploadTimeSpan.Seconds.ToString("00") + ":" + TheFileModelRar.UploadTimeSpan.Ticks.ToString();
                        else if (StaticUtilities.ProcessTimeFormat == PTime.Milliseconds)
                            TheFileModelRar.UploadTime = TheFileModelRar.UploadTimeSpan.Seconds.ToString("00") + ":" + TheFileModelRar.UploadTimeSpan.Milliseconds.ToString("000");
                        fileModels.Add(TheFileModelRar);
                        continue;
                    }
                    else if (Path.GetExtension(file.FileName).Contains(".xml"))
                    {
                        DateTime dtStart = DateTime.Now;
                        file.SaveAs(TheFilePath);
                        FileModel TheFileModelXml = new FileModel()
                        {
                            OriginalFileName = Path.GetFileName(file.FileName),
                            UploadedFileName = Path.GetFileName(file.FileName),
                            DirectoryPath = TheDirectoryPath,
                            FilePath = TheFilePath,
                            Status = PStatus.None,
                            StatusText = "Uploaded"
                        };
                        DateTime dtEnd = DateTime.Now;
                        TheFileModelXml.UploadTimeSpan = dtEnd.Subtract(dtStart);
                        if (StaticUtilities.ProcessTimeFormat == PTime.Ticks)
                            TheFileModelXml.UploadTime = TheFileModelXml.UploadTimeSpan.Seconds.ToString("00") + ":" + TheFileModelXml.UploadTimeSpan.Ticks.ToString();
                        else if (StaticUtilities.ProcessTimeFormat == PTime.Milliseconds)
                            TheFileModelXml.UploadTime = TheFileModelXml.UploadTimeSpan.Seconds.ToString("00") + ":" + TheFileModelXml.UploadTimeSpan.Milliseconds.ToString("000");
                        fileModels.Add(TheFileModelXml);
                    }
                    else if (Path.GetExtension(file.FileName).Contains(".zip"))
                    {
                        DateTime dtStart = DateTime.Now;
                        file.SaveAs(TheFilePath);
                        using (var zipFile = ZipFile.OpenRead(TheFilePath))
                        {
                            VMZipFile TheVMZipFile = StaticUtilities.ZipFileInfo(zipFile);
                            if (TheVMZipFile == null)
                            {
                                FileModel TheFileModelZip1 = new FileModel()
                                {
                                    OriginalFileName = Path.GetFileName(file.FileName),
                                    UploadedFileName = Path.GetFileName(file.FileName),
                                    DirectoryPath = TheDirectoryPath,
                                    FilePath = TheFilePath,
                                    Status = PStatus.Error,
                                    StatusText = "Invalid Data"
                                };
                                DateTime dtEnd = DateTime.Now;
                                TheFileModelZip1.UploadTimeSpan = dtEnd.Subtract(dtStart);
                                if (StaticUtilities.ProcessTimeFormat == PTime.Ticks)
                                    TheFileModelZip1.UploadTime = TheFileModelZip1.UploadTimeSpan.Seconds.ToString("00") + ":" + TheFileModelZip1.UploadTimeSpan.Ticks.ToString();
                                else if (StaticUtilities.ProcessTimeFormat == PTime.Milliseconds)
                                    TheFileModelZip1.UploadTime = TheFileModelZip1.UploadTimeSpan.Seconds.ToString("00") + ":" + TheFileModelZip1.UploadTimeSpan.Milliseconds.ToString("000");
                                fileModels.Add(TheFileModelZip1);
                                continue;
                            }
                            zipFile.ExtractToDirectory(TheDirectoryPath);

                            string TheExtractedPath = TheDirectoryPath;
                            if (TheVMZipFile.ExtractedFolderName != null)
                                TheExtractedPath = Path.Combine(TheDirectoryPath, TheVMZipFile.ExtractedFolderName);

                            DirectoryInfo TheDirectoryInfo = new DirectoryInfo(TheExtractedPath);
                            FileInfo[] TheFileInfo = TheDirectoryInfo.GetFiles("*.xml");
                            if (TheFileInfo == null)
                            {
                                FileModel TheFileModelFileInfo1 = new FileModel()
                                {
                                    OriginalFileName = Path.GetFileName(file.FileName),
                                    UploadedFileName = Path.GetFileName(file.FileName),
                                    DirectoryPath = TheDirectoryPath,
                                    FilePath = TheFilePath,
                                    Status = PStatus.Error,
                                    StatusText = "Invalid Data"
                                };
                                DateTime dtEnd = DateTime.Now;
                                TheFileModelFileInfo1.UploadTimeSpan = dtEnd.Subtract(dtStart);
                                if (StaticUtilities.ProcessTimeFormat == PTime.Ticks)
                                    TheFileModelFileInfo1.UploadTime = TheFileModelFileInfo1.UploadTimeSpan.Seconds.ToString("00") + ":" + TheFileModelFileInfo1.UploadTimeSpan.Ticks.ToString();
                                else if (StaticUtilities.ProcessTimeFormat == PTime.Milliseconds)
                                    TheFileModelFileInfo1.UploadTime = TheFileModelFileInfo1.UploadTimeSpan.Seconds.ToString("00") + ":" + TheFileModelFileInfo1.UploadTimeSpan.Milliseconds.ToString("000");
                                fileModels.Add(TheFileModelFileInfo1);
                                continue;
                            }
                            if (TheFileInfo.Length == 0)
                            {
                                FileModel TheFileModelFileInfo2 = new FileModel()
                                {
                                    OriginalFileName = Path.GetFileName(file.FileName),
                                    UploadedFileName = Path.GetFileName(file.FileName),
                                    DirectoryPath = TheDirectoryPath,
                                    FilePath = TheFilePath,
                                    Status = PStatus.Error,
                                    StatusText = "Invalid Data"
                                };
                                DateTime dtEnd = DateTime.Now;
                                TheFileModelFileInfo2.UploadTimeSpan = dtEnd.Subtract(dtStart);
                                if (StaticUtilities.ProcessTimeFormat == PTime.Ticks)
                                    TheFileModelFileInfo2.UploadTime = TheFileModelFileInfo2.UploadTimeSpan.Seconds.ToString("00") + ":" + TheFileModelFileInfo2.UploadTimeSpan.Ticks.ToString();
                                else if (StaticUtilities.ProcessTimeFormat == PTime.Milliseconds)
                                    TheFileModelFileInfo2.UploadTime = TheFileModelFileInfo2.UploadTimeSpan.Seconds.ToString("00") + ":" + TheFileModelFileInfo2.UploadTimeSpan.Milliseconds.ToString("000");
                                fileModels.Add(TheFileModelFileInfo2);
                                continue;
                            }

                            DateTime dtEndZip = DateTime.Now;
                            TimeSpan TheZipUploadTime = new TimeSpan(dtEndZip.Subtract(dtStart).Ticks / TheFileInfo.Length);
                            foreach (FileInfo TheFile in TheFileInfo)
                            {
                                DateTime dtStartZip = DateTime.Now;
                                FileModel fileModelZip = new FileModel()
                                {
                                    OriginalFileName = TheFile.Name,
                                    UploadedFileName = TheFile.Name,
                                    DirectoryPath = TheDirectoryPath,
                                    FilePath = TheFile.FullName,
                                    Status = PStatus.None
                                };
                                DateTime dtEnd = DateTime.Now;
                                fileModelZip.UploadTimeSpan = TheZipUploadTime + dtEnd.Subtract(dtStartZip);
                                if (StaticUtilities.ProcessTimeFormat == PTime.Ticks)
                                    fileModelZip.UploadTime = fileModelZip.UploadTimeSpan.Seconds.ToString("00") + ":" + fileModelZip.UploadTimeSpan.Ticks.ToString();
                                else if (StaticUtilities.ProcessTimeFormat == PTime.Milliseconds)
                                    fileModelZip.UploadTime = fileModelZip.UploadTimeSpan.Seconds.ToString("00") + ":" + fileModelZip.UploadTimeSpan.Milliseconds.ToString("000");
                                fileModels.Add(fileModelZip);
                            }
                        }
                    }
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
                string TheDirectoryPath = Path.Combine(DestinationPath, CurrDir);
                CreateDirectory(TheDirectoryPath);
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];

                    string TheFilePath = Path.Combine(TheDirectoryPath, Path.GetFileName(file.FileName));
                    if (Path.GetExtension(file.FileName).Contains(".rar"))
                    {
                        DateTime dtStart = DateTime.Now;
                        FileModel TheFileModelRar = new FileModel()
                        {
                            OriginalFileName = Path.GetFileName(file.FileName),
                            UploadedFileName = Path.GetFileName(file.FileName),
                            DirectoryPath = TheDirectoryPath,
                            FilePath = "",
                            Status = PStatus.Error,
                            StatusText = "Invalid Format"
                        };
                        DateTime dtEnd = DateTime.Now;
                        TheFileModelRar.UploadTimeSpan = dtEnd.Subtract(dtStart);
                        if (StaticUtilities.ProcessTimeFormat == PTime.Ticks)
                            TheFileModelRar.UploadTime = TheFileModelRar.UploadTimeSpan.Seconds.ToString("00") + ":" + TheFileModelRar.UploadTimeSpan.Ticks.ToString();
                        else if (StaticUtilities.ProcessTimeFormat == PTime.Milliseconds)
                            TheFileModelRar.UploadTime = TheFileModelRar.UploadTimeSpan.Seconds.ToString("00") + ":" + TheFileModelRar.UploadTimeSpan.Milliseconds.ToString("000");
                        fileModels.Add(TheFileModelRar);
                        continue;
                    }
                    else if (Path.GetExtension(file.FileName).Contains(".xml"))
                    {
                        DateTime dtStart = DateTime.Now;
                        file.SaveAs(TheFilePath);
                        FileModel TheFileModelXml = new FileModel()
                        {
                            OriginalFileName = Path.GetFileName(file.FileName),
                            UploadedFileName = Path.GetFileName(file.FileName),
                            DirectoryPath = TheDirectoryPath,
                            FilePath = TheFilePath,
                            Status = PStatus.None,
                            StatusText = "Uploaded"
                        };
                        DateTime dtEnd = DateTime.Now;
                        TheFileModelXml.UploadTimeSpan = dtEnd.Subtract(dtStart);
                        if (StaticUtilities.ProcessTimeFormat == PTime.Ticks)
                            TheFileModelXml.UploadTime = TheFileModelXml.UploadTimeSpan.Seconds.ToString("00") + ":" + TheFileModelXml.UploadTimeSpan.Ticks.ToString();
                        else if (StaticUtilities.ProcessTimeFormat == PTime.Milliseconds)
                            TheFileModelXml.UploadTime = TheFileModelXml.UploadTimeSpan.Seconds.ToString("00") + ":" + TheFileModelXml.UploadTimeSpan.Milliseconds.ToString("000");
                        fileModels.Add(TheFileModelXml);
                    }
                    else if (Path.GetExtension(file.FileName).Contains(".zip"))
                    {
                        DateTime dtStart = DateTime.Now;
                        file.SaveAs(TheFilePath);
                        using (var zipFile = ZipFile.OpenRead(TheFilePath))
                        {
                            VMZipFile TheVMZipFile = StaticUtilities.ZipFileInfo(zipFile);
                            if (TheVMZipFile == null)
                            {
                                FileModel TheFileModelZip1 = new FileModel()
                                {
                                    OriginalFileName = Path.GetFileName(file.FileName),
                                    UploadedFileName = Path.GetFileName(file.FileName),
                                    DirectoryPath = TheDirectoryPath,
                                    FilePath = TheFilePath,
                                    Status = PStatus.Error,
                                    StatusText = "Invalid Data"
                                };
                                DateTime dtEnd = DateTime.Now;
                                TheFileModelZip1.UploadTimeSpan = dtEnd.Subtract(dtStart);
                                if (StaticUtilities.ProcessTimeFormat == PTime.Ticks)
                                    TheFileModelZip1.UploadTime = TheFileModelZip1.UploadTimeSpan.Seconds.ToString("00") + ":" + TheFileModelZip1.UploadTimeSpan.Ticks.ToString();
                                else if (StaticUtilities.ProcessTimeFormat == PTime.Milliseconds)
                                    TheFileModelZip1.UploadTime = TheFileModelZip1.UploadTimeSpan.Seconds.ToString("00") + ":" + TheFileModelZip1.UploadTimeSpan.Milliseconds.ToString("000");
                                fileModels.Add(TheFileModelZip1);
                                continue;
                            }
                            zipFile.ExtractToDirectory(TheDirectoryPath);

                            string TheExtractedPath = TheDirectoryPath;
                            if (TheVMZipFile.ExtractedFolderName != null)
                                TheExtractedPath = Path.Combine(TheDirectoryPath, TheVMZipFile.ExtractedFolderName);

                            DirectoryInfo TheDirectoryInfo = new DirectoryInfo(TheExtractedPath);
                            FileInfo[] TheFileInfo = TheDirectoryInfo.GetFiles("*.xml");
                            if (TheFileInfo == null)
                            {
                                FileModel TheFileModelFileInfo1 = new FileModel()
                                {
                                    OriginalFileName = Path.GetFileName(file.FileName),
                                    UploadedFileName = Path.GetFileName(file.FileName),
                                    DirectoryPath = TheDirectoryPath,
                                    FilePath = TheFilePath,
                                    Status = PStatus.Error,
                                    StatusText = "Invalid Data"
                                };
                                DateTime dtEnd = DateTime.Now;
                                TheFileModelFileInfo1.UploadTimeSpan = dtEnd.Subtract(dtStart);
                                if (StaticUtilities.ProcessTimeFormat == PTime.Ticks)
                                    TheFileModelFileInfo1.UploadTime = TheFileModelFileInfo1.UploadTimeSpan.Seconds.ToString("00") + ":" + TheFileModelFileInfo1.UploadTimeSpan.Ticks.ToString();
                                else if (StaticUtilities.ProcessTimeFormat == PTime.Milliseconds)
                                    TheFileModelFileInfo1.UploadTime = TheFileModelFileInfo1.UploadTimeSpan.Seconds.ToString("00") + ":" + TheFileModelFileInfo1.UploadTimeSpan.Milliseconds.ToString("000");
                                fileModels.Add(TheFileModelFileInfo1);
                                continue;
                            }
                            if (TheFileInfo.Length == 0)
                            {
                                FileModel TheFileModelFileInfo2 = new FileModel()
                                {
                                    OriginalFileName = Path.GetFileName(file.FileName),
                                    UploadedFileName = Path.GetFileName(file.FileName),
                                    DirectoryPath = TheDirectoryPath,
                                    FilePath = TheFilePath,
                                    Status = PStatus.Error,
                                    StatusText = "Invalid Data"
                                };
                                DateTime dtEnd = DateTime.Now;
                                TheFileModelFileInfo2.UploadTimeSpan = dtEnd.Subtract(dtStart);
                                if (StaticUtilities.ProcessTimeFormat == PTime.Ticks)
                                    TheFileModelFileInfo2.UploadTime = TheFileModelFileInfo2.UploadTimeSpan.Seconds.ToString("00") + ":" + TheFileModelFileInfo2.UploadTimeSpan.Ticks.ToString();
                                else if (StaticUtilities.ProcessTimeFormat == PTime.Milliseconds)
                                    TheFileModelFileInfo2.UploadTime = TheFileModelFileInfo2.UploadTimeSpan.Seconds.ToString("00") + ":" + TheFileModelFileInfo2.UploadTimeSpan.Milliseconds.ToString("000");
                                fileModels.Add(TheFileModelFileInfo2);
                                continue;
                            }

                            DateTime dtEndZip = DateTime.Now;
                            TimeSpan TheZipUploadTime = new TimeSpan(dtEndZip.Subtract(dtStart).Ticks / TheFileInfo.Length);
                            foreach (FileInfo TheFile in TheFileInfo)
                            {
                                DateTime dtStartZip = DateTime.Now;
                                FileModel fileModelZip = new FileModel()
                                {
                                    OriginalFileName = TheFile.Name,
                                    UploadedFileName = TheFile.Name,
                                    DirectoryPath = TheDirectoryPath,
                                    FilePath = TheFile.FullName,
                                    Status = PStatus.None
                                };
                                DateTime dtEnd = DateTime.Now;
                                fileModelZip.UploadTimeSpan = TheZipUploadTime + dtEnd.Subtract(dtStartZip);
                                if (StaticUtilities.ProcessTimeFormat == PTime.Ticks)
                                    fileModelZip.UploadTime = fileModelZip.UploadTimeSpan.Seconds.ToString("00") + ":" + fileModelZip.UploadTimeSpan.Ticks.ToString();
                                else if (StaticUtilities.ProcessTimeFormat == PTime.Milliseconds)
                                    fileModelZip.UploadTime = fileModelZip.UploadTimeSpan.Seconds.ToString("00") + ":" + fileModelZip.UploadTimeSpan.Milliseconds.ToString("000");
                                fileModels.Add(fileModelZip);
                            }
                        }
                    }
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
            if (string.IsNullOrWhiteSpace(DirectoryPath)) return;
            if (!Directory.Exists(DirectoryPath)) Directory.CreateDirectory(DirectoryPath);
        }

        public void DeleteDirectory(string DirectoryPath)
        {
            if (string.IsNullOrWhiteSpace(DirectoryPath)) return;
            if (Directory.Exists(DirectoryPath)) Directory.Delete(DirectoryPath, true);
        }

        public string ReplaceXmlLimitations(string data)
        {
            data = data.Replace("&", "and");
            return data;
        }

        public bool IsCreateTextFile(string content, string DirectoryPath, string fileName)
        {
            if (string.IsNullOrWhiteSpace(DirectoryPath)) return false;
            if (string.IsNullOrWhiteSpace(fileName)) return false;
            try
            {
                if (!Directory.Exists(DirectoryPath)) new GeneralService().CreateDirectory(DirectoryPath);
                using (FileStream fs = File.Create(Path.Combine(DirectoryPath, fileName)))
                {
                    Byte[] aura = new UTF8Encoding(true).GetBytes(content);
                    fs.Write(aura, 0, aura.Length);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

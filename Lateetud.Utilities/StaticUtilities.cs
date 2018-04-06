using Lateetud.Utilities.Models;
using System;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Net.Mail;
using System.Web;
using System.Web.UI;

namespace Lateetud.Utilities
{
    public static class StaticUtilities
    {
        public static PTime ProcessTimeFormat { get; set; }


        public static void SendErrorNotification(string Subject, string Body, Exception Ex,HttpRequest TheRequest)
        {
            try
            {
                Subject = ConfigurationManager.AppSettings["ErrorSubject"] + Subject;
                Page ThePage = HttpContext.Current.Handler as Page;
                ThePage.Server.ClearError();
                if (TheRequest == null)
                    TheRequest = ThePage.Request;
                if (Ex != null)
                {
                    Ex=Ex.GetBaseException();
                    Body += "\n\nMessage: " + Ex.Message + "\n\n"
                    + "Source: " + Ex.Source + "\n\n"
                    + Ex.StackTrace;
                    Subject+= ": " + Ex.Message.Replace("\n","").Replace("\r","");
                }
                Body += "\n\n" + TheRequest.Url.ToString() + "\n\n" + TheRequest.UserHostAddress + "\n\n";

                string Address = ConfigurationManager.AppSettings["ErrorAddress"].ToString();
                MailMessage Mail = new MailMessage(Address,Address);
                Mail.Subject = Subject;
                Mail.IsBodyHtml = false;
                Mail.Body = Body;
                SmtpClient MailServer = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"]);
                MailServer.Send(Mail);
            }
            catch
            {
            }
        }
        public static string FormatMoney(decimal Value)
        {
            return String.Format("{0:0.00}", Value);
        }
        public static bool IsAppSettingTrue(string Key)
        {
            return ConfigurationManager.AppSettings[Key] != null && Convert.ToBoolean(ConfigurationManager.AppSettings[Key]);
        }
        public static bool IsApSettingEqual(string Key, string Value)
        {
            return ConfigurationManager.AppSettings[Key] != null && ConfigurationManager.AppSettings[Key].ToString()==Value;
        }
        public static string Truncate(string TheString, int Length)
        {
            return TheString.Substring(0, Math.Min(TheString.Length, Length));
        }
        public static Control FindControlRecursive(Control Root, string Id)
        {
            if (Root.ID == Id)
                return Root;
            foreach (Control Ctl in Root.Controls)
            {
                Control FoundCtl = FindControlRecursive(Ctl, Id);
                if (FoundCtl != null)
                    return FoundCtl;
            }
            return null;
        }
        public static string Left(string value, int start,int length)
        {
            return value.Substring(start, length);
        }
        public static string Right(string value, int length)
        {
            return value.Substring(value.Length - length);
        }
        public static bool IsSubstringValueExist(string value, string SubstringValue)
        {
            return (value.IndexOf(SubstringValue) < 0 ? false : true);
        }
        public static VMZipFile ZipFileInfo(ZipArchive zipArchive)
        {
            if (zipArchive == null) return null;
            if (zipArchive.Entries.Count == 0) return null;
            VMZipFile vMZipFile = new VMZipFile();
            vMZipFile.ExtractedFolderName = null;
            for (int i = 0; i < zipArchive.Entries.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(Path.GetExtension(zipArchive.Entries[i].FullName)))
                {
                    vMZipFile.ExtractedFolderName = StaticUtilities.Left(zipArchive.Entries[i].FullName, 0, zipArchive.Entries[i].FullName.Length - 1);
                    break;
                }
            }
            return vMZipFile;
        }
    }
}

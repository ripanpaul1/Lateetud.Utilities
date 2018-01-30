using System.IO;
using System.Xml;

namespace Lateetud.Utilities.XmlManager
{
    public class XmlService
    {
        public bool CreateXml(string XmlContent, string XmlPath, string FileName)
        {
            return this.CreateXml(XmlContent, Path.Combine(XmlPath, FileName));
        }
        public bool CreateXml(string XmlContent, string XmlPathWithFileName)
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(XmlContent);
                xDoc.Save(XmlPathWithFileName);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

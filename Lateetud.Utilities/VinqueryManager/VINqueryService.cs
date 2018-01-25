using System;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Net;

namespace Lateetud.Utilities.VinqueryManager
{
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    [XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class VINquery
    {
        public VINqueryVIN VIN { get; set; }
        [XmlAttributeAttribute()]
        public string Version { get; set; }
        [XmlAttributeAttribute()]
        public string Report_Type { get; set; }
        [XmlAttributeAttribute()]
        public string Date { get; set; }
    }

    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class VINqueryVIN
    {
        public VINqueryVINVehicle Vehicle { get; set; }
        [XmlAttributeAttribute()]
        public string Number { get; set; }
        [XmlAttributeAttribute()]
        public string Status { get; set; }
    }

    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class VINqueryVINVehicle
    {
        [XmlElementAttribute("Item")]
        public VINqueryVINVehicleItem[] Item { get; set; }
        [XmlAttributeAttribute()]
        public ushort VINquery_Vehicle_ID { get; set; }
        [XmlAttributeAttribute()]
        public ushort Model_Year { get; set; }
        [XmlAttributeAttribute()]
        public string Make { get; set; }
        [XmlAttributeAttribute()]
        public string Model { get; set; }
        [XmlAttributeAttribute()]
        public string Trim_Level { get; set; }
    }

    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class VINqueryVINVehicleItem
    {
        [XmlAttributeAttribute()]
        public string Key { get; set; }
        [XmlAttributeAttribute()]
        public string Value { get; set; }
        [XmlAttributeAttribute()]
        public string Unit { get; set; }
    }

    public class VINqueryService
    {
        private readonly string url;
        public VINqueryService(string apiUrl, string accessCode, string vin)
        {
            this.url = apiUrl +
                "?accessCode=" + accessCode +
                "&vin=" + vin +
                "&reportType=2";
        }
        public VINqueryService(string apiUrl, string accessCode, string vin, string reportType)
        {
            this.url = apiUrl +
                "?accessCode=" + accessCode +
                "&vin=" + vin +
                "&reportType=" + reportType;
        }

        public string Execute()
        {
            using (var client = new WebClient())
            {
                return client.DownloadString(this.url);
            } 
        }
    }
}

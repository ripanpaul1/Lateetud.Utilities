using System.Collections.Generic;
using System.Net;
using System.Xml.Serialization;

namespace Lateetud.Utilities.VinqueryManager
{
    [XmlRoot(ElementName = "Item")]
    public class Item
    {
        [XmlAttribute(AttributeName = "Key")]
        public string Key { get; set; }
        [XmlAttribute(AttributeName = "Value")]
        public string Value { get; set; }
        [XmlAttribute(AttributeName = "Unit")]
        public string Unit { get; set; }
    }

    [XmlRoot(ElementName = "Vehicle")]
    public class Vehicle
    {
        [XmlElement(ElementName = "Item")]
        public List<Item> Item { get; set; }
        [XmlAttribute(AttributeName = "VINquery_Vehicle_ID")]
        public string VINquery_Vehicle_ID { get; set; }
        [XmlAttribute(AttributeName = "Model_Year")]
        public string Model_Year { get; set; }
        [XmlAttribute(AttributeName = "Make")]
        public string Make { get; set; }
        [XmlAttribute(AttributeName = "Model")]
        public string Model { get; set; }
        [XmlAttribute(AttributeName = "Trim_Level")]
        public string Trim_Level { get; set; }
    }

    [XmlRoot(ElementName = "VIN")]
    public class VIN
    {
        [XmlElement(ElementName = "Vehicle")]
        public Vehicle Vehicle { get; set; }
        [XmlAttribute(AttributeName = "Number")]
        public string Number { get; set; }
        [XmlAttribute(AttributeName = "Status")]
        public string Status { get; set; }
    }

    [XmlRoot(ElementName = "VINquery")]
    public class VINquery
    {
        [XmlElement(ElementName = "VIN")]
        public VIN VIN { get; set; }
        [XmlAttribute(AttributeName = "Version")]
        public string Version { get; set; }
        [XmlAttribute(AttributeName = "Report_Type")]
        public string Report_Type { get; set; }
        [XmlAttribute(AttributeName = "Date")]
        public string Date { get; set; }
    }

    public class Vinquery
    {
        // NOTE: all query string parameter values must be URL-encoded!
        private readonly string url;

        public Vinquery(string apiUrl, string accessCode, string vin)
        {
            this.url = apiUrl +
                "?accessCode=" + accessCode +
                "&vin=" + vin +
                "&reportType=2";
        }

        public Vinquery(string apiUrl, string accessCode, string vin, string reportType)
        {
            this.url = apiUrl +
                "?accessCode=" + accessCode +
                "&vin=" + vin +
                "&reportType=" + reportType;
        }

        public string Execute()
        {
            using (var client = new WebClient())
                return client.DownloadString(this.url);
        }
    }
}

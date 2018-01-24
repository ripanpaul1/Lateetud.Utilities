using System.Net;

namespace Lateetud.Utilities.Address
{
    public class AddressByZipResult
    {
        public string country { get; set; }
        public string state { get; set; }
        public string city { get; set; }
    }

    public class AddressByZip
    {
        // NOTE: all query string parameter values must be URL-encoded!
        private readonly string url;

        public AddressByZip(string apiUrl, string ZipCode)
        {
            this.url = apiUrl + ZipCode;
        }

        public string Execute()
        {
            using (var client = new WebClient())
                return client.DownloadString(this.url);
        }
    }

 
}

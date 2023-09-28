using Newtonsoft.Json;
using RestSharp;
using Sdk.ImgBB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sdk.ImageShack
{
    public class ImageShackClient
    {
        private const string apikey = "235BCFOQ6bdafe3458612269b6172428edae39d9";
        private readonly RestClient client;

        public ImageShackClient()
        {
            this.client = new RestClient("https://post.imageshack.us");
        }

        public ImageShackApiResult UploadImageByFilename(string fileName, string filePath)
        {
            var request = new RestRequest("upload_api.php", Method.Post);

            request.AddParameter("key", apikey);
            request.AddParameter("format", "json");

            request.AddFile("fileupload", filePath);

            var response = this.client.Execute(request);
            var obj = JsonConvert.DeserializeObject<ImageShackApiResult>(response.Content);

            return obj;
        }
    }
}

using DotNetEnv;
using Newtonsoft.Json;
using RestSharp;

namespace Sdk.ImageShack
{
    public class ImageShackClient
    {
        private readonly string apikey;
        private readonly RestClient client;

        public ImageShackClient()
        {
            this.apikey = Env.GetString("imageShackApiKey");
            this.client = new RestClient("https://post.imageshack.us");
        }

        public ImageShackApiResult UploadImageByFile(string fileName, string filePath)
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

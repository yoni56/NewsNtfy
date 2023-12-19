using DotNetEnv;
using Newtonsoft.Json;
using RestSharp;

namespace Sdk.ImgBB
{
    public class ImgBBClient
    {
        private readonly string apikey;
        private readonly RestClient client;

        public ImgBBClient()
        {
            this.apikey = Env.GetString("imgBBApiKey");
            this.client = new RestClient("https://api.imgbb.com");
        }

        public ImgBBApiResult UploadImageBase64(string base64EncodedImage, TimeSpan expiration, string fileName = "")
        {
            var request = new RestRequest("/1/upload", Method.Post);

            // max expiration date by api limit is 180 days
            if (expiration.TotalSeconds >= 60 || expiration.TotalSeconds <= 15552000)
            {
                request.AddQueryParameter("expiration", expiration.TotalSeconds);
            }

            if (!string.IsNullOrWhiteSpace(fileName))
            {
                request.AddQueryParameter("filename", fileName);
            }

            request.AddQueryParameter("key", apikey);

            // add the image encoded to the request body
            request.AddParameter("image", base64EncodedImage);

            var response = this.client.Execute(request);
            var obj = JsonConvert.DeserializeObject<ImgBBApiResult>(response.Content);

            return obj;
        }
    }
}

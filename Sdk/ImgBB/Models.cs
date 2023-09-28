namespace Sdk.ImgBB
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Data
    {
        public string? id { get; set; }
        public string? title { get; set; }
        public string? url_viewer { get; set; }
        public string? url { get; set; }
        public string? display_url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int size { get; set; }
        public int time { get; set; }
        public int expiration { get; set; }
        public Image? image { get; set; }
        public Thumb? thumb { get; set; }
        public Medium? medium { get; set; }
        public string? delete_url { get; set; }
    }

    public class Image
    {
        public string? filename { get; set; }
        public string? name { get; set; }
        public string? mime { get; set; }
        public string? extension { get; set; }
        public string? url { get; set; }
    }

    public class Medium
    {
        public string? filename { get; set; }
        public string? name { get; set; }
        public string? mime { get; set; }
        public string? extension { get; set; }
        public string? url { get; set; }
    }

    public class ImgBBApiResult
    {
        public Data? data { get; set; }
        public bool success { get; set; }
        public int status { get; set; }
    }

    public class Thumb
    {
        public string? filename { get; set; }
        public string? name { get; set; }
        public string? mime { get; set; }
        public string? extension { get; set; }
        public string? url { get; set; }
    }
}

using Newtonsoft.Json;

namespace Sdk.ImageShack
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class ExifInfo
    {
        public string exiforientation { get; set; }
        public string exifimagesnum { get; set; }
        public string exiffilesize { get; set; }
    }

    public class Files
    {
        public int server { get; set; }
        public int bucket { get; set; }
        public Image image { get; set; }
        public Thumb thumb { get; set; }
    }

    public class Image
    {
        public int size { get; set; }

        [JsonProperty("content-type")]
        public string contenttype { get; set; }
        public string filename { get; set; }
        public string original_filename { get; set; }
    }

    public class Links
    {
        public string image_link { get; set; }
        public string image_html { get; set; }
        public string image_bb { get; set; }
        public string image_bb2 { get; set; }
        public string thumb_link { get; set; }
        public string thumb_html { get; set; }
        public string thumb_bb { get; set; }
        public string thumb_bb2 { get; set; }
        public string is_link { get; set; }
        public string done { get; set; }
    }

    public class Rating
    {
        public int ratings { get; set; }
        public int avg { get; set; }
    }

    public class Resolution
    {
        public int width { get; set; }
        public int height { get; set; }
    }

    public class ImageShackApiResult
    {
        public string status { get; set; }
        public int version { get; set; }
        public int timestamp { get; set; }
        public string base_url { get; set; }
        public string lp_hash { get; set; }
        public string id { get; set; }
        public Rating rating { get; set; }
        public Files files { get; set; }
        public Resolution resolution { get; set; }

        [JsonProperty("exif-info")]
        public ExifInfo exifinfo { get; set; }
        public string @class { get; set; }
        public string visibility { get; set; }
        public Uploader uploader { get; set; }
        public Links links { get; set; }
    }

    public class Thumb
    {
        public int size { get; set; }
        public string content { get; set; }
        public string filename { get; set; }
    }

    public class Uploader
    {
        public string ip { get; set; }
        public string cookie { get; set; }
    }


}

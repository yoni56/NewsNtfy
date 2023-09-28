using RestSharp;

namespace NewsNotify.Services
{
    public class NtfySh
    {
        private readonly RestClient client;

        public NtfySh()
        {
            this.client = new RestClient("https://ntfy.sh");
        }

        public void sendMessage(string title, string text, string attachUrl, string httpUrl)
        {
            var request = new RestRequest("/newsNotify");

            request.AddHeader("Title", title);
            request.AddHeader("Click", httpUrl);

            if (attachUrl.Contains("https"))
            {
                request.Method = Method.Post;
                request.AddHeader("Attach", attachUrl);
            }
            else
            {
                request.Method = Method.Put;
                request.AddFile("Filename", attachUrl);
            }
           

            request.AddBody(text);

            this.client.Execute(request);
        }
    }
}

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
            var request = new RestRequest("/newsNotify", Method.Post);

            request.AddHeader("Title", title);
            request.AddHeader("Click", httpUrl);
            request.AddHeader("Attach", attachUrl);

            request.AddBody(text);

            this.client.Execute(request);
        }
    }
}

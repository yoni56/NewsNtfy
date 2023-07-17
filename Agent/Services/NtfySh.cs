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

        public void sendMessage(string title, string text, string url)
        {
            var request = new RestRequest("/newsNotify", Method.Post);
            request.AddHeader("title", title);
            request.AddHeader("click", url);
            request.AddBody(text);

            this.client.Execute(request);
        }
    }
}

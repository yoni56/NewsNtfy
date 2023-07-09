using ScrapySharp.Network;

namespace NewsNotify.Jobs
{
    public abstract class BaseJob
    {
        private readonly ScrapingBrowser _browser;

        public BaseJob()
        {
            this._browser = new ScrapingBrowser();
        }

        protected ScrapingBrowser GetBrowser()
        {
            return this._browser;
        }
    }
}

using Sdk.Articles;
using Sdk.Base;

namespace zhk
{
    public class DllMain : IDllMain
    {
        public override void Execute()
        {
            try
            {
                var pageDom = this.GetPageDom("https://www.zhk.co.il/category/news/karmiel-news/");
                var article = new ZhkGrabber(pageDom).GrabArticleFirstOrDefault();

                this.RaiseUpdateEvent(article);
            }
            catch (Exception ex)
            {
                this.RaiseUpdateEvent(new ExceptionArticle()
                {
                    OptionalInfo = ex.ToString()
                });
            }
        }
    }
}

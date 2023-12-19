using Sdk.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sdk.Base
{
    public interface IGrabber
    {
        public IArticle GrabArticleFirstOrDefault();
    }
}

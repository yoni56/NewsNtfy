﻿using Hanssens.Net;
using RestSharp;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace Sdk.Articles
{
    public abstract class IArticle
    {
        public string Key { get; set; }
        public string SiteName { get; set; }

        public string Headline { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Link { get; set; }
        public string ImgSrc { get; set; }

        public string OptionalInfo { get; set; }

        public override int GetHashCode()
        {
            return this.GetIntHashMd5ByStringValue(this.Link);
        }

        public abstract IArticle GetCached(ILocalStorage storage);

        private int GetIntHashMd5ByStringValue(string value)
        {
            MD5 md5Hasher = MD5.Create();
            var hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(value));
            var ivalue = BitConverter.ToInt32(hashed, 0);
            return ivalue;
        }
    }
}

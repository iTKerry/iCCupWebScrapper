using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iCCup.BL.Utils;
using iCCup.DATA.Models;
using ScrapySharp.Extensions;
using ScrapySharp.Network;

namespace iCCup.BL.Scrapper
{
    public class ScrapperBase
    {
        private readonly ScrapingBrowser _browser;

        public ScrapperBase()
        {
            _browser = new ScrapingBrowser
            {
                AllowAutoRedirect = true,
                AllowMetaRedirect = true
            };
        }

        public async Task<Tuple<List<UserSearch>, string, string>> SearchPlayer(string nickname)
        {
            var url = string.Format(Globals.iCCupUrl + Globals.Search, nickname);
            return await Search(new Uri(url));
        }

        public async Task<Tuple<List<UserSearch>, string, string>> SearchPlayer(Uri url)
        {
            return await Search(url);
        }

        private async Task<Tuple<List<UserSearch>, string, string>> Search(Uri url)
        {
            List<UserSearch> result = new List<UserSearch>();
            var nextPageUrl = "";
            var prevPageUrl = "";

            var page = await _browser.NavigateToPageAsync(url);
            var table = page.Html.CssSelect(".search-user h2 a.profile-view-link");
            var links = table.Select(i => i.GetAttributeValue("href"));

            result.AddRange(links.Select(l => new UserSearch
            {
                Url = l,
                Nickname = l
                    .Replace("gamingprofile/", "")
                    .Replace(".html", "")
            }));

            if (page.Html.CssSelect(".next a").Any())
                nextPageUrl = Globals.iCCupUrl + page.Html.CssSelect(".next a").First().GetAttributeValue("href");

            if (page.Html.CssSelect(".previous a").Any())
                prevPageUrl = Globals.iCCupUrl + page.Html.CssSelect(".previous a").First().GetAttributeValue("href");

            return Tuple.Create(result, nextPageUrl, prevPageUrl);
        }
    }
}

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
            _browser = new ScrapingBrowser();
            _browser.AllowAutoRedirect = true;
            _browser.AllowMetaRedirect = true;
        }

        public async Task<List<UserSearch>> SearchPlayer(string nickname)
        {
            List<UserSearch> result = new List<UserSearch>();
            var nextPage = true;
            var url = string.Format(Globals.iCCupUrl + Globals.Search, nickname);

            while (nextPage)
            {
                var page = await _browser.NavigateToPageAsync(new Uri(url));

                var table = page.Html.CssSelect(".search-user h2 a.profile-view-link");
                var links = table.Select(i => i.GetAttributeValue("href"));
                result.AddRange(links.Select(l => new UserSearch
                {
                    Url = l,
                    Nickname = l
                        .Replace("gamingprofile/", "")
                        .Replace(".html", "")
                }));

                nextPage = page.Html.CssSelect(".next a").Any();
                if (nextPage)
                {
                    url = Globals.iCCupUrl + page.Html.CssSelect(".next a").First().GetAttributeValue("href");
                }
            }

            return result;
        }
    }
}

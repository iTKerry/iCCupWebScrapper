using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Threading;
using iCCup.DATA.Models;
using iCCup.UI.Infrastructure.Contracts;
using iCCup.UI.Infrastructure.Utils;
using ScrapySharp.Extensions;
using ScrapySharp.Network;

namespace iCCup.UI.Infrastructure.Scrapper
{
    public class ScrapperBase
    {
        private readonly ILoggerService _logger;
        private readonly ScrapingBrowser _browser;

        public ScrapperBase(ILoggerService logger)
        {
            _logger = logger;
            _browser = new ScrapingBrowser
            {
                AllowAutoRedirect = true,
                AllowMetaRedirect = true
            };
        }

        #region SearchPlayer

        public async Task<(List<UserSearch>, string, string)> SearchPlayer(string nickname)
        {
            var url = string.Format(Globals.iCCupUrl + Globals.Search, nickname);

            DispatcherHelper.CheckBeginInvokeOnUI(() => _logger.AddInfoWithUrl("Navigate to webpage - ", url));

            return await Search(new Uri(url));
        }

        public async Task<(List<UserSearch>, string, string)> SearchPlayer(Uri url)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() => _logger.AddInfoWithUrl("Redirect to webpage - ", url.ToString()));

            return await Search(url);
        }

        private async Task<(List<UserSearch>, string, string)> Search(Uri url)
        {
            var page = await _browser.NavigateToPageAsync(url);
            var playerTabs = page.Html.CssSelect(".search-user");
            var result = (from playerTab in playerTabs
                let parsedUrl = playerTab.CssSelect("h2 a.profile-view-link").First().GetAttributeValue("href")
                let parsedNickname = parsedUrl.Replace("gamingprofile/", "").Replace(".html", "")
                let ranks =
                playerTab.CssSelect("div")
                    .Where(div => Globals.Ranks.ContainsKey(div.GetAttributeValue("class")))
                    .ToArray()
                let stats =
                playerTab.CssSelect(".search-info .left")
                    .Select(node => node.InnerText)
                    .Where(t => t.Contains('-'))
                    .Select(s => s.Split('-').Select(str => str.ToInteger()).ToArray())
                    .ToArray()
                select new UserSearch
                {
                    Url = parsedUrl,
                    Nickname = parsedNickname,
                    Pts5V5 = int.Parse(ranks[0].GetAttributeValue("title")),
                    Rank5V5 = Globals.Ranks[ranks[0].GetAttributeValue("class")],
                    Win5V5 = stats[0][0],
                    Lose5V5 = stats[0][1],
                    Pts3V3 = int.Parse(ranks[1].GetAttributeValue("title")),
                    Rank3V3 = Globals.Ranks[ranks[1].GetAttributeValue("class")],
                    Win3V3 = stats[1][0],
                    Lose3V3 = stats[1][1]
                }).ToList();

            return (result, GetPagination(page, ".next a"), GetPagination(page, ".previous a"));
        }

        private static string GetPagination(WebPage page, string xpath)
        {
            return page.Html.CssSelect(xpath).Any() ? Globals.iCCupUrl + page.Html.CssSelect(xpath).First().GetAttributeValue("href") : string.Empty;
        }

        #endregion

        #region GameProfile

        public async Task<UserGameProfile> GetPlayerProfile(Uri url, UserSearch userSearch)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() => _logger.AddInfoWithUrl("Navigation to game profile - ", url.ToString()));

            var page = await _browser.NavigateToPageAsync(url);

            var mainBoard = page.Html.CssSelect(".stata-body tr td").ToArray();

            var userProfile = new UserGameProfile(userSearch)
            {
                RaitingPosition5V5 = mainBoard[1].InnerText.Replace("#", "").ToInteger(),
                Couriers = mainBoard[7].InnerText.ToInteger(),
                Neutrals = mainBoard[9].InnerText.ToInteger(),
                Hours = mainBoard[11].InnerText.ToInteger(),
                Winrate5V5 = mainBoard[13].InnerText.Replace("%", "").ToInteger(),
                Leaves5V5 = mainBoard[15].InnerText.ToInteger(),
                MaxWinstreak5V5 = mainBoard[19].InnerText.ToInteger(),
                CurrentWinstreak5V5 = mainBoard[21].InnerText.ToInteger(),
                GamesListUrl = mainBoard.CssSelect("a").First(a => a.GetAttributeValue("href").Contains("matchlist/")).GetAttributeValue("href")
            };

            return userProfile;
        }

        #endregion
    }
}

﻿using System;
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
            PushToLogWithUrl(url);

            return await Search(new Uri(url));
        }

        public async Task<(List<UserSearch>, string, string)> SearchPlayer(Uri url)
        {
            PushToLogWithUrl(url.ToString());

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
                    .Select(s => s.Split('-').Select(str => str.AsInt()).ToArray())
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

            var images = page.Html.CssSelect(".search-photo")
                .CssSelect("img")
                .Where(i => i.GetAttributeValue("alt").Equals("avatar"))
                .Select(node => node.GetAttributeValue("src"))
                .ToArray();

            var indx = 0;
            result.ForEach(r => r.ImageSource = images[indx++]);

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
            PushToLogWithUrl(url.ToString());

            var page = await _browser.NavigateToPageAsync(url);

            var mainBoard = page.Html.CssSelect(".stata-body tr td").ToArray();

            var userProfile = new UserGameProfile(userSearch)
            {
                RaitingPosition5V5 = mainBoard[1].InnerText.Replace("#", "").AsInt(),
                Couriers = mainBoard[7].InnerText.AsInt(),
                Neutrals = mainBoard[9].InnerText.AsInt(),
                Hours = mainBoard[11].InnerText.AsInt(),
                Winrate5V5 = mainBoard[13].InnerText.Replace("%", "").AsInt(),
                Leaves5V5 = mainBoard[15].InnerText.AsInt(),
                MaxWinstreak5V5 = mainBoard[19].InnerText.AsInt(),
                CurrentWinstreak5V5 = mainBoard[21].InnerText.AsInt(),
                GamesListUrl = mainBoard.CssSelect("a").First(a => a.GetAttributeValue("href").Contains("matchlist/")).GetAttributeValue("href")
            };

            await GetPersonalGameDetails(userProfile);

            return userProfile;
        }

        private async Task GetPersonalGameDetails(UserGameProfile gameProfile)
        {
            PushToLogWithUrl(gameProfile.GamesListUrl);

            var games = new List<GameDetailsPersonal>();
            var page = await _browser.NavigateToPageAsync(new Uri($"{Globals.iCCupUrl}dota/{gameProfile.GamesListUrl}"));
            var urls = page.Html.CssSelect(".game-details").Select(a => a.GetAttributeValue("href")).ToArray();

            foreach (var matchUrl in urls)
            {
                PushToLogWithUrl(matchUrl);

                var gamePage = await _browser.NavigateToPageAsync(new Uri($"{Globals.iCCupUrl}dota/{matchUrl}"));
                var sections = gamePage.Html.CssSelect(".t-corp2").ToList();
                var date = sections[0].CssSelect(".field2").First().InnerText;
                var gameName = sections[1].CssSelect(".field2").First().InnerText;
                var time = sections[2].CssSelect(".field2").First().InnerText;

                var statsTable = sections
                    .Where(n => n.CssSelect(".field2")
                        .Any(sn => sn.ChildNodes
                            .Any(ssn => ssn.InnerText == gameProfile.Nickname)))
                    .Select(r => r.CssSelect(".field2").Select(rt => rt.InnerText).ToArray())
                    .First();

                games.Add(new GameDetailsPersonal
                {
                    DateTime = date,
                    GameName = gameName,
                    GameTime = time,
                    Kills = statsTable[1].AsInt(),
                    Deaths = statsTable[2].AsInt(),
                    Assists = statsTable[3].AsInt(),
                    CreepStats = statsTable[4].AsInt(),
                    GoldLeft = statsTable[5].AsInt(),
                    TowersDestroyed = statsTable[6].AsInt(),
                    GameSide = sections
                                   .FindIndex(n => n.CssSelect(".field2")
                                       .Any(sn => sn.ChildNodes
                                           .Any(ssn => ssn.InnerText == gameProfile.Nickname))) < 10
                        ? GameSide.Sentinel
                        : GameSide.Scourge
                });
            }
        }

        #endregion

        private void PushToLogWithUrl(string url)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() => _logger.AddInfoWithUrl("Navigate to webpage - ", url));
        }
    }
}

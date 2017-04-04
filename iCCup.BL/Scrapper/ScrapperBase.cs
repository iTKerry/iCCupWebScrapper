using ScrapySharp.Network;

namespace iCCup.BL.Scrapper
{
    public class ScrapperBase
    {
        private readonly ScrapingBrowser _browser;

        public ScrapperBase()
        {
            _browser = new ScrapingBrowser();
        }
    }
}

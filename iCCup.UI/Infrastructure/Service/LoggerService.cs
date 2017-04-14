using System;
using System.Diagnostics;
using System.Windows.Documents;
using System.Windows.Media;
using iCCup.DATA.Models;
using iCCup.UI.Infrastructure.Contracts;

namespace iCCup.UI.Infrastructure.Service
{
    public class LoggerService : ILoggerService
    {
        private readonly IMessangerService _messangerService;

        public LoggerService(IMessangerService messangerService)
        {
            _messangerService = messangerService;
        }

        public void AddInfo(string text)
        {
            var paragraph = new Paragraph();
            paragraph.Inlines.Add(new Bold(new Run(DateTime.Now.ToLongTimeString() + ":") {Foreground = Brushes.DarkGreen}));
            paragraph.Inlines.Add(new Run($" {text}"));

            _messangerService.AddToLog(new LogMessange { Content = paragraph });
        }

        public void AddInfoWithUrl(string text, string url)
        {
            var paragraph = new Paragraph();
            paragraph.Inlines.Add(new Bold(new Run(DateTime.Now.ToLongTimeString() + ":") { Foreground = Brushes.DarkGreen }));
            paragraph.Inlines.Add(new Run($" {text}"));

            var link = new Hyperlink(new Run(url));
            link.Click += (sender, args) => Process.Start(url);
            paragraph.Inlines.Add(link);

            _messangerService.AddToLog(new LogMessange { Content = paragraph });
        }

        public void AddError(Exception ex, string text)
        {
            var paragraph = new Paragraph();
            paragraph.Inlines.Add(new Bold(new Run(DateTime.Now.ToLongTimeString() + ":") { Foreground = Brushes.DarkRed }));
            paragraph.Inlines.Add(new Run($" {text}\n\n{ex.StackTrace}") {Foreground = Brushes.DarkRed});

            _messangerService.AddToLog(new LogMessange { Content = paragraph });
        }
    }
}

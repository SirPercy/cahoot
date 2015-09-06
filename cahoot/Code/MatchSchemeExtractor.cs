using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using HtmlAgilityPack;
using Calendar = cahoot.Models.Calendar;

namespace cahoot.Code
{
    public class MatchSchemeExtractor
    {

        public string Url { get; set; }
        public string Team { get; set; }

        public IEnumerable<Calendar> Extract()
        {
            var webb = new HtmlWeb();
            webb.OverrideEncoding = Encoding.GetEncoding("iso-8859-1");
            var doc = webb.Load(Url);

            var trNodes = doc.DocumentNode.SelectNodes("//table[@class='clTblStandings']/tbody/ul/tr");
            var extractedMatches = new List<ExtractedData>();
            DateTime currentDate = new DateTime();
            foreach (var trNode in trNodes)
            {
                var match = new ExtractedData();
                foreach (var node in trNode.ChildNodes)
                {
                    var span = node.SelectSingleNode("./span") ?? node.SelectSingleNode("./a");
                    if(span == null)
                        continue;
                    
                    if (span.Id.Contains("MatchDayFormatted"))
                    {
                        if (!string.IsNullOrEmpty(span.InnerText))
                        {
                            try
                            {
                                if (span.InnerText.Split('/').Any())
                                {
                                    var month = int.Parse(span.InnerText.Split('/')[1]);
                                    var year = month < DateTime.Now.Month ? DateTime.Now.Year + 1 : DateTime.Now.Year;
                                    currentDate = DateTime.ParseExact(span.InnerText + " " + year, "ddd d/M yyyy",
                                        new CultureInfo("sv-SE"));
                                }
                            }
                            catch { continue;}
                        }
                    }
                    if (trNode.InnerText.Contains("BK Cahoot"))
                    {

                        if (span.Id.Contains("MatchTimeFormatted"))
                        {
                            match.Time = span.InnerText;
                        }
                        else if (span.Id.Contains("MatchFakta"))
                        {
                            match.Teams = span.InnerText.Replace("BK Cahoot", "BK Cahoot " + Team);
                        }
                        else if (span.Id.Contains("HyperLinkHall"))
                        {
                            match.Alley = span.InnerText;
                        }
                    }
                }
                if (!string.IsNullOrEmpty(match.Teams))
                {
                    match.Date = currentDate;
                    extractedMatches.Add(match);
                }

            }
            return extractedMatches.Select(m => new Calendar{EventDate = m.Date, EventText = m.Teams + ", kl " + m.Time, EventInfo = m.Alley});
        }

    }

    public struct ExtractedData
    {
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string Teams { get; set; }
        public string Alley { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Xml.Serialization;
using cahoot.Models;
namespace cahoot.Code
{
    public class XmlUtil
    {
        public static List<MenuItem> GetStatItems(Constants.XmlDataType type){
            string file;
            if (type == Constants.XmlDataType.Standings)
            {
                file = HttpContext.Current.Server.MapPath("~/Files/Xml/Standings.xml");
                if(!File.Exists(file))
                    return new List<MenuItem>();
              
            }
            else
            {
                file = HttpContext.Current.Server.MapPath("~/Files/Xml/Statistics.xml");
                if (!File.Exists(file))
                    return new List<MenuItem>();
  
            }

            var statistics = Deserialize<Statistics>(file);
            return statistics.MenuItem;
         
       }
        public static List<Sponsor> GetSponsorItems()
        {
            string file = HttpContext.Current.Server.MapPath("~/Files/Xml/Sponsors.xml");
            if (!File.Exists(file))
                return new List<Sponsor>();

           
            var statistics = Deserialize<Sponsor>(file);
            return statistics.SponsorEntires;

        }
        public static void SaveStatItems(Action<Statistics> standingToSave, Constants.XmlDataType type)
        {
            string file = HttpContext.Current.Server.MapPath(type == Constants.XmlDataType.Standings ? "~/Files/Xml/Standings.xml" : "~/Files/Xml/Statistics.xml");
            var stat = new Statistics();
            standingToSave(stat);
            Seralialize(stat, file);
            HttpContext.Current.Cache.Remove("statitems");
        }
        public static void SaveSponsorItems(Action<Sponsor> sponsorToSave)
        {
            string file = HttpContext.Current.Server.MapPath("~/Files/Xml/Sponsors.xml");
            var sponsor = new Sponsor();
            sponsorToSave(sponsor);
            //sponsorToSave(sponsor);
            Seralialize(sponsor, file);
        }

        private static void Seralialize<T>(T data, string file)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (TextWriter textWriter = new StreamWriter(file))
            {
                serializer.Serialize(textWriter, data);
                textWriter.Close();
            }
        }
        private static T Deserialize<T>(string file)
        {
            T data;
            var deseralizer = new XmlSerializer(typeof(T));
            using (TextReader reader = new StreamReader(file))
            {
                data = (T)deseralizer.Deserialize(reader);
                reader.Close();
            }
            return data;

        }
    }
}
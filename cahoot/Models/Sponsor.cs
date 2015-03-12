using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;

namespace cahoot.Models
{
    [Serializable]
    [XmlRoot]
    public class Sponsor
    {
        [XmlElement]
        [Required(ErrorMessage = "Vänligen ange ett namn")]
        public string Name { get; set; }
        [XmlElement]
        public string Link { get; set; }
        [XmlElement]
        public string LogotypeUrl { get; set; }
        [XmlElement]
        public string TeaserText { get; set; }
        public List<Sponsor> SponsorEntires { get; set; }
    }
}
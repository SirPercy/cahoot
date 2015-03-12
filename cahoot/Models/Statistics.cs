using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;

namespace cahoot.Models
{
    [Serializable]
    [XmlRoot(ElementName="statistics", Namespace="")]
    public class Statistics
    {
        [XmlElement(ElementName="menuitem")]
        public List<MenuItem> MenuItem { get; set; }
    }
    public class MenuItem
    {
        [XmlElement(ElementName = "name")]
        [Required(ErrorMessage = "Vänligen ange ett lag")]
        public string Name { get; set; }
        [XmlElement(ElementName = "link")]
        [Required(ErrorMessage = "Vänligen ange en länk")]
        public string Link { get; set; }
    }
}
   

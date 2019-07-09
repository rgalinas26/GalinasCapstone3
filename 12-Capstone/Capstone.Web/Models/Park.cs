using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class Park
    {
        public string Park_Code { get; set; }
        public string Park_Name { get; set; }
        public string State { get; set; }
        public int Acerage { get; set; }
        public int Elevation_In_Feet { get; set; }
        public decimal Miles_Of_Trail { get; set; }
        public int Number_Of_Campsites { get; set; }
        public string Climate { get; set; }
        public int Year_Founded { get; set; }
        public int Annual_Visitor_Count { get; set; }
        public string Quote { get; set; }
        public string Quote_Source { get; set; }
        public string Description { get; set; }
        public int Entry_Fee { get; set; }
        public int Number_Of_Species { get; set; }
    }
}

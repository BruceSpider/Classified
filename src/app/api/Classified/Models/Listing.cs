using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Classified.Models
{
    public class Listing
    {
        #region Listing
        public int ClassifiedId { get; set; }

        public string ListingType { get; set; }
        public int ListingTypeId { get; set; }

        public string Category { get; set; }
        public int CategoryId { get; set; }

        public int Expiry { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Image { get; set; }
        #endregion

    }
}
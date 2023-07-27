using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Client.Models
{
    public class Address
    {
        public string street { get; set; }
        public string city { get; set; }
        public string provstate { get; set; }
        public string postalzipcode { get; set; }
    }

    public class Rating
    {
        public int minRating { get; set; } = 0;         //default to 0
        public int maxRating { get; set; } = 5;         //default to 5
        public int currentRating { get; set; }
    }
    public class Cost
    {
        public int minCost { get; set; } = 0;
        public int maxCost { get; set; } = 5;
        public int currentCost { get; set; }
    }
    public class RestaurantInfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public Address address { get; set; }
        public string summary { get; set; }
        public string foodType { get; set; }
        public Rating rating { get; set; }

        public Cost cost { get; set; }

    }
}
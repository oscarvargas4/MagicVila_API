﻿namespace MagicVila_VilaAPI.Models
{
    public class Vila
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string Rate { get; set; }
        public int Sqft { get; set; }
        public int Occupancy { get; set; }
        public string ImageUrl { get; set; }
        public string Amenity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}

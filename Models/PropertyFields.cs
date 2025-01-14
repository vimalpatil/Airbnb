﻿namespace Airbnb.WebAPI.Models
{
    public class PropertyDetails
    {
        public int p_id { get; set; }
        public string title {  get; set; }
        public string description { get; set; }
        public string image_name {  get; set; }
        public double price { get; set; }
        public string location {  get; set; }
        public string geometry_coordinate { get; set;}
        public DateTime date { get; set; }
        
        public string country { get; set;}
        public int ?user_id { get; set; }
        public string ?username { get; set; }
        public string ?user_type { get; set; }
    }

   
}

namespace Airbnb.WebAPI.Models
{
    public class ReviewFields
    {
        public int review_id { get; set; }
        public int rating { get; set; }
        public string comment { get; set; }
        public int user_id { get; set; }
        public int p_id { get; set; }
        public DateTime review_date {  get; set; }
    }
}

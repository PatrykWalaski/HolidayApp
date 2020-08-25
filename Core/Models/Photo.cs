namespace Core.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string PhotoUrl { get; set; }
        public bool isMain { get; set; }
        public string PublicID { get; set; }


        public Offer Offer  { get; set; }
        public int OfferId { get; set; }
    }
}
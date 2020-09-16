namespace Core.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string PublicID { get; set; }


        public Holiday Holiday  { get; set; }
        public int HolidayId { get; set; }
    }
}
namespace Core.Models
{
    public class HolidayParams
    {
        public string Agencies { get; set; } = null;
        public string Meals { get; set; } = null;
        public string Countries { get; set; } = null;
        public int? MinDuration { get; set; } = null;
        public int? MaxDuration { get; set; } = null;
        public decimal? MinPrice { get; set; } = null;
        public decimal? MaxPrice { get; set; } = null;
        public string Sort { get; set; } = null;
    }
}
namespace Core.Models
{
    public class HolidayParams
    {
        public string Agencies { get; set; } = null;
        public string Meals { get; set; } = null;
        public string Countries { get; set; } = null;
        public int? MinStars { get; set; }
        public int? MinDuration { get; set; } = null;
        public int? MaxDuration { get; set; } = null;
        public decimal? MinPrice { get; set; } = null;
        public decimal? MaxPrice { get; set; } = null;
        public string Sort { get; set; } = null;


        private const int MaxPageSize = 50;
        public int PageIndex {get; set;} = 1;
        private int _pageSize = 6;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}
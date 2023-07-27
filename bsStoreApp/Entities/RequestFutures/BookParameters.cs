namespace Entities.RequestFuture
{
    public class BookParameters : RequestParameters
	{
        public uint minPrice { get; set; }
        public uint maxPrice { get; set; } = 1000;
        public bool ValidPriceRange => maxPrice > minPrice;

        public String? SearchTerm { get; set; }

        public BookParameters()
        {
            OrderBy = "id";
        }

    }
}

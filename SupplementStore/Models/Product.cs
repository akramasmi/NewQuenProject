namespace SupplementStore.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Category { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public double Rating { get; set; }
        public int ReviewCount { get; set; }
        public string ImageEmoji { get; set; } = "💊";
        public bool InStock { get; set; } = true;

        public decimal DiscountPercent =>
            OldPrice.HasValue && OldPrice.Value > Price
                ? Math.Round((OldPrice.Value - Price) / OldPrice.Value * 100)
                : 0;
    }
}

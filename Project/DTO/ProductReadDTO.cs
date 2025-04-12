namespace Project.DTO
{
    public class ProductReadDTO
    {
        public int ProductId { get; set; }
        public required string Name { get; set; }

        public int CategoryId { get; set; }
        public required string CategoryName { get; set; }

        public int BrandId { get; set; }
        public required string BrandName { get; set; }

        public int AdminId { get; set; }
        public required string AdminName { get; set; }
    }
}

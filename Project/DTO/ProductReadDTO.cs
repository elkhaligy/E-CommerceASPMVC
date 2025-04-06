namespace Project.DTO
{
    public class ProductReadDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public int BrandId { get; set; }
        public string BrandName { get; set; }

        public int AdminId { get; set; }
        public string AdminName { get; set; }
    }
}

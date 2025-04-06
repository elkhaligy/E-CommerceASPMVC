namespace Project.DTO
{
    public class ProductCreateDTO
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int  BrandId { get; set; }
        public int AdminId { get; set; }
        //used in httpPost create (productDTO productdto) lightwight class 
    }
}

using Microsoft.AspNetCore.Mvc;



namespace Project.Contract
{
    public interface IProductRepository
    {
        Task addProductAsync(Product product);
        public void edit(Product product);
        public void delete(Product product);
         List<Product> getAll();
        public void save();
         // getById(int id);
      //  public List<Product> getByName(string name);
    }
}

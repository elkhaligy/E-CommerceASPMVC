namespace Project.Contract
{
    public interface IBrandRepository
    {
        Task<IEnumerable<Brand>> GetAllBrandsAsync();
        Task<Brand> GetByIdAsync(int id);
        Task UpdateBrandAsync(Brand brand);
        Task DeleteBrandAsync(int id);
        Task AddBrand(Brand brand);
    }
}

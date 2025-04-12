namespace Project.Contract
{
    public interface IBrandRepository
    {
        Task<IEnumerable<Brand>> GetAllBrandsAsync();
        Task<Brand?> GetBrandByIdAsync(int id);
        Task UpdateBrandAsync(Brand brand);
        Task DeleteBrandAsync(int id);
        Task AddBrandAsync(Brand brand);
    }
}

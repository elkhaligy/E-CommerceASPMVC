namespace Project.Contract
{
    public interface IBrandService
    {
        Task<IEnumerable<Brand>> GetBrandsAsync();

    }
}

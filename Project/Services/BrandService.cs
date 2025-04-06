using Project.Contract;

namespace Project.Services
{
    public class BrandService:IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
            
        }
        public async Task<IEnumerable<Brand>> GetBrandsAsync()
        {
            return await _brandRepository.GetAllBrandsAsync();
        }

    }
}

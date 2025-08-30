using FastFood.Infra.Data.Context;

namespace FastFood.DataSource
{
    public class FastFoodDataSource : IDataSource 
    {
        private readonly FastFoodDbContext _context;

        public FastFoodDataSource(FastFoodDbContext context)
        {
            _context = context;
        }

        public FastFoodDbContext GetFastFoodContext()
        {
            return _context;
        }
    }
}

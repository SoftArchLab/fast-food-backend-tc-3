using FastFood.Infra.Data.Context;

namespace FastFood.DataSource
{
    public interface IDataSource
    {
        FastFoodDbContext GetFastFoodContext();
    }
}
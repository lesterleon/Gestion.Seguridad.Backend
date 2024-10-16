namespace Gestion.Seguridad.Backend.Infrastructure.Persistence.Repositories.Base
{
    public interface IBaseInterface<TEntity, TId> where TEntity : class
    {
        Task<TId> CrearAsync(TEntity entity);
        Task<bool> EditarAsync(TEntity entity);
        Task<bool> EliminarAsync(TId id);
        Task<IList<TEntity>> ListarAsync();
        Task<TEntity?> ObtenerAsync(TId id);
    }
}

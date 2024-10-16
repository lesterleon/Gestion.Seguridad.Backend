using Gestion.Seguridad.Backend.Domain.Entities;
using Gestion.Seguridad.Backend.Infrastructure.Persistence.Repositories.Base;

namespace Gestion.Seguridad.Backend.Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IUsuarioRepository : IBaseInterface<Usuario, int>
    {
        Task<bool> LoginAsync(string email, string clave);
    }
}

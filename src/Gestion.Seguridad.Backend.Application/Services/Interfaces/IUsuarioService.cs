using Gestion.Seguridad.Backend.Domain.DTOs.Usuario;

namespace Gestion.Seguridad.Backend.Application.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<int> CrearAsync(CrearUsuarioDto dto);
        Task<bool> EditarAsync(EditarUsuarioDto dto);
        Task<bool> EliminarAsync(int id);
        Task<IList<ListarUsuarioDto>> ListarAsync();
        Task<ObtenerUsuarioDto?> ObtenerAsync(int id);
        Task<bool> LoginAsync(string email, string clave);
    }
}

namespace Gestion.Seguridad.Backend.Domain.DTOs.Usuario
{
    public class LoginUsuarioDto
    {
        public string Email { get; set; } = string.Empty;
        public string Clave { get; set; } = string.Empty;
    }
}

namespace Gestion.Seguridad.Backend.Domain.DTOs.Usuario
{
    public class CrearUsuarioDto
    {
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string DNI { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Clave { get; set; } = string.Empty;
        public string? Direccion { get; set; } = null;
    }
}

using FluentValidation;
using Gestion.Seguridad.Backend.Domain.DTOs.Usuario;

namespace Gestion.Seguridad.Backend.Application.Validators.Usuario
{
    public class LoginUsuarioValidator : AbstractValidator<LoginUsuarioDto>
    {
        public LoginUsuarioValidator()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(x => x.Clave).NotEmpty().NotNull();
        }
    }
}

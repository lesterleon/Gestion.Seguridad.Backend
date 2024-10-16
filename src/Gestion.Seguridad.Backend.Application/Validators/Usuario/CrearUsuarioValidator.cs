using FluentValidation;
using Gestion.Seguridad.Backend.Domain.DTOs.Usuario;

namespace Gestion.Seguridad.Backend.Application.Validators.Usuario
{
    public class CrearUsuarioValidator : AbstractValidator<CrearUsuarioDto>
    {
        public CrearUsuarioValidator()
        {
            RuleFor(x => x.Nombres).NotEmpty().NotNull();
            RuleFor(x => x.Apellidos).NotEmpty().NotNull();
            RuleFor(x => x.DNI).NotEmpty().NotNull().Length(8);
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
        }
    }
}

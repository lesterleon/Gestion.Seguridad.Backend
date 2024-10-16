using FluentValidation;
using Gestion.Seguridad.Backend.Application.Services.Interfaces;
using Gestion.Seguridad.Backend.Domain.DTOs.Usuario;
using Gestion.Seguridad.Backend.Domain.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Gestion.Seguridad.Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioService _usuarioService;
        private readonly IValidator<CrearUsuarioDto> _crearUsuarioValidator;
        private readonly IValidator<LoginUsuarioDto> _loginUsuarioValidator;
        public AuthController(IConfiguration configuration, IUsuarioService usuarioService, IValidator<CrearUsuarioDto> crearUsuarioValidator, IValidator<LoginUsuarioDto> loginUsuarioValidator)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _usuarioService = usuarioService ?? throw new ArgumentNullException(nameof(usuarioService));
            _crearUsuarioValidator = crearUsuarioValidator ?? throw new ArgumentNullException(nameof(crearUsuarioValidator));
            _loginUsuarioValidator = loginUsuarioValidator ?? throw new ArgumentNullException(nameof(loginUsuarioValidator));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CrearUsuario")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ObtenerUsuarioDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] CrearUsuarioDto dto)
        {
            var result = _crearUsuarioValidator.Validate(dto);
            var errors = result.Errors.Select(x => x.ErrorMessage);
            if (!result.IsValid)
            {
                return new BadRequestObjectResult(errors);
            }
            var uri = new Uri("/Usuario/", UriKind.Relative);
            var id = await _usuarioService.CrearAsync(dto);
            return new CreatedResult(uri, id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginUsuarioDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUsuarioDto dto)
        {
            var result = _loginUsuarioValidator.Validate(dto);
            var errors = result.Errors.Select(x => x.ErrorMessage);
            if (!result.IsValid)
            {
                return new BadRequestObjectResult(errors);
            }
            var isAuth = await _usuarioService.LoginAsync(dto.Email, CryptoHelper.HashString(dto.Clave));
            var jwt = _configuration.GetSection("Jwt").Get<Jwt>()!;
            var token = new JwtHelper().GenerateToken(jwt);
            return new OkObjectResult(new
            {
                isAuth = isAuth,
                AuthToken = token
            });
        }
    }
}

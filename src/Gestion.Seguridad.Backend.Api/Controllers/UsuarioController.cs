using FluentValidation;
using Gestion.Seguridad.Backend.Application.Services.Interfaces;
using Gestion.Seguridad.Backend.Domain.DTOs.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gestion.Seguridad.Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IValidator<CrearUsuarioDto> _crearUsuarioValidator;
        private readonly IValidator<EditarUsuarioDto> _editarUsuarioValidator;
        public UsuarioController(IUsuarioService usuarioService, IValidator<CrearUsuarioDto> crearUsuarioValidator, IValidator<EditarUsuarioDto> editarUsuarioValidator)
        {
            _usuarioService = usuarioService ?? throw new ArgumentNullException(nameof(usuarioService));
            _crearUsuarioValidator = crearUsuarioValidator ?? throw new ArgumentNullException(nameof(crearUsuarioValidator));
            _editarUsuarioValidator = editarUsuarioValidator ?? throw new ArgumentNullException(nameof(editarUsuarioValidator));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
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
            return new CreatedResult(nameof(GetByIdAsync), await _usuarioService.CrearAsync(dto));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] EditarUsuarioDto dto)
        {
            var result = _editarUsuarioValidator.Validate(dto);
            var errors = result.Errors.Select(x => x.ErrorMessage);
            if (!result.IsValid)
            {
                return new BadRequestObjectResult(errors);
            }
            if (id != dto.Id)
            {
                return new ConflictResult();
            }
            await _usuarioService.EditarAsync(dto);
            return new NoContentResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            if (id <= 0)
            {
                return new BadRequestResult();
            }
            var result = await _usuarioService.ObtenerAsync(id);
            if (result == null)
            {
                return new NotFoundResult();
            }
            await _usuarioService.EliminarAsync(id);
            return new NoContentResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<ListarUsuarioDto>))]
        public async Task<IActionResult> GetAllAsync()
        {
            return new OkObjectResult(await _usuarioService.ListarAsync());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ObtenerUsuarioDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return new BadRequestResult();
            }
            var result = await _usuarioService.ObtenerAsync(id);
            if (result == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(result);
        }
    }
}

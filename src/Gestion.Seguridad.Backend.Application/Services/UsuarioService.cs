using AutoMapper;
using Gestion.Seguridad.Backend.Application.Services.Interfaces;
using Gestion.Seguridad.Backend.Domain.DTOs.Usuario;
using Gestion.Seguridad.Backend.Domain.Entities;
using Gestion.Seguridad.Backend.Domain.Helpers;
using Gestion.Seguridad.Backend.Infrastructure.Persistence.Repositories.Interfaces;

namespace Gestion.Seguridad.Backend.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IMapper mapper, IUsuarioRepository usuarioRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
        }

        public async Task<int> CrearAsync(CrearUsuarioDto dto)
        {
            var entity = _mapper.Map<Usuario>(dto);
            entity.Clave = CryptoHelper.HashString(entity.Clave);
            var id = await _usuarioRepository.CrearAsync(entity);
            return id;
        }

        public async Task<bool> EditarAsync(EditarUsuarioDto dto)
        {
            var entity = _mapper.Map<Usuario>(dto);
            return await _usuarioRepository.EditarAsync(entity);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            return await _usuarioRepository.EliminarAsync(id);
        }

        public async Task<IList<ListarUsuarioDto>> ListarAsync()
        {
            var list = await _usuarioRepository.ListarAsync();
            var dto = _mapper.Map<IList<ListarUsuarioDto>>(list);
            return dto;
        }

        public async Task<bool> LoginAsync(string email, string clave)
        {
            return await _usuarioRepository.LoginAsync(email, clave);
        }

        public async Task<ObtenerUsuarioDto?> ObtenerAsync(int id)
        {
            var entity = await _usuarioRepository.ObtenerAsync(id);
            var dto = _mapper.Map<ObtenerUsuarioDto>(entity);
            return dto;
        }
    }
}

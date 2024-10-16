using Dapper;
using Gestion.Seguridad.Backend.Domain.Entities;
using Gestion.Seguridad.Backend.Infrastructure.Persistence.Configuration;
using Gestion.Seguridad.Backend.Infrastructure.Persistence.Repositories.Interfaces;
using System.Data;

namespace Gestion.Seguridad.Backend.Infrastructure.Persistence.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public async Task<int> CrearAsync(Usuario entity)
        {
            using (var connection = SqlServerDatabase.CreateSqlServerConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Nombres", entity.Nombres, DbType.String, size: 100, direction: ParameterDirection.Input);
                parameters.Add("@Apellidos", entity.Apellidos, DbType.String, size: 100, direction: ParameterDirection.Input);
                parameters.Add("@DNI", entity.DNI, DbType.String, size: 20, direction: ParameterDirection.Input);
                parameters.Add("@Email", entity.Email, DbType.String, size: 100, direction: ParameterDirection.Input);
                parameters.Add("@Clave", entity.Clave, DbType.String, size: 100, direction: ParameterDirection.Input);
                parameters.Add("@Direccion", entity.Direccion, DbType.String, size: 200, direction: ParameterDirection.Input);
                parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                await connection.ExecuteAsync("usp_Insertar_Usuario", parameters, commandType: CommandType.StoredProcedure);
                int id = parameters.Get<int>("@Id");
                return id;
            }
        }

        public async Task<bool> EditarAsync(Usuario entity)
        {
            using (var connection = SqlServerDatabase.CreateSqlServerConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", entity.Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
                parameters.Add("@Nombres", entity.Nombres, DbType.String, size: 100, direction: ParameterDirection.Input);
                parameters.Add("@Apellidos", entity.Apellidos, DbType.String, size: 100, direction: ParameterDirection.Input);
                parameters.Add("@DNI", entity.DNI, DbType.String, size: 20, direction: ParameterDirection.Input);
                parameters.Add("@Email", entity.Email, DbType.String, size: 100, direction: ParameterDirection.Input);
                parameters.Add("@Direccion", entity.Direccion, DbType.String, size: 200, direction: ParameterDirection.Input);
                await connection.ExecuteAsync("usp_Actualizar_Usuario", parameters, commandType: CommandType.StoredProcedure);
                return true;
            }
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using (var connection = SqlServerDatabase.CreateSqlServerConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
                await connection.ExecuteAsync("usp_Eliminar_Usuario", parameters, commandType: CommandType.StoredProcedure);
                return true;
            }
        }

        public async Task<IList<Usuario>> ListarAsync()
        {
            using (var connection = SqlServerDatabase.CreateSqlServerConnection())
            {
                return (IList<Usuario>)await connection.QueryAsync<Usuario>("usp_Listar_Usuario", commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<bool> LoginAsync(string email, string clave)
        {
            using(var connection = SqlServerDatabase.CreateSqlServerConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Email", email, DbType.String);
                parameters.Add("@Clave", clave, DbType.String);
                var count = await connection.ExecuteScalarAsync<int>("usp_Login_Usuario", parameters, commandType: CommandType.StoredProcedure);
                return count > 0;
            }
        }

        public async Task<Usuario?> ObtenerAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            using (var connection = SqlServerDatabase.CreateSqlServerConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Usuario>("usp_Obtener_Usuario", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}

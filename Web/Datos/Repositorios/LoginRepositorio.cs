using Dapper;
using Datos.Interfaces;
using Modelos;
using MySql.Data.MySqlClient;

namespace Datos.Repositorios
{
    //Heredamos de la Interfaz de ILoginRepositorio
    public class LoginRepositorio : ILoginRepositorio
    {
        private string CadenaConexion; //variable para la conexion
        public LoginRepositorio(string _cadenaConexion)
        {
            CadenaConexion = _cadenaConexion; //Le pasamos la cadena de conexion
        }

        //METODO PARA LA CONEXION DE MYSQL
        private MySqlConnection Conexion()
        {
            return new MySqlConnection(CadenaConexion);
        }

        //Implementamos Interfaz
        //METODO ASINCRONO PARA VALIDAR USUARIO
        public async Task<bool> ValidarUsuarioAsync(Login login)
        {
            bool valido = false;
            try
            {
                using MySqlConnection _conexion = Conexion();
                await _conexion.OpenAsync();
                string sql = "SELECT 1 FROM usuario WHERE CodigoUsuario = @CodigoUsuario AND Contrasena = @Contrasena;";
                valido = await _conexion.ExecuteScalarAsync<bool>(sql, login);
            }
            catch (Exception)
            {
            }
            return valido;
        }
    }
}

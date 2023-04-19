using Dapper;
using Datos.Interfaces;
using Modelos;
using MySql.Data.MySqlClient;

namespace Datos.Repositorios
{
    //HEREDAMOS DE LA INTERFAZ IUsuarioRepositorio
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private string CadenaConexion; //variable para la conexion

        public UsuarioRepositorio(string _cadenaConexion)
        {
            CadenaConexion = _cadenaConexion;  //Le pasamos la cadena de conexion
        }

        //METODO PARA LA CONEXION DE MYSQL
        private MySqlConnection Conexion()
        {
            return new MySqlConnection(CadenaConexion);
        }

        //METODO PARA ACTUALIZAR REGISTRO
        public async Task<bool> ActualizarAsync(Usuario usuario)
        {
            bool resultado = false;
            try
            {
                using MySqlConnection _conexion = Conexion(); //llamamos el metodo de conexion
                await _conexion.OpenAsync(); //abre la conexion asincrona
                string sql = @"UPDATE usuario SET Nombre = @Nombre, Contrasena = @Contrasena, Correo = @Correo, Rol = @Rol, Foto = @Foto, EstaActivo = @EstaActivo 
                                WHERE CodigoUsuario = @CodigoUsuario;";
                //Convertirlo en Boleano
                resultado = Convert.ToBoolean(await _conexion.ExecuteAsync(sql, usuario));
            }
            catch (Exception)
            {
            }
            return resultado;
        }

        //METODO PARA ELIMINAR REGISTRO 
        public async Task<bool> EliminarAsync(string codigoUsuario)
        {
            bool resultado = false;
            try
            {
                using MySqlConnection _conexion = Conexion(); //llamamos el metodo de conexion
                await _conexion.OpenAsync(); //abre la conexion asincrona
                string sql = "DELETE FROM usuario WHERE CodigoUsuario = @CodigoUsuario;";
                //Convertirlo en Boleano
                resultado = Convert.ToBoolean(await _conexion.ExecuteAsync(sql, new { codigoUsuario }));
            }
            catch (Exception)
            {
            }
            return resultado;
        }

        //METODO PARA DEVOLVER TODOS LOS REGISTROS DE USUARIO
        public async Task<IEnumerable<Usuario>> GetListaAsync()
        {
            IEnumerable<Usuario> lista = new List<Usuario>();
            try
            {
                using MySqlConnection _conexion = Conexion();
                await _conexion.OpenAsync();
                string sql = "SELECT * FROM usuario;";
                lista = await _conexion.QueryAsync<Usuario>(sql);
            }
            catch (Exception)
            {
            }
            return lista;
        }

        //METODO PARA DEVOLVER USUARIO POR CODIGO
        public async Task<Usuario> GetPorCodigoAsync(string codigoUsuario)
        {
            //Declaramos un objeto y lo instanciamos
            Usuario user = new Usuario();
            try
            {
                using MySqlConnection _conexion = Conexion(); //llamamos el metodo de conexion
                await _conexion.OpenAsync(); //abre la conexion asincrona
                string sql = "SELECT * FROM usuario WHERE CodigoUsuario = @CodigoUsuario;";
                //Convertirlo en Boleano
                user = await _conexion.QueryFirstAsync<Usuario>(sql, new { codigoUsuario }); //QueryFirstAsync solo retorna un solo resultado 
            }
            catch (Exception)
            {
            }
            return user;
        }

        //METODO PARA CREAR UN NUEVO USUARIO
        public async Task<bool> NuevoAsync(Usuario usuario)
        {
            bool resultado = false;
            try
            {
                using MySqlConnection _conexion = Conexion(); //llamamos el metodo de conexion
                await _conexion.OpenAsync(); //abre la conexion asincrona
                string sql = @"INSERT INTO usuario (CodigoUsuario,Nombre,Contrasena,Correo,Rol,Foto,FechaCreacion,EstaActivo) 
                                VALUES (@CodigoUsuario,@Nombre,@Contrasena,@Correo,@Rol,@Foto,@FechaCreacion,@EstaActivo);";
                //Convertirlo en Boleano
                resultado = Convert.ToBoolean(await _conexion.ExecuteAsync(sql, usuario));
            }
            catch (Exception)
            {
            }
            return resultado;
        }
    }
}

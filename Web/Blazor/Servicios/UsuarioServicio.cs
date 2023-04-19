using Blazor.Interfaces;
using Datos.Interfaces;
using Datos.Repositorios;
using Modelos;

namespace Blazor.Servicios
{
    //Heredamos de IUsuarioServicio
    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly Config _config;
        private IUsuarioRepositorio usuarioRepositorio;

        //Constructor
        public UsuarioServicio(Config config)
        {
            _config = config;
            usuarioRepositorio = new UsuarioRepositorio(config.CadenaConexion);
        }

        public async Task<bool> ActualizarAsync(Usuario usuario)
        {
            //Retornamos la tarea del usuariorepositorio y llamamos el metodo actializar y le pasmos el parametro
            return await usuarioRepositorio.ActualizarAsync(usuario);
        }

        public async Task<bool> EliminarAsync(string codigo)
        {
            //retornamos el metodo
            return await usuarioRepositorio.EliminarAsync(codigo);
        }

        public async Task<IEnumerable<Usuario>> GetListaAsync()
        {
            //retornamos la lista
            return await usuarioRepositorio.GetListaAsync();
        }

        public async Task<Usuario> GetPorCodigoAsync(string codigo)
        {
            //retornamos el metodo 
            return await usuarioRepositorio.GetPorCodigoAsync(codigo);
        }

        public async Task<bool> NuevoAsync(Usuario usuario)
        {
            //Retornamos el metodo
            return await usuarioRepositorio.NuevoAsync(usuario);
        }
    }
}

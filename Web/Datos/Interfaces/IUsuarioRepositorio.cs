using Modelos;

namespace Datos.Interfaces
{
    public interface IUsuarioRepositorio
    {
        //Declarar solo los metodos 
        //Metodo asincrono para retornar todo el objeto usuario
        Task<Usuario> GetPorCodigoAsync(string codigoUsuario);

        //METODO PARA CREAR UN NUEVO REGISTRO 
        Task<bool> NuevoAsync(Usuario usuario);

        //METODO PARA ACTUALIZAR REGISTRO 
        Task<bool> ActualizarAsync(Usuario usuario);

        //METODO PARA ELIMINAR REGISTRO
        Task<bool> EliminarAsync(string codigo); // solo pasamos el codigo de usuario

        //METODO PARA RETORNAR TODA UNA LISTA DE USUARIOS
        Task<IEnumerable<Usuario>> GetListaAsync();
    }
}

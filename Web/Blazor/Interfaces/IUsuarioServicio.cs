using Modelos;

namespace Blazor.Interfaces
{
    public interface IUsuarioServicio
    {
        //Declaramos todos los metodos que va a tener este servicio
        //que van a ser los mismos que implementamos en el repositorio
        //Metodo asincrono para retornar todo el objeto usuario
        Task<Usuario> GetPorCodigoAsync(string codigo);

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

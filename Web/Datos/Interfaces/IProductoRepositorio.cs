using Modelos;

namespace Datos.Interfaces
{
    public interface IProductoRepositorio
    {
        //DECLARAMOS SOLO LOS METODOS 

        //METODO PARA CREAR UN NUEVO REGISTRO 
        Task<bool> Nuevo(Producto producto);

        //METODO PARA ACTUALIZAR REGISTRO 
        Task<bool> Actualizar(Producto producto);

        //METODO PARA ELIMINAR REGISTRO
        Task<bool> Eliminar(string codigo); // solo pasamos el codigo de producto

        //METODO PARA RETORNAR TODA UNA LISTA DE PRODUCTO
        Task<IEnumerable<Producto>> GetLista();

        //METODO ASINCRONO PARA RETORNAR TODO EL OBJETO DE PRODUCTO
        Task<Producto> GetPorCodigo(string Codigo);
    }
}

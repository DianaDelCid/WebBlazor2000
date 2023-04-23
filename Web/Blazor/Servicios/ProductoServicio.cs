using Blazor.Interfaces;
using Datos.Interfaces;
using Datos.Repositorios;
using Modelos;

namespace Blazor.Servicios
{
    //Heredamos de IProductoServicio
    public class ProductoServicio : IProductoServicio
    {
        private readonly Config _config;
        private IProductoRepositorio productoRepositorio;

        //Constructor
        public ProductoServicio(Config config)
        {
            _config = config;
            productoRepositorio = new ProductoRepositorio(config.CadenaConexion);
        }

        public async Task<bool> Actualizar(Producto producto)
        {
            //Retornamos la tarea del productorepositorio y llamamos el metodo actializar y le pasamos el parametro
            return await productoRepositorio.Actualizar(producto);
        }

        public async Task<bool> Eliminar(string codigo)
        {
            //retornamos el metodo de eliminar
            return await productoRepositorio.Eliminar(codigo);
        }

        public async Task<IEnumerable<Producto>> GetLista()
        {
            //retornamos la lista de productos
            return await productoRepositorio.GetLista();
        }

        public async Task<Producto> GetPorCodigo(string Codigo)
        {
            //retornamos el metodo
            return await productoRepositorio.GetPorCodigo(Codigo);
        }

        public async Task<bool> Nuevo(Producto producto)
        {
            //retornamos el metodo de nuevo producto
            return await productoRepositorio.Nuevo(producto);
        }
    }
}

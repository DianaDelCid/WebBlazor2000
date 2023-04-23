using Blazor.Interfaces;
using Microsoft.AspNetCore.Components;
using Modelos;

namespace Blazor.Pages.MisProductos
{
    public partial class Productos
    {
        [Inject] IProductoServicio productoServicio { get; set; } //Inyectamos el servicio
        IEnumerable<Producto> listaProductos { get; set; } //Declaramos una lista

        //Sobreescribimos el metodo para poder cargar la lista al inicio
        protected override async Task OnInitializedAsync()
        {
            listaProductos = await productoServicio.GetLista();
        }

    }
}

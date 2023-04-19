using Blazor.Interfaces;
using Microsoft.AspNetCore.Components;
using Modelos;

namespace Blazor.Pages.MisUsuarios
{
    //AQUI AGREGAMOS CODIGO C#
    public partial class Usuarios //poner partial para poder tener mas ordenado las clases
    {
        //Inyeccion de dependencia
        [Inject] private IUsuarioServicio usuarioServicio { get; set; }
        //declaramos una lista
        private IEnumerable<Usuario> lista { get; set; }

        //Para podder cargar la lista al inicio
        protected override async Task OnInitializedAsync()
        {
            lista = await usuarioServicio.GetListaAsync();
        }

    }
}

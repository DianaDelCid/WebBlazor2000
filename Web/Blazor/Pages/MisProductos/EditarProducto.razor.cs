using Blazor.Interfaces;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Modelos;

namespace Blazor.Pages.MisProductos
{
    public partial class EditarProducto
    {
        [Inject] private IProductoServicio productoServicio { get; set; } //Inyectamos el servicio
        [Inject] private NavigationManager navigationManager { get; set; } //Objeto de navegacion
        [Inject] private SweetAlertService Swal { get; set; } //Injectamos el sweetalert

        Producto producto = new Producto(); //Objeto de producto

        [Parameter] public string Codigo { get; set; } //resive el parametro
        string imgUrl = string.Empty; //Variable global para la foto 

        //SOBRESCRIBIMOS EL METODO 
        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(Codigo))
            {
                producto = await productoServicio.GetPorCodigo(Codigo);
            }
        }

        //METODO PARA SELECCIONAR LA FOTO
        private async Task SeleccionarImagen(InputFileChangeEventArgs e)
        {
            //Capturamos lo que el usuario a seleccionado
            IBrowserFile imgFile = e.File;
            var buffers = new byte[imgFile.Size];
            producto.Foto = buffers; //Le pasamos el arreglo de byte
            await imgFile.OpenReadStream().ReadAsync(buffers);
            string imageType = imgFile.ContentType;
            //Para poder visualizar en pantalla
            imgUrl = $"data:{imageType};base64,{Convert.ToBase64String(buffers)}";  //L0 convertimos
        }

        //METODO DE GUARDAR
        protected async Task Guardar()
        {
            if (string.IsNullOrWhiteSpace(producto.Codigo) || string.IsNullOrWhiteSpace(producto.Descripcion)) //Valida que los campos no esten vacio o que este con espacios en blanco
            {
                return;
            }

            bool edito = await productoServicio.Actualizar(producto);

            if (edito)
            {
                await Swal.FireAsync("Atención", "Producto Guardado", SweetAlertIcon.Success); //Que fue satisfactorio
            }
            else
            {
                await Swal.FireAsync("Eroor", "No se pudo Guardar el producto", SweetAlertIcon.Error);

            }
        }

        //METODO DE CANCELAR
        protected async Task Cancelar()
        {
            navigationManager.NavigateTo("/Productos"); //Nos manda a la ruta de productos
        }

        //METODO DE ELIMINAR
        protected async Task Eliminar()
        {
            bool elimino = false;
            SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
            {
                //Mostrara una ventana de alerta de si quiere eliminar
                Title = "¿Seguro que desea eliminar el producto?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
                ConfirmButtonText = "Aceptar",
                CancelButtonText = "Cancelar"
            });

            if (!string.IsNullOrEmpty(result.Value)) //validar si esta vacio
            {
                elimino = await productoServicio.Eliminar(producto.Codigo);

                if (elimino)
                {
                    await Swal.FireAsync("Felicidades", "Producto Eliminado", SweetAlertIcon.Success); //Que fue satisfactorio
                    navigationManager.NavigateTo("/Productos"); //Nos manda a la pantalla de usuarios
                }
                else
                {
                    await Swal.FireAsync("Error", "No se pudo eliminar el producto", SweetAlertIcon.Error);
                }
            }
        }

    }
}

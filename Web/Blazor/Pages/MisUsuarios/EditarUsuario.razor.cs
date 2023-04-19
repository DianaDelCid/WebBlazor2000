using Blazor.Interfaces;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Modelos;

namespace Blazor.Pages.MisUsuarios
{
    public partial class EditarUsuario
    {
        [Inject] private IUsuarioServicio usuarioServicio { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; } //Objeto de navegacion
        [Inject] private SweetAlertService Swal { get; set; } //Injectamos el sweetalert

        private Usuario user = new Usuario(); //Objeto de usuario
        [Parameter] public string CodigoUsuario { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(CodigoUsuario))
            {
                user = await usuarioServicio.GetPorCodigoAsync(CodigoUsuario);
            }
        }

        //METODO DE GUARDAR
        protected async void Guardar()
        {
            if (string.IsNullOrWhiteSpace(user.CodigoUsuario) || string.IsNullOrWhiteSpace(user.Nombre) || //Valida que no este vacio o que este con espacios en blanco
                string.IsNullOrWhiteSpace(user.Contrasena) || string.IsNullOrWhiteSpace(user.Rol) || user.Rol == "Seleccionar")
            {
                return; //Cancela la ejecucion
            }

            bool edito = await usuarioServicio.ActualizarAsync(user);

            if (edito)
            {
                await Swal.FireAsync("Felicidades", "Usuario Actualizado", SweetAlertIcon.Success); //Que fue satisfactorio
            }
            else
            {
                await Swal.FireAsync("Eroor", "No se pudo actualizar le usuario", SweetAlertIcon.Error);
            }
        }

        //METODO DE CANCELAR
        protected async void Cancelar()
        {
            navigationManager.NavigateTo("/Usuarios"); //Nos manda a la ruta de usuaios
        }

        //METODO DE ELIMINAR
        protected async void Eliminar()
        {
            SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
            {
                //Mostrara una ventana de alerta de si quiere eliminar
                Title = "¿Seguro que desea eliminar el usuario?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
                ConfirmButtonText = "Aceptar",
                CancelButtonText = "Cancelar"
            });

            if (!string.IsNullOrEmpty(result.Value)) //validar si esta vacio
            {
                bool elimino = await usuarioServicio.EliminarAsync(user.CodigoUsuario);

                if (elimino)
                {
                    await Swal.FireAsync("Felicidades", "Usuario Elimino", SweetAlertIcon.Success); //Que fue satisfactorio

                    navigationManager.NavigateTo("/Usuarios"); //Nos manda a la pantalla de usuarios
                }
                else
                {
                    await Swal.FireAsync("Eroor", "No se pudo eliminar le usuario", SweetAlertIcon.Error);
                }
            }

        }

    }
}

//ENUMERABLE PARA PODER SELECCIONAR EN EL INPUT SELECT
enum Roles
{
    Seleccionar,
    Administrador,
    Usuario
}

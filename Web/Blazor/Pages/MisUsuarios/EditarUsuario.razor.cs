﻿using Blazor.Interfaces;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
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
        string imgUrl = string.Empty; //Variable global para la foto 
        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(CodigoUsuario))
            {
                user = await usuarioServicio.GetPorCodigoAsync(CodigoUsuario);
            }
        }

        //METODO PARA SELECCIONAR LA FOTO
        private async Task SeleccionarImagen(InputFileChangeEventArgs e)
        {
            //Capturamos lo que el usuario a seleccionado
            IBrowserFile imgFile = e.File;
            var buffers = new byte[imgFile.Size];
            user.Foto = buffers; //Le pasamos el arreglo de byte
            await imgFile.OpenReadStream().ReadAsync(buffers);
            string imageType = imgFile.ContentType;
            //Para poder visualizar en pantalla
            imgUrl = $"data:{imageType};base64,{Convert.ToBase64String(buffers)}";  //L0 convertimos
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
                    await Swal.FireAsync("Eroor", "No se pudo eliminar el usuario", SweetAlertIcon.Error);
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

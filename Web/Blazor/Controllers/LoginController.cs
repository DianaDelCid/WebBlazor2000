using Datos.Interfaces;
using Datos.Repositorios;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Modelos;
using System.Security.Claims;

namespace Blazor.Controllers
{
    public class LoginController : Controller
    {
        private readonly Config _config;
        private ILoginRepositorio _loginRepositorio;
        private IUsuarioRepositorio _usuarioRepositorio;

        //Consttructor
        public LoginController(Config config)
        {
            _config = config;
            _loginRepositorio = new LoginRepositorio(config.CadenaConexion);
            _usuarioRepositorio = new UsuarioRepositorio(config.CadenaConexion);
        }

        //METODO DE HttpPost
        [HttpPost("/autenticar/validar")]
        //Enpoint
        public async Task<IActionResult> Validacion(Login login)
        {
            string rol = string.Empty; //variable que almacena el rol
            try
            {
                bool usuarioValido = await _loginRepositorio.ValidarUsuarioAsync(login);
                if (usuarioValido)
                {
                    Usuario user = await _usuarioRepositorio.GetPorCodigoAsync(login.CodigoUsuario);
                    if (user.EstaActivo)
                    {
                        rol = user.Rol;
                        //Arreglo de Claiss
                        var claims = new[]
                        {
                            new Claim(ClaimTypes.Name, user.CodigoUsuario),
                            new Claim(ClaimTypes.Role, user.Rol)
                        };

                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTime.UtcNow.AddMinutes(10) }); //Para iniciar seccion
                    }
                    else
                    {
                        //Metodo que redirige
                        return LocalRedirect("/Login/El usuario no se encuentra activo"); //Lo manda al login con un parametro
                    }
                }
                else
                {
                    //Metodo que redirige
                    return LocalRedirect("/Login/Datos de usuarios invalidos");
                }
            }
            catch (Exception)
            {
            }
            return LocalRedirect("/"); //Ruta principal
        }


        //METODO PARA CERRAR LA SECCION
        [HttpGet("/Salir")]
        public async Task<IActionResult> Cerrar() //No resive ningun parametro
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/Login");
        }
    }
}

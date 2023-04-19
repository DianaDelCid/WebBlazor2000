using Blazor.Interfaces;
using Datos.Interfaces;
using Datos.Repositorios;
using Modelos;

namespace Blazor.Servicios
{
    public class LoginServicio : ILoginServicio
    {
        private readonly Config _config;
        private ILoginRepositorio loginRepositorio;

        //Constructor 
        public LoginServicio(Config config)
        {
            _config = config;
            loginRepositorio = new LoginRepositorio(config.CadenaConexion);
        }

        //METODO DE SERVICIO
        public async Task<bool> ValidarUsuarioAsync(Login login)
        {
            return await loginRepositorio.ValidarUsuarioAsync(login);
        }
    }
}

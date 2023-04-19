using Modelos;

namespace Datos.Interfaces
{
    public interface ILoginRepositorio
    {
        //Solo declaramos metodos no ponemos codigo

        //METODO ASINCRONO
        Task<bool> ValidarUsuarioAsync(Login login);

    }
}

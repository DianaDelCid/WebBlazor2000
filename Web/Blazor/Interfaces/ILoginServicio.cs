using Modelos;

namespace Blazor.Interfaces
{
    public interface ILoginServicio
    {
        //METODO ASINCRONO
        Task<bool> ValidarUsuarioAsync(Login login);
    }
}

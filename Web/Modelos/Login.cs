namespace Modelos
{
    public class Login
    {
        //Propiedades
        public string CodigoUsuario { get; set; }
        public string Contrasena { get; set; }

        //Constructor vacio
        public Login()
        {
        }

        //Constructor con parametros
        public Login(string codigoUsuario, string contrasena)
        {
            CodigoUsuario = codigoUsuario;
            Contrasena = contrasena;
        }
    }
}

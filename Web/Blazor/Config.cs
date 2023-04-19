namespace Blazor
{
    public class Config
    {
        public string CadenaConexion { get; set; } //Para poder leer la cadena de conexion

        //Constructor
        public Config(string _cadenaConexion)
        {
            CadenaConexion = _cadenaConexion;
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Modelos
{
    public class Usuario
    {

        //Propiedades
        [Required(ErrorMessage = "El Codigo es Obligatorio")] //Campo debajo abligatorio
        public string CodigoUsuario { get; set; }
        [Required(ErrorMessage = "El Nombre es Obligatorio")] //Campo debajo abligatorio
        public string Nombre { get; set; }
        public string Contrasena { get; set; }
        public string Correo { get; set; }
        [Required(ErrorMessage = "El Rol es Obligatorio")] //Campo debajo abligatorio
        public string Rol { get; set; }
        public byte[] Foto { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool EstaActivo { get; set; } //propiedad para ver si usuario esta activo o no

        //Constructor vacio
        public Usuario()
        {

        }

        //Constructor con parametros
        public Usuario(string codigoUsuario, string nombre, string contrasena, string correo, string rol, byte[] foto, DateTime fechaCreacion, bool estaActivo)
        {
            CodigoUsuario = codigoUsuario;
            Nombre = nombre;
            Contrasena = contrasena;
            Correo = correo;
            Rol = rol;
            Foto = foto;
            FechaCreacion = fechaCreacion;
            EstaActivo = estaActivo;
        }

    }
}

using System.ComponentModel.DataAnnotations;

namespace Modelos
{
    public class Producto
    {
        //PROPIEDADES
        [Required(ErrorMessage = "El Código es Obligatorio")] //Mensaje de error Campo Codigo es abligatorio
        public string Codigo { get; set; }
        [Required(ErrorMessage = "La descripción es Obligatoria")] //Mensaje de error Campo descripcion es abligatorio
        public string Descripcion { get; set; }
        public int Existencia { get; set; }
        public decimal Precio { get; set; }
        public byte[] Foto { get; set; }
        public bool EstaActivo { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace API_BasicStore.DTOs
{
    public class ProductoInDTO
    {
        [Required]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        [Required]
        public float Precio { get; set; }
        [Required]
        public int Stock { get; set; }
    }

    public class ChangeStockDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int NewStock { get; set; }
    }
}

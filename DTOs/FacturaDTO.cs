using API_BasicStore.Models;
using System.ComponentModel.DataAnnotations;

namespace API_BasicStore.DTOs
{
    public class FacturaDTO
    {
        public Factura factura;
        public List<DetalleFactura> detalles;
    }

    public class FacturaInDTO
    {
        [Required]
        public int IdCliente { get; set; }
        [Required]
        public List<DetalleFacturaDTO> articulos { get; set; }
    }

    public class DetalleFacturaDTO
    {
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public int ProductoID { get; set; }
    }
}

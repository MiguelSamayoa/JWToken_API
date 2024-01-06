using System.ComponentModel.DataAnnotations;

namespace API_BasicStore.Models
{
    public class DetalleFactura
    {
        public int Id { get; set; }

        public int Cantidad { get; set; }

        public float subTotal { get; set; }

        public Factura Factura { get; set; }

        public Producto Producto { get; set; }
    }
}

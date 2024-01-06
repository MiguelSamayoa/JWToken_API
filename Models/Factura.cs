using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace API_BasicStore.Models
{
    public class Factura
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }
        public float Total { get; set; }

        public List<DetalleFactura> Detalles { get; set; }
        public User Cliente { get; set; }
    }
}

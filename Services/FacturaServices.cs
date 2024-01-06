using API_BasicStore.DTOs;
using API_BasicStore.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API_BasicStore.Services
{
    public interface IFacturaServices
    {
        public Task<Factura> CrearFactura(FacturaInDTO FacturaCompleta);
        public Task<List<FacturaDTO>> GetFacturas();

    }

    public class FacturaServices : IFacturaServices
    {
        private readonly DBContext context;
        private readonly IMapper mapper;

        //Contructor
        public FacturaServices(DBContext Context, IMapper mapper)
        {
            context = Context;
            this.mapper = mapper;
        }


        public async Task<Factura> CrearFactura(FacturaInDTO FacturaCompleta)
        {
            float total = 0;
            List<DetalleFactura> Articulos = new List<DetalleFactura>();

            foreach(DetalleFacturaDTO articulo in FacturaCompleta.articulos)
            {
                Producto producto = context.Productos.Where(x => x.Id == articulo.ProductoID).FirstOrDefault();

                if (producto == null) throw new Exception("Producto no existe");

                total += producto.Precio * articulo.Cantidad;

                Articulos.Add(new DetalleFactura() {
                    Cantidad = articulo.Cantidad,
                    subTotal = producto.Precio * articulo.Cantidad,
                    Producto = producto
                });
            }

            Factura factura = new Factura()
            {
                Fecha = DateTime.Now,
                Total = total,
                Cliente = context.Usuario.Where(x => x.Id == FacturaCompleta.IdCliente).FirstOrDefault(),
            };
            await context.Facturas.AddAsync(factura);

            foreach ( DetalleFactura art in Articulos )
            {
                art.Factura = factura;
            }

            await context.AddRangeAsync( Articulos );

            await context.SaveChangesAsync();



            return factura;
        }

        public async Task<List<FacturaDTO>> GetFacturas()
        {
            List<Factura> facturas = context.Facturas.ToList();

            if (facturas.Count == 0) throw new Exception("No se encontro ninugna factura");

            List<FacturaDTO> FacturaCompleta = new List<FacturaDTO>();

            foreach(Factura fac in facturas)
            {
                List<DetalleFactura> Articulos = await context.DetalleFactura.Where(x => x.Factura.Id == fac.Id).ToListAsync();

                FacturaCompleta.Add(new FacturaDTO() { factura = fac, detalles = Articulos }) ;
            }

            return FacturaCompleta;
        }
    }
}

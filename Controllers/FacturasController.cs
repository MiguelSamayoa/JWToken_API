using API_BasicStore.DTOs;
using API_BasicStore.Models;
using API_BasicStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace API_BasicStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class FacturasController:ControllerBase
    {
        private readonly IFacturaServices facturaServices;
        private readonly IProductServices productoServices;

        public FacturasController(IFacturaServices FacturaServices, IProductServices ProductoServices)
        {
            facturaServices = FacturaServices;
            productoServices = ProductoServices;
        }

        [HttpPost]
        public async Task<ActionResult<Factura>> PostFatura(List<DetalleFacturaDTO> articulos)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            var userIdClaim = claimsIdentity?.FindFirst("IdUser");

            FacturaInDTO FacturaCompleta = new FacturaInDTO()
            {
                IdCliente = int.Parse(userIdClaim.Value),
                articulos = articulos
            };

            Factura factura = await facturaServices.CrearFactura(FacturaCompleta);

            if (factura == null) return BadRequest();

            return Ok(factura);
        }


        [HttpGet]
        public async Task<ActionResult<List<FacturaDTO>>> GetFacturas()
        {
            List<FacturaDTO> factura = await facturaServices.GetFacturas();

            return Ok(factura);
        }
    }
}

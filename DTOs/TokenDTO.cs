using API_BasicStore.Models;

namespace API_BasicStore.DTOs
{
    public class TokenDTO
    {
        public string Token { get; set; }
        public DateTime Creacion { get; set; }
        public UserDTO Usuario { get; set; }
    }
}

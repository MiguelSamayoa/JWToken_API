using System.Security.Claims;

namespace API_BasicStore.Models
{
    public class SessionToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime Creacion { get; set; }

        public int UserId { get; set; }

        public User Usuario { get; set; }
    }
}
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace API_BasicStore.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserTag { get; set; }

        public string Password { get; set; }
        public byte[] Salt { get; set; }
    }
}

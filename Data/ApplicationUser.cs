using Microsoft.AspNetCore.Identity;

namespace CarRental.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? Nombres { get; set; }

        public string? Cedula { get; set; }

        public string? NickName { get; set; }
    }

}

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTech.Marketing.Domain.Entities
{
    public partial class ApplicationUser : IdentityUser
    {
        public virtual Client Client { get; set; } = null!;
    }
}

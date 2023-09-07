using System.ComponentModel.DataAnnotations;

namespace MicrofundamentoAPISWEBServices_fuel_manager.Models
{
    public class AuthenticateDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string PassWord { get; set; }
    }
}

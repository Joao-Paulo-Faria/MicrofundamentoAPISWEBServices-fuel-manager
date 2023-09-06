using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MicrofundamentoAPISWEBServices_fuel_manager.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        [JsonIgnore]
        public string Password { get; set; }
        [Required]
        public Perfil Perfil { get; set; }

        //meu usuário vai esta associado a uma coleção de varios veiculosusuarios...

        public ICollection<VeiculoUsuarios> Veiculos { get; set; }
    }

    public enum Perfil
    {
        [Display(Name ="Adminstrador")] //estou adicionando o display name para poder retornar o nome do usuario corretamente para utilizar no framework endentity 
        Administrador,
        [Display(Name = "Usuario")]
        Usuario
    }
}

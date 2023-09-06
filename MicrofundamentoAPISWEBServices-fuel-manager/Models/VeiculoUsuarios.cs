using System.ComponentModel.DataAnnotations.Schema;

namespace MicrofundamentoAPISWEBServices_fuel_manager.Models
{
    [Table("VeiculoUsuarios")]
    public class VeiculoUsuarios
    {

        public int VeiculoId { get; set; }

        public Veiculo Veiculo { get; set; }

        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }
    }
}




//Preciso colcoar uma coleção de veiculos e usuários, por isso precisa criar uma tablea 
//intermediaria pois é uma relação de n para n...

//vai listar todos os usuários relacionados a determinado veiculo, então essa tabela é uma tabela
//de relacionamento, só preciso ter as forenkays para as outras tabelas.
//é uma tabela de relacionamento de n para n



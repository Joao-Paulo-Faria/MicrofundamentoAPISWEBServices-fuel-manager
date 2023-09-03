using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicrofundamentoAPISWEBServices_fuel_manager.Models
{
    [Table("Consumos")] //é para gerar o nome da tabela da forma que eu quero.
    public class Consumo : LinksHATEOS
    {
        //abaixo vamos colocar as propriedades que vamos utilizar na classe consumo.
        [Key] //configurar as propriedades para conexao com banco de dados.
        public int Id { get; set; }//propriedade
        [Required]
        public string Descricao { get; set; }//propriedade
        [Required]
        public DateTime Data { get; set; }//propriedade
        [Required]
        [Column(TypeName ="decimal(18,2)")]
        public decimal Valor { get; set; }//propriedade
        [Required]
        public TipoCombustivel Tipo { get; set; }//propriedade

        //colocando veiculoID o enttiframework automaticamente configura a forenkey
        [Required]
        public int VeiculoId { get; set; }//propriedade

        //navegacao virtual
        //vou criar um objeto do tipo veículo//sendo assim quando o entiframework for carregar
        //as informações de um consumo, ele também pode carregar todas as informações do veiculo
        //associadas a esse consumo. Sendo assim consigo navegar de uma entidade para outra de forma mais simples.
        //esse é o relacionamento virtual, ele preenche pra  mim as informações do veiculo quando eu peço
        public Veiculo Veiculo { get; set; }
    }
    //tipo de combustivel pode ser diesel/etanol/gasolina

    //vamos criar um enumerador para padronizar o tipo de combustivel...

    //Class enum é abastrata e o enum é numeravel , 0,1,2...
    public enum TipoCombustivel
    {
        Diesel,
        Etanol,
        Gasolina
    }

}
//Sabendo que cada consumo está associado a 1 veículo. Então vamos colocar a notação de configuração do entiframework
//

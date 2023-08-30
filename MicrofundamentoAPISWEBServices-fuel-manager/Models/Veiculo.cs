using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicrofundamentoAPISWEBServices_fuel_manager.Models
{
    [Table("Veiculos")] //nome da tabela que quero ter. Essa tabela ao ser criada no banco de dados quero o nome veiculos
    public class Veiculo
    {
        //São as propriedades do veículo
        //chave primária autoincremento
        //o ponto de interrogação (?) informa que o campo pode ser nulo.
        //modelo de negócio que é o veículo que vai armazenar as informações de
        //id/marca/modelo...
        //agora vamos colocar os nossos consumos
        [Key]
        public int Id { get; set; }
        [Required]
        public string Marca { get; set; }
        [Required]
        public string Modelo { get; set; }
        [Required]
        public string Placa { get; set; }
        [Required]
        public int AnoFabricacao { get; set; }
        [Required]
        public int AnoModelo { get; set; }
        public ICollection<Consumo> Consumos { get; set; }
    }
}

//Agora preciso fazer o relacionamento entre as 2 entidades que são veiculo e consumo
//Eu sei que 1 veículo possui varios itens de consumo, vários registros de consumo e agora eu preciso
//fazer essa notação. Preciso falar para o enntyframework que ela faça essa ligação entre a tabela de veiculos
// e a tabela de consumo. Ligacao 1 para n. Em que 1 veículo tem n consumos.
//por ser 1 para n, então nessa entidade não preciso ter nenhuma propriedade de consumo
//mas eu posso ter a navegação virtual que é public ICollection<Consumo> Consumos { get; set; }
//ICollection é uma coleção de consumos
//configurar o entiframework para gerar o nosso banco de dados e a partir dai gerar os nossos controladores e a regra de negocio em funcionamento da nossa web api
//A ICollection permite contar quantos itens existem na enumeração, adicionar, remover itens no fim da coleção, verificar a existência, entre outras operações.
//criar classe de contexto
//e integrar ele as funcionalidades do entiframework - Essa classe é chamada de classe de contexto do banco de dados e coordena a funcionalidade do Entity Framework para um dado modelo de dados. No código dessa classe você especifica quais entidades estão incluídas no modelo de dados.
//Essa classe de contexto administra os objetos entidades durante o tempo de execução, o que inclui preencher objetos com dados de um banco de dados, controlar alterações, e persistir dados para o banco de dados.
//ORM da microsoft - O Entity Framework é a tecnologia de modelagem ORM (mapeamento relacional de objeto) recomendada para novos aplicativos . NET. As Ferramentas do Entity Framework foram projetadas para ajudar você a criar aplicativos EF (Entity Framework)
//O que é e para que serve um ORM? Object-Relational Mapping (ORM), em português, mapeamento objeto-relacional, é uma técnica para aproximar o paradigma de desenvolvimento de aplicações orientadas a objetos ao paradigma do banco de dados relacional.